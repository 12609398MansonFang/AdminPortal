namespace AdminPortal.Dto;

public record class TenantDto(int Id, string? Name);

public record class CreateTenantDto(string? Name);