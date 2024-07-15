namespace AdminPortal.Entities;

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public int[]? Tenants { get; set; }

    public int[]? UserGroups { get; set; }
}