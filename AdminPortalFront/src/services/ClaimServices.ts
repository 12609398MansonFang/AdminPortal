import axios from "axios"

import type { Claim, CreateClaim } from '../types/ClaimTypes'
import { API_URL } from './APIService'

export async function getClaims() {
    try {
        const response = await axios.get<Claim[]>(`${API_URL}/claim`)
        return response
    } catch (error) {
        console.error(`Failed Fetching Claims`, error)
        throw new Error(`Failed To Fetch Claims`)
    }
}

export async function getClaim(id: number) {
    try {
        const response = await axios.get<Claim>(`${API_URL}/claim/${id}`)
        return response
    } catch (error) {
        console.error(`Failed Fetching Claim ${id}`, error)
        throw new Error(`Failed To Fetch Claim ${id}`)
    }
}


export async function createClaim(claim: CreateClaim) {
    try {
        const response = await axios.post(`${API_URL}/claim`, claim)
        return response
    } catch (error) {
        console.error(`Error Creating Claim`, error)
        throw new Error(`Failed To Create Claim`)
    }
}

export async function updateClaim(id: number, claim: CreateClaim) {
    try {
        const response = await axios.put(`${API_URL}/claim/${id}`, claim)
        return response
    } catch (error) {
        console.error(`Error Updating Claim ${id}`, error)
        throw new Error(`Failed To Update Claim ${id}`)
    }
}

export async function deleteClaim(id: number) {
    try {
        const response = await axios.delete(`${API_URL}/claim/${id}`)
        return response
    } catch (error) {
        console.error(`Error Deleting Claim ${id}`, error)
        throw new Error(`Failed To Delete Claim ${id}`)
    }
}