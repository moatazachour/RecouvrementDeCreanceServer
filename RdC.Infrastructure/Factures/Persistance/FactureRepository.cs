using Microsoft.EntityFrameworkCore;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Acheteurs;
using RdC.Domain.DTO.Facture;
using RdC.Domain.Factures;
using RdC.Infrastructure.Common.Persistance;
using System.Linq;
using System.Net.Http.Json;

namespace RdC.Infrastructure.Factures.Persistance
{
    public class FactureRepository : IFactureRepository
    {
        private readonly RecouvrementDBContext _dbContext;

        private readonly HttpClient _httpClient;

        public FactureRepository(RecouvrementDBContext dbContext, HttpClient httpClient)
        {
            _dbContext = dbContext;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7285/api/Factures");
        }

        public async Task<Facture?> GetByIdAsync(int FactureID)
        {
            return await _dbContext.Factures.FirstOrDefaultAsync(f => f.Id == FactureID);
        }

        public async Task<bool> AddFacturesAsync()
        {
            var currentFactures = await _dbContext.Factures.ToListAsync();

            try
            {
                var response = await _httpClient.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    var allFacturesDto = await _httpClient.GetFromJsonAsync<List<FactureDtoForExternalAPI>>("");

                    if (allFacturesDto != null)
                    {
                        var newFactures = allFacturesDto
                            .Where(dto => !currentFactures.Exists(cf => cf.Id == dto.FactureID)
                                           && dto.DateDeEcheance < DateOnly.FromDateTime(DateTime.Today))
                            .Select(dto => new Facture(
                                                dto.FactureID,
                                                dto.NumFacture,
                                                dto.DateDeEcheance,
                                                dto.MontantTotal,
                                                dto.MontantRestantDue,
                                                dto.AcheteurID))
                            .ToList();

                        newFactures.ForEach(facture =>
                        {
                            if (facture.MontantRestantDue == decimal.Zero)
                                facture.Status = FactureStatus.PAYEE;
                            else if (facture.MontantRestantDue == facture.MontantTotal)
                                facture.Status = FactureStatus.IMPAYEE;
                            else
                                facture.Status = FactureStatus.PARTIELLEMENT_PAYEE;
                        });

                        if (newFactures.Any())
                        {
                            await _dbContext.Factures.AddRangeAsync(newFactures);
                        }
                    }
                    return true;
                }
                else
                {
                    Console.WriteLine("External API call failed. Returning current factures.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while calling the external API: {ex.Message}");
            }

            return false;
        }

        public async Task<List<Facture>> ListAsync()
        {
            return await _dbContext.Factures
                .Where(f => f.Status != FactureStatus.DUPLIQUE)
                .ToListAsync();
        }

        public async Task<Facture?> UpdateAsync(int FactureID, FactureUpdate factureUpdate)
        {
            var facture = await _dbContext.Factures.FindAsync(FactureID);

            if (facture == null)
                return null;

            facture.MontantRestantDue = factureUpdate.MontantRestantDue;
            facture.Status = factureUpdate.Status;

            _dbContext.Factures.Update(facture);

            return facture;
        }

        public async Task<List<Facture>> GetByIdsAsync(List<int> factureIDs)
        {
            var factures = await _dbContext.Factures.Where(
                f => factureIDs.Contains(f.Id)).ToListAsync();

            return factures;
        }
    }
}
