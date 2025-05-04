using Microsoft.EntityFrameworkCore;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Acheteurs;
using RdC.Domain.DTO.Acheteur;
using RdC.Infrastructure.Common.Persistance;
using System.Net.Http.Json;

namespace RdC.Infrastructure.Acheteurs.Persistance
{
    public class AcheteurRepository : IAcheteurRepository
    {
        private readonly RecouvrementDBContext _dbContext;

        private readonly HttpClient _httpClient;

        public AcheteurRepository(RecouvrementDBContext dbContext, HttpClient httpClient)
        {
            _dbContext = dbContext;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7285/api/Acheteurs");
        }

        public async Task<List<Acheteur>> ListAsync()
        {
            return await _dbContext.Acheteurs
                .Include(a => a.Factures)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> AddAcheteursAsync()
        {
            var currentAcheteurs = await _dbContext.Acheteurs.ToListAsync();

            try
            {
                var response = await _httpClient.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    var allAcheteursDto = await _httpClient.GetFromJsonAsync<List<AcheteurDtoForExternalAPI>>("");

                    if (allAcheteursDto != null)
                    {
                        var newAcheteurs = allAcheteursDto
                            .Where(dto => !currentAcheteurs.Exists(ca => ca.Id == dto.AcheteurID))
                            .Select(dto => new Acheteur(
                                                dto.AcheteurID,
                                                dto.Nom,
                                                dto.Prenom,
                                                dto.Adresse,
                                                dto.Email,
                                                dto.Telephone,
                                                score: 100))
                            .ToList();

                        if (newAcheteurs.Any())
                        {
                            await _dbContext.Acheteurs.AddRangeAsync(newAcheteurs);
                        }
                    }
                    return true;
                }
                else
                {
                    Console.WriteLine("External API call failed. Returning current acheteurs.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while calling the external API: {ex.Message}");
            }

            return false;
        }

        public async Task<Acheteur?> GetByIdAsync(int acheteurID)
        {
            return await _dbContext.Acheteurs
                .Include(a => a.Factures)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == acheteurID);
        }
    }
}
