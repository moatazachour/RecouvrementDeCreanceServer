using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RdC.Application.Common.Interfaces;
using RdC.Domain.PaiementDates;
using RdC.Infrastructure.Common.Persistance;

namespace RdC.Infrastructure.PaiementDates.Persistance
{
    public class PaiementDateRepository : IPaiementDateRepository
    {
        private readonly RecouvrementDBContext _dbContext;
        private readonly string _connectionString;

        public PaiementDateRepository(
            RecouvrementDBContext dbContext,
            IConfiguration configuration)
        {
            _dbContext = dbContext;

            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found");
        }

        public async Task<bool> AddAsync(List<PaiementDate> paiementDateList)
        {
            await _dbContext.PaiementDates.AddRangeAsync(paiementDateList);

            return true;
        }

        public async Task<PaiementDate?> GetByIdAsync(int DateDeEcheeanceID)
        {
            return await _dbContext.PaiementDates
                .Include(pd => pd.PlanDePaiement)
                .Include(pd => pd.Paiements)
                .FirstOrDefaultAsync(pd => pd.Id == DateDeEcheeanceID);
        }

        public async Task<List<PaiementDate>> GetByPlanIdAsync(int PlanID)
        {
            return await _dbContext.PaiementDates.Where(pd => pd.PlanID == PlanID).ToListAsync();
        }

        public async Task<List<PaiementDate>> GetTodaysAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            return (await _dbContext.PaiementDates
                .Where(pd => !pd.IsPaid && !pd.IsLocked)
                .ToListAsync())
                .Where(pd => pd.EcheanceDate == today)
                .ToList();
        }

        public async Task<List<PaiementDate>> GetAllPreviousPaiementDateAsync(int PlanID)
        {
            return await _dbContext.PaiementDates
                .Where(pd => pd.PlanID == PlanID &&
                            pd.EcheanceDate < DateOnly.FromDateTime(DateTime.Today))
                .ToListAsync();
        }

        public async Task<PaiementDate?> GetPreviousPaiementDateAsync(int currentPaiementDateID)
        {
            PaiementDate? previousPaiementDate = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string query = @"
                    Select *
                    From PaiementDates
                    Where DateEcheanceID = (Select PreviousEcheanceID 
						                    From
						                    (
						                    Select 
							                    LAG(DateEcheanceID, 1) OVER (PARTITION BY PlanID ORDER BY DateEcheanceID) As PreviousEcheanceID,
							                    DateEcheanceID,
							                    LEAD(DateEcheanceID, 1) OVER (PARTITION BY PlanID ORDER BY DateEcheanceID) As NextEcheanceID,
							                    PlanID,
							                    EcheanceDate,
							                    IsPaid,
							                    IsLocked
						                    From PaiementDates
						                    ) R1
					                        Where DateEcheanceID = @CurrentDateEcheanceID)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@CurrentDateEcheanceID", currentPaiementDateID);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                previousPaiementDate = new PaiementDate(
                                    (int)reader["DateEcheanceID"],
                                    (int)reader["PlanID"],
                                    DateOnly.FromDateTime((DateTime)reader["EcheanceDate"]),
                                    Convert.ToDecimal(reader["MontantDeEcheance"]),
                                    Convert.ToDecimal(reader["MontantPayee"]),
                                    Convert.ToDecimal(reader["MontantDue"]),
                                    (bool)reader["IsPaid"],
                                    (bool)reader["IsLocked"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return previousPaiementDate;
        }

        public async Task<bool> UpdateAsync(PaiementDate updatedPaiementDate)
        {
            var existingPaiementDate = await _dbContext.PaiementDates
                .FirstOrDefaultAsync(pd => pd.Id == updatedPaiementDate.Id);

            if (existingPaiementDate == null)
            {
                return false;
            }

            existingPaiementDate.EcheanceDate = updatedPaiementDate.EcheanceDate;
            existingPaiementDate.MontantPayee = updatedPaiementDate.MontantPayee;
            existingPaiementDate.MontantDue = updatedPaiementDate.MontantDue;
            existingPaiementDate.IsPaid = updatedPaiementDate.IsPaid;
            existingPaiementDate.IsLocked = updatedPaiementDate.IsLocked;

            _dbContext.PaiementDates.Update(existingPaiementDate);

            return true;
        }

        public async Task<List<PaiementDate>> GetPaiementDatesByOffsetAsync(int DaysOffset)
        {
            DateOnly targetDate = DateOnly.FromDateTime(DateTime.Today.AddDays(DaysOffset));

            return await _dbContext.PaiementDates
                            .Where(pd => !pd.IsPaid && !pd.IsLocked && pd.EcheanceDate == targetDate)
                            .ToListAsync();
        }
    }
}
