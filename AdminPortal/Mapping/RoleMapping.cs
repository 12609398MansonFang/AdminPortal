using AdminPortal.Dto;
using AdminPortal.Entities;

namespace AdminPortal.Mapping;

public static class RoleMapping
{
    public static RoleDto ToDto(this Role role)
    {
        return new RoleDto(role.Id, role.Name, role.TenantId, role.Claims);
    }
}