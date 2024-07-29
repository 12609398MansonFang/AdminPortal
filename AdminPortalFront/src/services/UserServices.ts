import axios from "axios"

import type { User, CreateUser } from '../types/UserTypes'
import { API_URL } from './APIService'

export async function getUsers() {
    try {
        const response = await axios.get<User[]>(`${API_URL}/user`)
        return response
    } catch (error) {
        console.error(`Failed Fetching Users`, error)
        throw new Error(`Failed To Fetch Users`)
    }
}

export async function getUser(id: number) {
    try {
        const response = await axios.get<User>(`${API_URL}/user/${id}`)
        return response
    } catch (error) {
        console.error(`Failed Fetching User ${id}`, error)
        throw new Error(`Failed To Fetch User ${id}`)
    }
}

export async function createUser(user: CreateUser) {
    try {
        const response = await axios.post(`${API_URL}/user`, user)
        return response
    } catch (error) {
        console.error(`Error Creating User`, error)
        throw new Error(`Failed To Create User`)
    }
}

export async function updateUser(id: number, user: CreateUser) {
    try {
        const response = await axios.put(`${API_URL}/user/${id}`, user)
        return response
    } catch (error) {
        console.error(`Error Updating User ${id}`, error)
        throw new Error(`Failed To Update User ${id}`)
    }
}

export async function deleteUser(id: number) {
    try {
        const response = await axios.delete(`${API_URL}/user/${id}`)
        return response
    } catch (error) {
        console.error(`Error Deleting User ${id}`, error)
        throw new Error(`Failed To Delete User ${id}`)
    }
}