using AdminPortal.Dto;
using AdminPortal.Entities;

namespace AdminPortal.Mapping; 

public static class UserMapping
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto(user.Id, user.Name, user.Tenants, user.UserGroups);
    }
}