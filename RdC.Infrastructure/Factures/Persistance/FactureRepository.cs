using Microsoft.EntityFrameworkCore;
using RdC.Application.Common.Interfaces;
using RdC.Domain.DTO.Facture;
using RdC.Domain.Factures;
using RdC.Infrastructure.Common.Persistance;
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
            return await _dbContext.Factures.FirstOrDefaultAsync(f => f.FactureID == FactureID);
        }

        public async Task<List<Facture>> ListAsync()
        {
            var currentFactures = await _dbContext.Factures.ToListAsync();

            try
            {
                var response = await _httpClient.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    var allFactures = await _httpClient.GetFromJsonAsync<List<Facture>>("");

                    if (allFactures != null)
                    {
                        var newFactures = allFactures
                            .Where(facture => !currentFactures.Exists(cf => cf.FactureID == facture.FactureID))
                            .ToList();

                        newFactures.ForEach(facture =>
                        {
                            if (facture.MontantRestantDue == decimal.Zero)
                                facture.Status = FactureStatus.Payee;
                            else if (facture.MontantRestantDue == facture.MontantTotal)
                                facture.Status = FactureStatus.Impayee;
                            else
                                facture.Status = FactureStatus.PartiellementPayee;
                        });

                        if (newFactures.Any())
                        {
                            await _dbContext.Factures.AddRangeAsync(newFactures);
                            await _dbContext.SaveChangesAsync();

                            currentFactures.AddRange(newFactures);
                        }
                    }
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

            return currentFactures;
        }

        public async Task<Facture?> UpdateAsync(int FactureID, FactureUpdate factureUpdate)
        {
            var facture = await _dbContext.Factures.FindAsync(FactureID);

            if (facture == null)
                return null;

            facture.MontantRestantDue = factureUpdate.MontantRestantDue;
            facture.Status = factureUpdate.Status;

            _dbContext.Factures.Update(facture);

            await _dbContext.SaveChangesAsync();

            return facture;
        }
    }
}
