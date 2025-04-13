using RdC.Application.Common.Interfaces;
using RdC.Domain.Relances;
using RdC.Infrastructure.Common.Persistance;

namespace RdC.Infrastructure.Relances.Persistance.Email
{
    public class EmailRelanceRepository : IEmailRelanceRepository
    {
        private readonly RecouvrementDBContext _dbContext;

        public EmailRelanceRepository(RecouvrementDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(EmailRelance emailRelance)
        {
            await _dbContext.EmailRelances.AddAsync(emailRelance);

            return true;
        }
    }
}
