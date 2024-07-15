namespace AdminPortal.Dto;

public record class ClaimDto(int Id, string? Name);

public record class CreateClaimDto(string? Name);