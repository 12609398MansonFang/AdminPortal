export interface Role {
    id: number
    name: string
    tenantId: number | null
    claims: number[] | null
}

export interface CreateRole {
    name: string
    tenantId: number | null
    claims: number[] | null
}