using Microsoft.EntityFrameworkCore;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Acheteurs;
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
            var currentAcheteurs = await _dbContext.Acheteurs.ToListAsync();

            try
            {
                var response = await _httpClient.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    var allAcheteurs = await _httpClient.GetFromJsonAsync<List<Acheteur>>("");

                    if (allAcheteurs != null)
                    {
                        var newAcheteurs = allAcheteurs
                            .Where(acheteur => !currentAcheteurs.Exists(ca => ca.AcheteurID == acheteur.AcheteurID))
                            .ToList();

                        if (newAcheteurs.Any())
                        {
                            await _dbContext.Acheteurs.AddRangeAsync(newAcheteurs);
                            await _dbContext.SaveChangesAsync();

                            currentAcheteurs.AddRange(newAcheteurs);
                        }
                    }
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

            return currentAcheteurs;
        }

        public async Task<Acheteur?> GetByIdAsync(int acheteurID)
        {
            return await _dbContext.Acheteurs.FirstOrDefaultAsync(a => a.AcheteurID == acheteurID);
        }
    }
}
