namespace AdminPortal.Dto;

public record class UserDto(int Id, string? Name, int[]? Tenants, int[]? UserGroups);

public record class CreateUserDto(string? Name, int[]? Tenants, int[]? UserGroups);