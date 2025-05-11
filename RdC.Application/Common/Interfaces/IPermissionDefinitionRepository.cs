using RdC.Domain.Users;

namespace RdC.Application.Common.Interfaces
{
    public interface IPermissionDefinitionRepository
    {
        Task<List<PermissionDefinition>> GetAllAsync();
    }
}
