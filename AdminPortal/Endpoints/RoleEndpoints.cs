using AdminPortal.Data;
using AdminPortal.Dto;
using AdminPortal.Entities;
using AdminPortal.Mapping;

namespace AdminPortal.Endpoints;

public static class RoleEndpoints
{
    public static RouteGroupBuilder MapRoleEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("role");

        //CRUD to get all roles
        group.MapGet("/", (AdminPortalContext dbContext) =>
        {
            var roles = dbContext.Roles.ToList();
            var roleDto = roles.Select(role => role.ToDto());
            return Results.Ok(roleDto);
        });

        //CRUD to get single role
        group.MapGet("/{id}", (int id, AdminPortalContext dbContext) =>
        {
            var roles = dbContext.Roles.Where(role => role.Id == id).ToList();
            var roleDto = roles.Select(role => role.ToDto());
            return Results.Ok(roleDto);
        });

        //CRUD to create a role
        group.MapPost("/", (CreateRoleDto createRoleDto, AdminPortalContext dbContext) =>
        {
            if (createRoleDto == null || string.IsNullOrEmpty(createRoleDto.Name) || createRoleDto.TenantId == default || createRoleDto.Claims == null || !createRoleDto.Claims.Any())
            {
                return Results.BadRequest("Invalid message data.");
            };
            var role = new Role
            {
                Name = createRoleDto.Name,
                TenantId = createRoleDto.TenantId,
                Claims = createRoleDto.Claims
            };

            dbContext.Roles.Add(role);
            dbContext.SaveChanges();

            var roleDto = role.ToDto();
            return Results.Created($"/{role.Id}", roleDto);
        });

        //CRUD to update a role
        group.MapPut("/{id}", (int id, CreateRoleDto updateRoleDto, AdminPortalContext dbContext) => {
            var role = dbContext.Roles.Find(id);
            if (role == null)
            {
                return Results.NotFound();
            }
            if (updateRoleDto == null || string.IsNullOrEmpty(updateRoleDto.Name) || updateRoleDto.TenantId == default || updateRoleDto.Claims == null || !updateRoleDto.Claims.Any())
            {
                return Results.BadRequest("Invalid update data.");
            }

            role.Name = updateRoleDto.Name;
            role.TenantId = updateRoleDto.TenantId;
            role.Claims = updateRoleDto.Claims;

            dbContext.Roles.Update(role);
            dbContext.SaveChanges();

            var roleDto = role.ToDto();

            return Results.Ok(roleDto);
        });

        //CRUD to delete a role
        group.MapDelete("/{id}", (int id, AdminPortalContext dbContext) =>
        {
            var role = dbContext.Roles.Find(id);
            if (role == null)
            {
                return Results.NotFound();
            }

            dbContext.Roles.Remove(role);
            dbContext.SaveChanges();

            return Results.NoContent();
        });

        return group;
    }
}
