using RdC.Domain.Relances;

namespace RdC.Application.Common.Interfaces
{
    public interface ISMSRelanceRepository
    {
        Task<bool> AddAsync(SMSRelance smsRelance);
    }
}
