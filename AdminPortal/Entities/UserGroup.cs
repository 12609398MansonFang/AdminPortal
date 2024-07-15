namespace AdminPortal.Entities;

public class UserGroup
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int TenantId { get; set; }

    public int[]? Roles { get; set; }
}