using AdminPortal.Dto;
using AdminPortal.Entities;

namespace AdminPortal.Mapping;

public static class ClaimMapping
{
    public static ClaimDto ToDto(this Claim claim)
    {
        return new ClaimDto(claim.Id, claim.Name);
    }
}