using AdminPortal.Dto;
using AdminPortal.Entities;

namespace AdminPortal.Mapping;

public static class TenantMapping
{
    public static TenantDto ToDto(this Tenant tenant)
    {
        return new TenantDto(tenant.Id, tenant.Name);
    }
}