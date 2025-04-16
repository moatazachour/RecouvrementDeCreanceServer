using RdC.Domain.PaiementDates;

namespace RdC.Application.Common.Interfaces
{
    public interface IPaiementDateRepository
    {
        Task<bool> AddAsync(List<PaiementDate> paiementDateList);

        Task<PaiementDate?> GetByIdAsync(int DateDeEcheeanceID);

        Task<List<PaiementDate>> GetByPlanIdAsync(int PlanID);

        Task<List<PaiementDate>> GetTodaysAsync();

        Task<List<PaiementDate>> GetAllPreviousPaiementDateAsync(int PlanID);

        Task<PaiementDate?> GetPreviousPaiementDateAsync(int currentPaiementDateID);

        Task<bool> UpdateAsync(PaiementDate paiementDate);

        Task<List<PaiementDate>> GetPaiementDatesByOffsetAsync(int DaysOffset);
    }
}
