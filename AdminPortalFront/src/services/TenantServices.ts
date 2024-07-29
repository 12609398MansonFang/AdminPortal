import axios from 'axios'

import type { Tenant, CreateTenant } from '../types/TenantTypes'
import { API_URL } from './APIService'

export async function getTenants() {
    try {
        const response = await axios.get<Tenant[]>(`${API_URL}/tenant`)
        return response
    } catch (error) {
        console.error(`Failed Fetching Tenants`, error)
        throw new Error(`Failed To Fetch Tenants`)
    }
}

export async function getTenant(id: number) {
    try {
        const response = await axios.get<Tenant>(`${API_URL}/tenant/${id}`)
        return response
    } catch (error) {
        console.error(`Failed Fetching Tenant ${id}`, error)
        throw new Error(`Failed To Fetch Tenant ${id}`)
    }
}

export async function createTenant(tenant: CreateTenant) {
    try {
        const response = await axios.post(`${API_URL}/tenant`, tenant)
        return response
    } catch (error) {
        console.error(`Error Creating Tenant`, error)
        throw new Error(`Failed To Create Tenant`)
    }
}

export async function updateTenant(id: number, tenant: CreateTenant) {
    try {
        const response = await axios.put(`${API_URL}/tenant/${id}`, tenant)
        return response
    } catch (error) {
        console.error(`Error Updating Tenant ${id}`, error)
        throw new Error(`Failed To Update Tenant ${id}`)
    }
}

export async function deleteTenant(id: number) {
    try {
        const response = await axios.delete(`${API_URL}/tenant/${id}`)
        return response
    } catch (error) {
        console.error(`Error Deleting Tenant ${id}`, error)
        throw new Error(`Failed To Delete Tenant ${id}`)
    }
}