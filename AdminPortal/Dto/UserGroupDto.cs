namespace AdminPortal.Dto;

public record class UserGroupDto(int Id, string? Name, int TenantId, int[]? Roles);

public record class CreateUserGroupDto(string? Name, int TenantId, int[]? Roles);