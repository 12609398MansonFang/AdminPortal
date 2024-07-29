export interface UserGroup {
    id: number 
    name:  string
    tenantId: number | null
    roles: number[] | null
}

export interface CreateUserGroup {
    name: string
    tenantId: number | null
    roles: number[] | null
}