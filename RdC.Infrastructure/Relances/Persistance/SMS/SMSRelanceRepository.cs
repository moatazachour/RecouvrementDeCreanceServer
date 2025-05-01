using RdC.Application.Common.Interfaces;
using RdC.Domain.Relances;
using RdC.Infrastructure.Common.Persistance;

namespace RdC.Infrastructure.Relances.Persistance.SMS
{
    public class SMSRelanceRepository : ISMSRelanceRepository
    {
        private readonly RecouvrementDBContext _dbContext;

        public SMSRelanceRepository(RecouvrementDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(SMSRelance smsRelance)
        {
            await _dbContext.SMSRelances.AddAsync(smsRelance);

            return true;
        }
    }
}