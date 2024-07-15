using AdminPortal.Data;
using AdminPortal.Dto;
using AdminPortal.Entities;
using AdminPortal.Mapping;

namespace AdminPortal.Endpoints;

public static class UserGroupEndpoints
{
    public static RouteGroupBuilder MapUserGroupEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("usergroup");

        //CRUD to get all usergroups
        group.MapGet("/", (AdminPortalContext dbContext) =>
        {
            var userGroups = dbContext.UserGroups.ToList();
            var userGroupDto = userGroups.Select(userGroup => userGroup.ToDto());
            return Results.Ok(userGroupDto);
        });

        //CRUD to get single usergroup
        group.MapGet("/{id}", (int id, AdminPortalContext dbContext) =>
        {
            var userGroups = dbContext.UserGroups.Where(userGroup => userGroup.Id == id).ToList();
            var userGroupDto = userGroups.Select(userGroup => userGroup.ToDto());
            return Results.Ok(userGroupDto);
        });

        //CRUD to create a usergroup
        group.MapPost("/", (CreateUserGroupDto createUserGroupDto, AdminPortalContext dbContext) =>
        {
            if (createUserGroupDto == null || string.IsNullOrEmpty(createUserGroupDto.Name) || createUserGroupDto.TenantId == default || createUserGroupDto.Roles == null || !createUserGroupDto.Roles.Any())
            {
                return Results.BadRequest("Invalid message data.");
            };
            var userGroup = new UserGroup
            {
                Name = createUserGroupDto.Name,
                TenantId = createUserGroupDto.TenantId,
                Roles = createUserGroupDto.Roles
            };

            dbContext.UserGroups.Add(userGroup);
            dbContext.SaveChanges();

            var userGroupDto = userGroup.ToDto();
            return Results.Created($"/{userGroup.Id}", userGroupDto);
        });

        //CRUD to update a usergroup
        group.MapPut("/{id}", (int id, CreateUserGroupDto updateUserGroupDto, AdminPortalContext dbContext) => {
            var userGroup = dbContext.UserGroups.Find(id);
            if (userGroup == null)
            {
                return Results.NotFound();
            }
            if (updateUserGroupDto == null || string.IsNullOrEmpty(updateUserGroupDto.Name) || updateUserGroupDto.TenantId == default || updateUserGroupDto.Roles == null || !updateUserGroupDto.Roles.Any())
            {
                return Results.BadRequest("Invalid update data.");
            }

            userGroup.Name = updateUserGroupDto.Name;
            userGroup.TenantId = updateUserGroupDto.TenantId;
            userGroup.Roles = updateUserGroupDto.Roles;

            dbContext.UserGroups.Update(userGroup);
            dbContext.SaveChanges();

            var userGroupDto = userGroup.ToDto();

            return Results.Ok(userGroupDto);
        });

        //CRUD to delete a usergroup
        group.MapDelete("/{id}", (int id, AdminPortalContext dbContext) =>
        {
            var userGroup = dbContext.UserGroups.Find(id);
            if (userGroup == null)
            {
                return Results.NotFound();
            }

            dbContext.UserGroups.Remove(userGroup);
            dbContext.SaveChanges();

            return Results.NoContent();
        });

        return group;
    }
}