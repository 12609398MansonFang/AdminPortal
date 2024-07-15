using AdminPortal.Dto;
using AdminPortal.Entities;

namespace AdminPortal.Mapping;

public static class UserGroupMapping
{
    public static UserGroupDto ToDto(this UserGroup userGroup)
    {
        return new UserGroupDto(userGroup.Id, userGroup.Name, userGroup.TenantId, userGroup.Roles);
    }
}