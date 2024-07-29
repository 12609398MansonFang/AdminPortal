import axios from "axios"

import type { UserGroup, CreateUserGroup } from '../types/UserGroupTypes'
import { API_URL } from './APIService'

export async function getUserGroups() {
    try {
        const response = await axios.get<UserGroup[]>(`${API_URL}/usergroup`)
        return response
    } catch (error) {
        console.error(`Failed Fetching UserGroups`, error)
        throw new Error(`Failed To Fetch UserGroups`)
    }
}

export async function getUserGroup(id: number) {
    try {
        const response = await axios.get<UserGroup>(`${API_URL}/usergroup/${id}`)
        return response
    } catch (error) {
        console.error(`Failed Fetching UserGroup ${id}`, error)
        throw new Error(`Failed To Fetch UserGroup ${id}`)
    }
}

export async function createUserGroup(userGroup: CreateUserGroup) {
    try {
        const response = await axios.post(`${API_URL}/usergroup`, userGroup)
        return response
    } catch (error) {
        console.error(`Error Creating UserGroup`, error)
        throw new Error(`Failed To Create UserGroup`)
    }
}

export async function updateUserGroup(id: number, userGroup: CreateUserGroup) {
    try {
        const response = await axios.put(`${API_URL}/usergroup/${id}`, userGroup)
        return response
    } catch (error) {
        console.error(`Error Updating UserGroup ${id}`, error)
        throw new Error(`Failed To Update UserGroup ${id}`)
    }
}

export async function deleteUserGroup(id: number) {
    try {
        const response = await axios.delete(`${API_URL}/usergroup/${id}`)
        return response
    } catch (error) {
        console.error(`Error Deleting UserGroup ${id}`, error)
        throw new Error(`Failed To Delete UserGroup ${id}`)
    }
}