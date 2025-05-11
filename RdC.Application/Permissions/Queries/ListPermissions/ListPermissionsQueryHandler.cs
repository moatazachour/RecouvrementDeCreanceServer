using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.DTO.Permission;

namespace RdC.Application.Permissions.Queries.ListPermissions
{
    internal sealed class ListPermissionsQueryHandler
        : IRequestHandler<ListPermissionsQuery, List<PermissionDefinitionResponse>>
    {
        private readonly IPermissionDefinitionRepository _permissionDefinitionRepository;

        public ListPermissionsQueryHandler(
            IPermissionDefinitionRepository permissionDefinitionRepository)
        {
            _permissionDefinitionRepository = permissionDefinitionRepository;
        }

        public async Task<List<PermissionDefinitionResponse>> Handle(
            ListPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissionList = await _permissionDefinitionRepository.GetAllAsync();

            var permissionsResponse = permissionList.Select(p => new PermissionDefinitionResponse(
                                                                    p.Id, p.PermissionName)).ToList();

            return permissionsResponse;
        }
    }
}
