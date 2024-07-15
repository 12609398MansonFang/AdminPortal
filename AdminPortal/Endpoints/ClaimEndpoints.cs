using AdminPortal.Data;
using AdminPortal.Dto;
using AdminPortal.Entities;
using AdminPortal.Mapping;

namespace AdminPortal.Endpoints;

public static class ClaimEndpoints
{
    public static RouteGroupBuilder MapClaimEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("claim");

        //CRUD to get all claims
        group.MapGet("/", (AdminPortalContext dbContext) =>
        {
            var claims = dbContext.Claims.ToList();
            var claimDto = claims.Select(claim => claim.ToDto());
            return Results.Ok(claimDto);
        });

        //CRUD to get single claim
        group.MapGet("/{id}", (int id, AdminPortalContext dbContext) => 
        {
            var claims = dbContext.Claims.Where(claim => claim.Id == id).ToList();
            var claimDto = claims.Select(claim => claim.ToDto());
            return Results.Ok(claimDto);
        });

        //CRUD to create a claim
        group.MapPost("/", (CreateClaimDto createClaimDto, AdminPortalContext dbContext) => 
        {
            if (createClaimDto == null || string.IsNullOrEmpty(createClaimDto.Name))
            {
                return Results.BadRequest("Invalid message data.");
            };
            var claim = new Claim
            {
                Name = createClaimDto.Name
            };

            dbContext.Claims.Add(claim);
            dbContext.SaveChanges();

            var claimDto = claim.ToDto();
            return Results.Created($"/{claim.Id}", claimDto);
        });

        //CRUD to update a claim
        group.MapPut("/{id}", (int id, CreateClaimDto updateClaimDto, AdminPortalContext dbContext) => {
            var claim = dbContext.Claims.Find(id);
            if (claim == null)
            {
                return Results.NotFound();
            }
            if (!string.IsNullOrEmpty(updateClaimDto.Name))
            {
                claim.Name = updateClaimDto.Name;
            }
            dbContext.Claims.Update(claim);
            dbContext.SaveChanges();

            var claimDto = claim.ToDto();

            return Results.Ok(claimDto);
        });

        //CRUD to delete a claim
        group.MapDelete("/{id}", (int id, AdminPortalContext dbContext) =>
        {
            var claim = dbContext.Claims.Find(id);
            if (claim == null)
            {
                return Results.NotFound();
            }

            dbContext.Claims.Remove(claim);
            dbContext.SaveChanges();

            return Results.NoContent();
        });

        return group;
    }
}
