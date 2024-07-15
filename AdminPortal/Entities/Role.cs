namespace AdminPortal.Entities;

public class Role
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public int TenantId { get; set; }

    public int[]? Claims { get; set; }
}