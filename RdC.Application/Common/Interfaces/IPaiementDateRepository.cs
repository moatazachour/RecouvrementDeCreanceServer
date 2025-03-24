using RdC.Domain.PaiementDates;

namespace RdC.Application.Common.Interfaces
{
    public interface IPaiementDateRepository
    {
        Task<bool> AddAsync(List<PaiementDate> paiementDateList);

        Task<PaiementDate?> GetByIdAsync(int DateDeEcheeanceID);

        Task<List<PaiementDate>> GetByPlanIdAsync(int PlanID);
    }
}
