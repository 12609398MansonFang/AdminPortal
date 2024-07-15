namespace AdminPortal.Dto;

public record class RoleDto(int Id, string? Name, int TenantId, int[]? Claims);

public record class CreateRoleDto(string? Name, int TenantId, int[]? Claims);