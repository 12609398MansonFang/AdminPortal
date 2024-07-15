using AdminPortal.Data;
using AdminPortal.Dto;
using AdminPortal.Entities;
using AdminPortal.Mapping;

namespace AdminPortal.Endpoints;

public static class TenantEndpoints
{
    public static RouteGroupBuilder MapTenantEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("tenant");

        //CRUD to get all tenants
        group.MapGet("/", (AdminPortalContext dbContext) =>
        {
            var tenants = dbContext.Tenants.ToList();
            var tenantDto = tenants.Select(tenant => tenant.ToDto());
            return Results.Ok(tenantDto);
        });

        //CRUD to get single tenant
        group.MapGet("/{id}", (int id, AdminPortalContext dbContext) => 
        {
            var tenants = dbContext.Tenants.Where(tenant => tenant.Id == id).ToList();
            var tenantDto = tenants.Select(tenant => tenant.ToDto());
            return Results.Ok(tenantDto);
        });

        //CRUD to create a tenant
        group.MapPost("/", (CreateTenantDto createTenantDto, AdminPortalContext dbContext) =>
        {
            if (createTenantDto == null || string.IsNullOrEmpty(createTenantDto.Name))
            {
                return Results.BadRequest("Invalid message data.");
            };
            var tenant = new Tenant
            {
                Name = createTenantDto.Name
            };

            dbContext.Tenants.Add(tenant);
            dbContext.SaveChanges();

            var tenantDto = tenant.ToDto();
            return Results.Created($"/{tenant.Id}", tenantDto);
        });

        //CRUD to update tenant
        group.MapPut("/{id}", (int id, CreateTenantDto updateTenantDto, AdminPortalContext dbContext) => 
        {
            var tenant = dbContext.Tenants.Find(id);
            if (tenant == null)
            {
                return Results.NotFound();
            }
            if (!string.IsNullOrEmpty(updateTenantDto.Name))
            {
                tenant.Name = updateTenantDto.Name;
            }
            dbContext.Tenants.Update(tenant);
            dbContext.SaveChanges();

            var tenantDto = tenant.ToDto();

            return Results.Ok(tenantDto);
        });

        //CRUD to delete a tenant
        group.MapDelete("/{id}", (int id, AdminPortalContext dbContext) =>
        {
            var tenant = dbContext.Tenants.Find(id);
            if (tenant == null)
            {
                return Results.NotFound();
            }

            dbContext.Tenants.Remove(tenant);
            dbContext.SaveChanges();

            return Results.NoContent();
        });

        return group;
    }
}