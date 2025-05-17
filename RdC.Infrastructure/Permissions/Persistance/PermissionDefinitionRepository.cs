using Microsoft.EntityFrameworkCore;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Users;
using RdC.Infrastructure.Common.Persistance;

namespace RdC.Infrastructure.Permissions.Persistance
{
    public class PermissionDefinitionRepository : IPermissionDefinitionRepository
    {
        private readonly RecouvrementDBContext _dbContext;

        public PermissionDefinitionRepository(RecouvrementDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<PermissionDefinition>> GetAllAsync()
        {
            return _dbContext.PermissionDefinitions.ToListAsync();
        }
    }
}
