using Microsoft.EntityFrameworkCore;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Acheteurs;
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


        // here I want to change it that if the server from which I import Factures with this api https://localhost:7285/api/Factures
        // is not running I will return the currentFactures.
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
    }
}
