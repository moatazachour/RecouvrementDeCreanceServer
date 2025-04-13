using RdC.Domain.Relances;

namespace RdC.Application.Common.Interfaces
{
    public interface IEmailRelanceRepository
    {
        Task<bool> AddAsync(EmailRelance emailRelance);
    }
}
