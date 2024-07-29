import axios from "axios"

import type { Role, CreateRole } from '../types/RoleTypes'
import { API_URL } from './APIService'

export async function getRoles() {
    try {
        const response = await axios.get<Role[]>(`${API_URL}/role`)
        return response
    } catch (error) {
        console.error(`Failed Fetching Roles`, error)
        throw new Error(`Failed To Fetch Roles`)
    }
}

export async function getRole(id: number) {
    try {
        const response = await axios.get<Role>(`${API_URL}/role/${id}`)
        return response
    } catch (error) {
        console.error(`Failed Fetching Role ${id}`, error)
        throw new Error(`Failed To Fetch Role ${id}`)
    }
} 

export async function createRole(role: CreateRole) {
    try {
        const response = await axios.post(`${API_URL}/role`, role)
        return response
    } catch (error) {
        console.error(`Error Creating Role`, error)
        throw new Error(`Failed To Create Role`)
    }
}

export async function updateRole(id: number, role: CreateRole) {
    try {
        const response = await axios.put(`${API_URL}/role/${id}`, role)
        return response
    } catch (error) {
        console.error(`Error Updating Role ${id}`, error)
        throw new Error(`Failed To Update Role ${id}`)
    }
}

export async function deleteRole(id: number) {
    try {
        const response = await axios.delete(`${API_URL}/role/${id}`)
        return response
    } catch (error) {
        console.error(`Error Deleting Role ${id}`, error)
        throw new Error(`Failed To Delete Role ${id}`)
    }
}