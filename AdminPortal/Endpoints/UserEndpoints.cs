using AdminPortal.Data;
using AdminPortal.Dto;
using AdminPortal.Entities;
using AdminPortal.Mapping;

namespace AdminPortal.Endpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("user");

        //CRUD to get all users
        group.MapGet("/", (AdminPortalContext dbContext) =>
        {
            var users = dbContext.Users.ToList();
            var userDto = users.Select(user => user.ToDto());
            return Results.Ok(userDto);
        });

        //CRUD to get single user
        group.MapGet("/{id}", (int id, AdminPortalContext dbContext) =>
        {
            var users = dbContext.Users.Where(user => user.Id == id).ToList();
            var userDto = users.Select(user => user.ToDto());
            return Results.Ok(userDto);
        });

        //CRUD to create a user
        group.MapPost("/", (CreateUserDto createUserDto, AdminPortalContext dbContext) =>
        {
            if (createUserDto == null || string.IsNullOrEmpty(createUserDto.Name) || createUserDto.Tenants == null || !createUserDto.Tenants.Any() || createUserDto.UserGroups == null || !createUserDto.UserGroups.Any())
            {
                return Results.BadRequest("Invalid message data.");
            };
            var user = new User
            {
                Name = createUserDto.Name,
                Tenants = createUserDto.Tenants,
                UserGroups = createUserDto.UserGroups
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var userDto = user.ToDto();
            return Results.Created($"/{user.Id}", userDto);
        });

        //CRUD to update a user
        group.MapPut("/{id}", (int id, CreateUserDto updateUserDto, AdminPortalContext dbContext) => {
            var user = dbContext.Users.Find(id);
            if (user == null)
            {
                return Results.NotFound();
            }
            if (updateUserDto == null || string.IsNullOrEmpty(updateUserDto.Name) || updateUserDto.Tenants == null || !updateUserDto.Tenants.Any() || updateUserDto.UserGroups == null || !updateUserDto.UserGroups.Any())
            {
                return Results.BadRequest("Invalid update data.");
            }

            user.Name = updateUserDto.Name;
            user.Tenants = updateUserDto.Tenants;
            user.UserGroups = updateUserDto.UserGroups;

            dbContext.Users.Update(user);
            dbContext.SaveChanges();

            var userDto = user.ToDto();

            return Results.Ok(userDto);
        });

        //CRUD to delete a user
        group.MapDelete("/{id}", (int id, AdminPortalContext dbContext) =>
        {
            var user = dbContext.Users.Find(id);
            if (user == null)
            {
                return Results.NotFound();
            }

            dbContext.Users.Remove(user);
            dbContext.SaveChanges();

            return Results.NoContent();
        });

        return group;
    }
}