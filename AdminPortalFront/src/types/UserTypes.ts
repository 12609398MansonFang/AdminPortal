export interface User {
    id: number 
    name: string
    tenants: number[] | null
    userGroups: number[] | null
}

export interface CreateUser {
    name: string
    tenants: number[] | null
    userGroups: number[] | null
}