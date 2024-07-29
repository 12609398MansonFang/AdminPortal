<script setup lang="ts">
import { ref, onMounted } from 'vue'

import type { Claim, CreateClaim } from '../types/ClaimTypes'
import type { Tenant, CreateTenant } from '../types/TenantTypes'
import type { Role, CreateRole } from '../types/RoleTypes'
import type { UserGroup, CreateUserGroup } from '../types/UserGroupTypes'
import type { User, CreateUser } from '../types/UserTypes'

import { getClaims, getClaim, createClaim, updateClaim, deleteClaim } from '../services/ClaimServices'
import { getTenants, getTenant, createTenant, updateTenant, deleteTenant } from '../services/TenantServices'
import { getRoles, getRole, createRole, updateRole, deleteRole } from '../services/RoleServices'
import { getUserGroups, getUserGroup, createUserGroup, updateUserGroup, deleteUserGroup } from '../services/UserGroupServices'
import { getUsers, getUser, createUser, updateUser, deleteUser } from '../services/UserServices'


const claims = ref<Claim[]>([])
const tenants = ref<Tenant[]>([])
const roles = ref<Role[]>([])
const userGroups = ref<UserGroup[]>([])
const users = ref<User[]>([]) 


//-------------------Get All------------------------------------//
onMounted(async () => {
    try{
        const responseGetClaims = await getClaims()
        const responseGetTenants = await getTenants()
        const responseGetRoles = await getRoles()
        const responseGetUserGroups = await getUserGroups()
        const responseGetUsers = await getUsers()
        claims.value = responseGetClaims.data
        tenants.value = responseGetTenants.data
        roles.value = responseGetRoles.data
        userGroups.value = responseGetUserGroups.data
        users.value = responseGetUsers.data
    } catch (error) {
        console.error('Error during onMounted:', error);
    }
})


//-----------------------Get Single---------------------------------//
const singleClaim = ref<Claim | null>(null)
const inputClaimId = ref<number>(0)
const handleGetClaim = async (id: number) => {
    try {
        const response = await getClaim(id)
        singleClaim.value = response.data
    } catch (error) {
        console.error('Error during getting a claim:', error);
    }
}

const singleTenant = ref<Tenant | null>(null)
const inputTenantId = ref<number>(0)
const handleGetTenant = async (id: number) => {
    try {
        const response = await getTenant(id)
        singleTenant.value = response.data
    } catch (error) {
        console.error('Error during getting a tenant:', error);
    }
}

const singleRole = ref<Role | null>(null)
const inputRoleId = ref<number>(0)
const handleGetRole = async (id: number) => {
    try {
        const response = await getRole(id)
        singleRole.value = response.data
    } catch (error) {
        console.error('Error during getting a role:', error);
    }
}

const singleUserGroup = ref<UserGroup | null>(null)
const inputUserGroupId = ref<number>(0)
const handleGetUserGroup = async (id: number) => {
    try {
        const response = await getUserGroup(id)
        singleUserGroup.value = response.data
    } catch (error) {
        console.error('Error during getting a UserGroup:', error);
    }
}

const singleUser = ref<User | null>(null)
const inputUserId = ref<number>(0)
const handleGetUser = async (id: number) => {
    try {
        const response = await getUser(id)
        singleUser.value = response.data
    } catch (error) {
        console.error('Error during getting a User:', error);
    }
}

//-----------------------Create---------------------------------//
const inputCreateClaim = ref('')
const handleCreateClaim = async() => {
    if (inputCreateClaim.value.trim() !== ''){
        try{
        const claim: CreateClaim = {name: inputCreateClaim.value}
        const response = await createClaim(claim)
        console.log('Claim created Successfully', response) 
        inputCreateClaim.value = ''
        const responseGetClaims = await getClaims()
        claims.value = responseGetClaims.data
        } catch (error){
            console.error(`Error Creating Claim`, error);
        }
    } else {
        alert('Invalid Inputs')
        inputCreateClaim.value = ''
    }
}
//-------
const inputCreateTenant = ref('')
const handleCreateTenant = async() => {
    if (inputCreateTenant.value.trim() !== ''){
        try{
        const tenant: CreateTenant = {name: inputCreateTenant.value}
        const response = await createTenant(tenant)
        console.log('Tenant created Successfully', response) 
        inputCreateTenant.value = ''
        const responseGetTenants = await getTenants()
        tenants.value = responseGetTenants.data
        } catch (error){
            console.error(`Error Creating Tenant`, error);
        }
    } else {
        alert('Invalid Inputs')
        inputCreateTenant.value = ''
    }
}
//-----=--
const inputCreateRoleName = ref('')
const inputCreateRoleTenant = ref<number | null>(null)
const inputCreateRoleClaims = ref<number[]>([])
const handleCreateRoleAddTenant = async(id: number) => {
    if (inputCreateRoleTenant.value == id){
        inputCreateRoleTenant.value = null
        console.log('Unselected', id)
    } else {
        inputCreateRoleTenant.value = id
        console.log('Selected', id)
    }
}
const handleCreateRoleAddClaims = async(id: number) => {
    if (inputCreateRoleClaims.value.includes(id)){
        inputCreateRoleClaims.value = inputCreateRoleClaims.value.filter(Id => Id !== id)
        console.log(inputCreateRoleClaims.value)
    } else {
        inputCreateRoleClaims.value.push(id)
        console.log(inputCreateRoleClaims.value)
    }
}
const handleCreateRole = async() => {
    if (inputCreateRoleName.value.trim() !== '' && inputCreateRoleTenant.value !== null && inputCreateRoleClaims.value.length !== 0){
        try{
        const role: CreateRole = {name: inputCreateRoleName.value, tenantId: inputCreateRoleTenant.value, claims: inputCreateRoleClaims.value}
        const response = await createRole(role)
        console.log('Role created Successfully', response) 
        inputCreateRoleName.value = ''
        inputCreateRoleTenant.value = null
        inputCreateRoleClaims.value = []
        const responseGetRoles = await getRoles()
        roles.value = responseGetRoles.data
        } catch (error){
            console.error(`Error Creating Role`, error);
        }
    } else {
        alert('Invalid Inputs')
        inputCreateRoleName.value = ''
        inputCreateRoleTenant.value = null
        inputCreateRoleClaims.value = []
    }
}
//----=---
const inputCreateUserGroupName = ref('')
const inputCreateUserGroupTenant = ref<number | null>(null)
const inputCreateUserGroupRoles = ref<number[]>([])
const handleCreateUserGroupAddTenant = async(id: number) => {
    if (inputCreateUserGroupTenant.value == id){
        inputCreateUserGroupTenant.value = null
        console.log('Unselected', id)
    } else {
        inputCreateUserGroupTenant.value = id
        console.log('Selected', id)
    }
}
const handleCreateUserGroupAddRoles = async(id: number) => {
    if (inputCreateUserGroupRoles.value.includes(id)){
        inputCreateUserGroupRoles.value = inputCreateUserGroupRoles.value.filter(Id => Id !== id)
        console.log(inputCreateUserGroupRoles.value)
    } else {
        inputCreateUserGroupRoles.value.push(id)
        console.log(inputCreateUserGroupRoles.value)
    }
}
const handleCreateUserGroup = async() => {
    if (inputCreateUserGroupName.value.trim() !== '' && inputCreateUserGroupTenant.value !== null && inputCreateUserGroupRoles.value.length !== 0){
        try{
        const usergroup: CreateUserGroup = {name: inputCreateUserGroupName.value, tenantId: inputCreateUserGroupTenant.value, roles: inputCreateUserGroupRoles.value}
        const response = await createUserGroup(usergroup)
        console.log('Role created Successfully', response) 
        inputCreateUserGroupName.value = ''
        inputCreateUserGroupTenant.value = null
        inputCreateUserGroupRoles.value = []
        const responseGetUserGroups = await getUserGroups()
        userGroups.value = responseGetUserGroups.data
        } catch (error){
            console.error(`Error Creating UserGroup`, error);

        }
    } else {
        alert('Invalid Inputs')
        inputCreateUserGroupName.value = ''
        inputCreateUserGroupTenant.value = null
        inputCreateUserGroupRoles.value = []
    }
}
//----=---
const inputCreateUserName = ref('')
const inputCreateUserTenant = ref<number[]>([])
const inputCreateUserUserGroups = ref<number[]>([])
const handleCreateUserAddTenants = async(id: number) => {
    if (inputCreateUserTenant.value.includes(id)){
        inputCreateUserTenant.value = inputCreateUserTenant.value.filter(Id => Id !== id)
        console.log(inputCreateUserTenant.value)
    } else {
        inputCreateUserTenant.value.push(id)
        console.log(inputCreateUserTenant.value)
    }
}
const handleCreateUserAddUserGroups = async(id: number) => {
    if (inputCreateUserUserGroups.value.includes(id)){
        inputCreateUserUserGroups.value = inputCreateUserUserGroups.value.filter(Id => Id !== id)
        console.log(inputCreateUserUserGroups.value)
    } else {
        inputCreateUserUserGroups.value.push(id)
        console.log(inputCreateUserUserGroups.value)
    }
}
const handleCreateUser = async() => {
    if (inputCreateUserName.value.trim() !== '' && inputCreateUserTenant.value.length !== 0 && inputCreateUserUserGroups.value.length !== 0){
        try{
        const user: CreateUser = {name: inputCreateUserName.value, tenants: inputCreateUserTenant.value, userGroups: inputCreateUserUserGroups.value}
        const response = await createUser(user)
        console.log('Role created Successfully', response) 
        inputCreateUserName.value = ''
        inputCreateUserTenant.value = []
        inputCreateUserUserGroups.value = []
        const responseGetUsers = await getUsers()
        users.value = responseGetUsers.data
        } catch (error){
            console.error(`Error Creating User`, error);

        }
    } else {
        alert('Invalid Inputs')
        inputCreateUserName.value = ''
        inputCreateUserTenant.value = []
        inputCreateUserUserGroups.value = []
    }
}

//-----------------------Update---------------------------------//
//-----------
const inputUpdateClaimId = ref<number | null>(null)
const inputUpdateClaim = ref('')
const handleUpdateClaimId = async(id: number) => {
    if (inputUpdateClaimId.value == id){
        inputUpdateClaimId.value = null
        console.log('Unselected', id)
    } else {
        inputUpdateClaimId.value = id
        console.log('Selected', id)
    }
}
const handleUpdateClaim = async() => {
    if (inputUpdateClaimId.value !== null && inputUpdateClaim.value.trim() !== ''){
        try{
        const claim: CreateClaim = {name: inputUpdateClaim.value}
        const response = await updateClaim(inputUpdateClaimId.value, claim)
        console.log('Claim created Successfully', response) 
        inputUpdateClaim.value = ''
        inputUpdateClaimId.value = null
        const responseGetClaims = await getClaims()
        claims.value = responseGetClaims.data
        } catch (error){
            console.error(`Error Updating Claim`, error);
        }
    } else {
        alert('Invalid Inputs')
        inputUpdateClaim.value = ''
        inputUpdateClaimId.value = null
    }
}
//-----------------
const inputUpdateTenantId = ref<number | null>(null)
const inputUpdateTenant = ref('')
const handleUpdateTenantId = async(id: number) => {
    if (inputUpdateTenantId.value == id){
        inputUpdateTenantId.value = null
        console.log('Unselected', id)
    } else {
        inputUpdateTenantId.value = id
        console.log('Selected', id)
    }
}
const handleUpdateTenant = async() => {
    if (inputUpdateTenantId.value !== null && inputUpdateTenant.value.trim() !== ''){
        try{
        const tenant: CreateTenant = {name: inputUpdateTenant.value}
        const response = await updateTenant(inputUpdateTenantId.value, tenant)
        console.log('Tenant created Successfully', response) 
        inputUpdateTenant.value = ''
        inputUpdateTenantId.value = null
        const responseGetTenants = await getTenants()
        tenants.value = responseGetTenants.data
        } catch (error){
            console.error(`Error Updating Tenant`, error);
        }
    } else {
        alert('Invalid Inputs')
        inputUpdateTenant.value = ''
        inputUpdateTenantId.value = null
    }
}
//-----=--
const inputUpdateRoleId = ref<number | null>(null)
const inputUpdateRoleName = ref('')
const inputUpdateRoleTenant = ref<number | null>(null)
const inputUpdateRoleClaims = ref<number[]>([])
const handleUpdateRoleId = async(id: number) => {
    if (inputUpdateRoleId.value == id){
        inputUpdateRoleId.value = null
        console.log('Unselected', id)
    } else {
        inputUpdateRoleId.value = id
        console.log('Selected', id)
    }
}
const handleUpdateRoleAddTenant = async(id: number) => {
    if (inputUpdateRoleTenant.value == id){
        inputUpdateRoleTenant.value = null
        console.log('Unselected', id)
    } else {
        inputUpdateRoleTenant.value = id
        console.log('Selected', id)
    }
}
const handleUpdateRoleAddClaims = async(id: number) => {
    if (inputUpdateRoleClaims.value.includes(id)){
        inputUpdateRoleClaims.value = inputUpdateRoleClaims.value.filter(Id => Id !== id)
        console.log(inputUpdateRoleClaims.value)
    } else {
        inputUpdateRoleClaims.value.push(id)
        console.log(inputUpdateRoleClaims.value)
    }
}
const handleUpdateRole = async() => {
    if (inputUpdateRoleId.value !== null && inputUpdateRoleName.value.trim() !== '' && inputUpdateRoleTenant.value !== null && inputUpdateRoleClaims.value.length !== 0){
        try{
        const role: CreateRole = {name: inputUpdateRoleName.value, tenantId: inputUpdateRoleTenant.value, claims: inputUpdateRoleClaims.value}
        const response = await updateRole(inputUpdateRoleId.value, role)
        console.log('Role Updated Successfully', response) 
        inputUpdateRoleId.value = null
        inputUpdateRoleName.value = ''
        inputUpdateRoleTenant.value = null
        inputUpdateRoleClaims.value = []
        const responseGetRoles = await getRoles()
        roles.value = responseGetRoles.data
        } catch (error){
            console.error(`Error Creating Role`, error);
        }
    } else {
        alert('Invalid Inputs')
        inputUpdateRoleId.value = null
        inputUpdateRoleName.value = ''
        inputUpdateRoleTenant.value = null
        inputUpdateRoleClaims.value = []
    }
}
//-----------
const inputUpdateUserGroupId = ref<number | null>(null)
const inputUpdateUserGroupName = ref('')
const inputUpdateUserGroupTenant = ref<number | null>(null)
const inputUpdateUserGroupRoles = ref<number[]>([])
const handleUpdateUserGroupId = async(id: number) => {
    if (inputUpdateUserGroupId.value == id){
        inputUpdateUserGroupId.value = null
        console.log('Unselected', id)
    } else {
        inputUpdateUserGroupId.value = id
        console.log('Selected', id)
    }
}
const handleUpdateUserGroupAddTenant = async(id: number) => {
    if (inputUpdateUserGroupTenant.value == id){
        inputUpdateUserGroupTenant.value = null
        console.log('Unselected', id)
    } else {
        inputUpdateUserGroupTenant.value = id
        console.log('Selected', id)
    }
}
const handleUpdateUserGroupAddRoles = async(id: number) => {
    if (inputUpdateUserGroupRoles.value.includes(id)){
        inputUpdateUserGroupRoles.value = inputUpdateUserGroupRoles.value.filter(Id => Id !== id)
        console.log(inputUpdateUserGroupRoles.value)
    } else {
        inputUpdateUserGroupRoles.value.push(id)
        console.log(inputUpdateUserGroupRoles.value)
    }
}
const handleUpdateUserGroup = async() => {
    if (inputUpdateUserGroupId.value !== null && inputUpdateUserGroupName.value.trim() !== '' && inputUpdateUserGroupTenant.value !== null && inputUpdateUserGroupRoles.value.length !== 0){
        try{
        const usergroup: CreateUserGroup = {name: inputUpdateUserGroupName.value, tenantId: inputUpdateUserGroupTenant.value, roles: inputUpdateUserGroupRoles.value}
        const response = await updateUserGroup(inputUpdateUserGroupId.value, usergroup)
        console.log('UserGroup Updated Successfully', response) 
        inputUpdateUserGroupId.value = null
        inputUpdateUserGroupName.value = ''
        inputUpdateUserGroupTenant.value = null
        inputUpdateUserGroupRoles.value = []
        const responseGetUserGroups = await getUserGroups()
        userGroups.value = responseGetUserGroups.data
        } catch (error){
            console.error(`Error Updating UserGroup`, error);

        }
    } else {
        alert('Invalid Inputs')
        inputUpdateUserGroupId.value = null
        inputUpdateUserGroupName.value = ''
        inputUpdateUserGroupTenant.value = null
        inputUpdateUserGroupRoles.value = []
    }
}
//-----------
const inputUpdateUserId = ref<number | null>(null)
const inputUpdateUserName = ref('')
const inputUpdateUserTenant = ref<number[]>([])
const inputUpdateUserUserGroups = ref<number[]>([])
const handleUpdateUserId = async(id: number) => {
    if (inputUpdateUserId.value == id){
        inputUpdateUserId.value = null
        console.log('Unselected', id)
    } else {
        inputUpdateUserId.value = id
        console.log('Selected', id)
    }
}
const handleUpdateUserAddTenants = async(id: number) => {
    if (inputUpdateUserTenant.value.includes(id)){
        inputUpdateUserTenant.value = inputUpdateUserTenant.value.filter(Id => Id !== id)
        console.log(inputUpdateUserTenant.value)
    } else {
        inputUpdateUserTenant.value.push(id)
        console.log(inputUpdateUserTenant.value)
    }
}
const handleUpdateUserAddUserGroups = async(id: number) => {
    if (inputUpdateUserUserGroups.value.includes(id)){
        inputUpdateUserUserGroups.value = inputUpdateUserUserGroups.value.filter(Id => Id !== id)
        console.log(inputUpdateUserUserGroups.value)
    } else {
        inputUpdateUserUserGroups.value.push(id)
        console.log(inputUpdateUserUserGroups.value)
    }
}
const handleUpdateUser = async() => {
    if (inputUpdateUserId.value !== null && inputUpdateUserName.value.trim() !== '' && inputUpdateUserTenant.value.length !== 0 && inputUpdateUserUserGroups.value.length !== 0){
        try{
        const user: CreateUser = {name: inputUpdateUserName.value, tenants: inputUpdateUserTenant.value, userGroups: inputUpdateUserUserGroups.value}
        const response = await updateUser(inputUpdateUserId.value, user)
        console.log('User Updated', response) 
        inputUpdateUserId.value = null
        inputUpdateUserName.value = ''
        inputUpdateUserTenant.value = []
        inputUpdateUserUserGroups.value = []
        const responseGetUsers = await getUsers()
        users.value = responseGetUsers.data
        } catch (error){
            console.error(`Error Updating User`, error);
        }
    } else {
        alert('Invalid Inputs')
        inputUpdateUserId.value = null
        inputUpdateUserName.value = ''
        inputUpdateUserTenant.value = []
        inputUpdateUserUserGroups.value = []
    }
}

//-------------------Delete-----------------//
//------
const inputDeleteClaimId = ref<number | null>(null)
const handleDeleteClaimId = async(id: number) => {
    if (inputDeleteClaimId.value == id){
        inputDeleteClaimId.value = null
        console.log('Unselected', id)
    } else {
        inputDeleteClaimId.value = id
        console.log('Selected', id)
    }
}
const handleDeleteClaim = async() => {
    if (inputDeleteClaimId.value !== null){
        try {
            const response = await deleteClaim(inputDeleteClaimId.value)
            inputDeleteClaimId.value = null
            const responseGetClaims = await getClaims()
            claims.value = responseGetClaims.data
        } catch (error){
            console.error(`Error Deleting Claims`, error);
        }
    } else {
        inputDeleteClaimId.value = null
    }
}
//----------
const inputDeleteTenantId = ref<number | null>(null)
const handleDeleteTenantId = async(id: number) => {
    if (inputDeleteTenantId.value == id){
        inputDeleteTenantId.value = null
        console.log('Unselected', id)
    } else {
        inputDeleteTenantId.value = id
        console.log('Selected', id)
    }
}
const handleDeleteTenant = async() => {
    if (inputDeleteTenantId.value !== null){
        try {
            const response = await deleteTenant(inputDeleteTenantId.value)
            inputDeleteTenantId.value = null
            const responseGetTenants = await getTenants()
            tenants.value = responseGetTenants.data
        } catch (error){
            console.error(`Error Deleting Roles`, error);
        }
    } else {
        inputDeleteTenantId.value = null
    }
}
//----------
const inputDeleteRoleId = ref<number | null>(null)
const handleDeleteRoleId = async(id: number) => {
    if (inputDeleteRoleId.value == id){
        inputDeleteRoleId.value = null
        console.log('Unselected', id)
    } else {
        inputDeleteRoleId.value = id
        console.log('Selected', id)
    }
}
const handleDeleteRole = async() => {
    if (inputDeleteRoleId.value !== null){
        try {
            const response = await deleteRole(inputDeleteRoleId.value)
            inputDeleteRoleId.value = null
            const responseGetRoles = await getRoles()
            roles.value = responseGetRoles.data
        } catch (error){
            console.error(`Error Deleting Roles`, error);
        }
    } else {
        inputDeleteRoleId.value = null
    }
}
//----------
const inputDeleteUserGroupId = ref<number | null>(null)
const handleDeleteUserGroupId = async(id: number) => {
    if (inputDeleteUserGroupId.value == id){
        inputDeleteUserGroupId.value = null
        console.log('Unselected', id)
    } else {
        inputDeleteUserGroupId.value = id
        console.log('Selected', id)
    }
}
const handleDeleteUserGroup = async() => {
    if (inputDeleteUserGroupId.value !== null){
        try {
            const response = await deleteUserGroup(inputDeleteUserGroupId.value)
            inputDeleteUserGroupId.value = null
            const responseGetUserGroups = await getUserGroups()
            userGroups.value = responseGetUserGroups.data
        } catch (error){
            console.error(`Error Deleting UserGroup`, error);
        }
    } else {
        inputDeleteUserGroupId.value = null
    }
}
//----------
const inputDeleteUserId = ref<number | null>(null)
const handleDeleteUserId = async(id: number) => {
    if (inputDeleteUserId.value == id){
        inputDeleteUserId.value = null
        console.log('Unselected', id)
    } else {
        inputDeleteUserId.value = id
        console.log('Selected', id)
    }
}
const handleDeleteUser = async() => {
    if (inputDeleteUserId.value !== null){
        try {
            const response = await deleteUser(inputDeleteUserId.value)
            inputDeleteUserId.value = null
            const responseGetUsers = await getUsers()
            users.value = responseGetUsers.data
        } catch (error){
            console.error(`Error Deleting User`, error);
        }
    } else {
        inputDeleteUserId.value = null
    }
}

</script>

<template>
    <div class="w-full h-full flex space-x-2">

            <div class="ClaimsContainer w-[20%] bg-slate-100 space-y-2">
                <h1>Claims</h1>

                <div>
                    <p class="font-bold ">All Claims</p>
                    <div v-for="claim in claims" :key="claim.id">
                        <p>{{ claim }}</p>
                    </div>                    
                </div>

                <div>
                    <p class="font-bold ">Get a Claim</p>
                    <input placeholder="input id" v-model="inputClaimId">
                    <button class="shadow-lg" @click="handleGetClaim(inputClaimId)">Click</button>
                    <p>Claim:</p>
                    <p>{{ singleClaim }}</p>
                </div>

                <div>
                    <p class="font-bold ">Create a Claim</p>
                    <input v-model="inputCreateClaim" placeholder="Input new claim">
                    <button class="shadow-lg" @click="handleCreateClaim()">Click</button>
                </div>

                <div>
                    <p class="font-bold ">Update a Claim</p>
                    <div v-for="claim in claims" :key="claim.id">
                        <button class="font-light bg-red-200" v-if="inputUpdateClaimId == claim.id " @click="handleUpdateClaimId(claim.id)">{{ claim.name }}</button>
                        <button class="font-light" v-else @click="handleUpdateClaimId(claim.id)">{{ claim.name }}</button>
                    </div>
                    <input v-model="inputUpdateClaim" placeholder="Input Update Claim">
                    <button class="shadow-lg" @click="handleUpdateClaim()">Click</button>
                </div>

                <div>
                    <p class="font-bold ">Delete A Claims</p>
                    <p>Select A Claim</p>
                    <div v-for="claim in claims" :key="claim.id">
                        <button class="font-light bg-red-200" v-if="inputDeleteClaimId == claim.id " @click="handleDeleteClaimId(claim.id)">{{ claim.name }}</button>
                        <button class="font-light" v-else @click="handleDeleteClaimId(claim.id)">{{ claim.name }}</button>
                    </div>
                    <button class="shadow-lg" @click="handleDeleteClaim">Delete</button>
                </div>
                
            </div>

            <div class="TenantsContainer w-[20%] bg-slate-100 space-y-2">
                <h1>Tenants</h1>

                <div>
                    <p class="font-bold ">All Tenants</p>
                    <div v-for="tenant in tenants" :key="tenant.id">
                        <p>{{ tenant }}</p>
                    </div>
                </div>

                <div>
                    <p class="font-bold ">Get a Tenant</p>
                    <input placeholder="input id" v-model="inputTenantId">
                    <button class="shadow-lg" @click="handleGetTenant(inputTenantId)">Click</button>
                    <p>Tenant:</p>
                    <p>{{ singleTenant }}</p>
                </div>

                <div>
                    <p class="font-bold ">Create a Tenant</p>
                    <input v-model="inputCreateTenant" placeholder="Input new tenant">
                    <button class="shadow-lg" @click="handleCreateTenant()">Click</button>
                </div>

                <div>
                    <p class="font-bold ">Update a Tenant</p>
                    <div v-for="tenant in tenants" :key="tenant.id">
                        <button class="font-light bg-red-200" v-if="inputUpdateTenantId == tenant.id " @click="handleUpdateTenantId(tenant.id)">{{ tenant.name }}</button>
                        <button class="font-light" v-else @click="handleUpdateTenantId(tenant.id)">{{ tenant.name }}</button>
                    </div>
                    <input v-model="inputUpdateTenant" placeholder="Input Update tenant">
                    <button class="shadow-lg" @click="handleUpdateTenant()">Click</button>
                </div>

                <div>
                    <p class="font-bold ">Delete A Tenant</p>
                    <p>Select A Tenant</p>
                    <div v-for="tenant in tenants" :key="tenant.id">
                        <button class="font-light bg-red-200" v-if="inputDeleteTenantId == tenant.id " @click="handleDeleteTenantId(tenant.id)">{{ tenant.name }}</button>
                        <button class="font-light" v-else @click="handleDeleteTenantId(tenant.id)">{{ tenant.name }}</button>
                    </div>
                    <button class="shadow-lg" @click="handleDeleteTenant">Delete</button>
                </div>

            </div>

            <div class="RolesContainer w-[20%] bg-slate-100 space-y-2">
                <h1>Roles</h1>

                <div>
                    <p class="font-bold ">All Roles</p>
                    <div v-for="role in roles" :key="role.id">
                        <p>{{ role }}</p>
                    </div>
                </div>

                <div>
                    <p class="font-bold ">Get a Role</p>
                    <input placeholder="input id" v-model="inputRoleId">
                    <button class="shadow-lg" @click="handleGetRole(inputRoleId)">Click</button>
                    <p>Role:</p>
                    <p>{{ singleRole }}</p>
                </div>

                <div>
                    <p class="font-bold ">Create a Role</p>
                    <input v-model="inputCreateRoleName">
                    <p>Select a Tenant</p>
                    <div v-for="tenant in tenants" :key="tenant.id">
                        <button class="font-light bg-red-200" v-if="inputCreateRoleTenant == tenant.id" @click="handleCreateRoleAddTenant(tenant.id)">{{ tenant.name }}</button>
                        <button class="font-light" v-else @click="handleCreateRoleAddTenant(tenant.id)">{{ tenant.name }}</button>
                    </div>
                    <p>Select From Claims</p>
                    <div v-for="claim in claims" :key="claim.id">
                        <button class="font-light bg-red-200" v-if="inputCreateRoleClaims.includes(claim.id)" @click="handleCreateRoleAddClaims(claim.id)">{{ claim.name }}</button>
                        <button class="font-light" v-else @click="handleCreateRoleAddClaims(claim.id)">{{ claim.name }}</button>
                    </div>

                    <button class="shadow-lg" @click="handleCreateRole">Click</button>
                </div>

                <div>
                    <p class="font-bold ">Update a Role</p>
                    <div v-for="role in roles" :key="role.id">
                        <button class="font-light bg-red-200" v-if="inputUpdateRoleId == role.id " @click="handleUpdateRoleId(role.id)">{{ role.name }}</button>
                        <button class="font-light" v-else @click="handleUpdateRoleId(role.id)">{{ role.name }}</button>
                    </div>
                    <input v-model="inputUpdateRoleName">
                    <p>Select a Tenant</p>
                    <div v-for="tenant in tenants" :key="tenant.id">
                        <button class="font-light bg-red-200" v-if="inputUpdateRoleTenant == tenant.id" @click="handleUpdateRoleAddTenant(tenant.id)">{{ tenant.name }}</button>
                        <button class="font-light" v-else @click="handleUpdateRoleAddTenant(tenant.id)">{{ tenant.name }}</button>
                    </div>
                    <p>Select From Claims</p>
                    <div v-for="claim in claims" :key="claim.id">
                        <button class="font-light bg-red-200" v-if="inputUpdateRoleClaims.includes(claim.id)" @click="handleUpdateRoleAddClaims(claim.id)">{{ claim.name }}</button>
                        <button class="font-light" v-else @click="handleUpdateRoleAddClaims(claim.id)">{{ claim.name }}</button>
                    </div>

                    <button class="shadow-lg" @click="handleUpdateRole">Click</button>
                </div>

                <div>
                    <p class="font-bold ">Delete A Role</p>
                    <p>Select A Role</p>
                    <div v-for="role in roles" :key="role.id">
                        <button class="font-light bg-red-200" v-if="inputDeleteRoleId == role.id " @click="handleDeleteRoleId(role.id)">{{ role.name }}</button>
                        <button class="font-light" v-else @click="handleDeleteRoleId(role.id)">{{ role.name }}</button>
                    </div>
                    <button class="shadow-lg" @click="handleDeleteRole">Delete</button>
                </div>

            </div>

            <div class="UserGroupsContainer w-[20%] bg-slate-100 space-y-2">
                <h1>UserGroups</h1>

                <div>
                    <p class="font-bold ">All UserGroups</p>
                    <div v-for="userGroup in userGroups" :key="userGroup.id">
                        <p>{{ userGroup }}</p>
                    </div>
                </div>

                <div>
                    <p class="font-bold ">Get a UserGroup</p>
                    <input placeholder="input id" v-model="inputUserGroupId">
                    <button class="shadow-lg" @click="handleGetUserGroup(inputUserGroupId)">Get</button>
                    <p>UserGroup:</p>
                    <p>{{ singleUserGroup }}</p>
                </div>

                <div>
                    <p class="font-bold ">Create a UserGroup</p>
                    <input v-model="inputCreateUserGroupName">
                    <p>Select a Tenant</p>
                    <div v-for="tenant in tenants" :key="tenant.id">
                        <button class="font-light bg-red-200" v-if="inputCreateUserGroupTenant == tenant.id" @click="handleCreateUserGroupAddTenant(tenant.id)">{{ tenant.name }}</button>
                        <button class="font-light" v-else @click="handleCreateUserGroupAddTenant(tenant.id)">{{ tenant.name }}</button>
                    </div>
                    <p>Select From Roles</p>
                    <div v-for="role in roles" :key="role.id">
                        <button class="font-light bg-red-200" v-if="inputCreateUserGroupRoles.includes(role.id)" @click="handleCreateUserGroupAddRoles(role.id)">{{ role.name }}</button>
                        <button class="font-light" v-else @click="handleCreateUserGroupAddRoles(role.id)">{{ role.name }}</button>
                    </div>

                    <button class="shadow-lg" @click="handleCreateUserGroup">Create</button>
                </div>

                <div>
                    <p class="font-bold ">Update a UserGroup</p>
                    <p>Select A UserGroup</p>
                    <div v-for="userGroup in userGroups" :key="userGroup.id">
                        <button class="font-light bg-red-200" v-if="inputUpdateUserGroupId == userGroup.id " @click="handleUpdateUserGroupId(userGroup.id)">{{ userGroup.name }}</button>
                        <button class="font-light" v-else @click="handleUpdateUserGroupId(userGroup.id)">{{ userGroup.name }}</button>
                    </div>
                    <input v-model="inputUpdateUserGroupName">
                    <p>Select a Tenant</p>
                    <div v-for="tenant in tenants" :key="tenant.id">
                        <button class="font-light bg-red-200" v-if="inputUpdateUserGroupTenant == tenant.id" @click="handleUpdateUserGroupAddTenant(tenant.id)">{{ tenant.name }}</button>
                        <button class="font-light" v-else @click="handleUpdateUserGroupAddTenant(tenant.id)">{{ tenant.name }}</button>
                    </div>
                    <p>Select From Roles</p>
                    <div v-for="role in roles" :key="role.id">
                        <button class="font-light bg-red-200" v-if="inputUpdateUserGroupRoles.includes(role.id)" @click="handleUpdateUserGroupAddRoles(role.id)">{{ role.name }}</button>
                        <button class="font-light" v-else @click="handleUpdateUserGroupAddRoles(role.id)">{{ role.name }}</button>
                    </div>

                    <button class="shadow-lg" @click="handleUpdateUserGroup">Update</button>
                </div>

                <div>
                    <p class="font-bold ">Delete A UserGroup</p>
                    <p>Select A UserGroup</p>
                    <div v-for="userGroup in userGroups" :key="userGroup.id">
                        <button class="font-light bg-red-200" v-if="inputDeleteUserGroupId == userGroup.id " @click="handleDeleteUserGroupId(userGroup.id)">{{ userGroup.name }}</button>
                        <button class="font-light" v-else @click="handleDeleteUserGroupId(userGroup.id)">{{ userGroup.name }}</button>
                    </div>
                    <button class="shadow-lg" @click="handleDeleteUserGroup">Delete</button>
                </div>


            </div>

            <div class="UsersContainer w-[20%] bg-slate-100 space-y-2">
                <h1>Users</h1>

                <div>
                    <p class="font-bold ">All Users</p>
                    <div v-for="user in users" :key="user.id">
                        <p>{{ user }}</p>
                    </div>
                </div>

                <div>
                    <p class="font-bold ">Get a User</p>
                    <input placeholder="input id" v-model="inputUserId">
                    <button class="shadow-lg" @click="handleGetUser(inputUserId)">Get</button>
                    <p>User:</p>
                    <p>{{ singleUser }}</p>
                </div>

                <div>
                    <p class="font-bold ">Create a User</p>
                    <input v-model="inputCreateUserName">
                    <p>Select From Tenants</p>
                    <div v-for="tenant in tenants" :key="tenant.id">
                        <button class="font-light bg-red-200" v-if="inputCreateUserTenant.includes(tenant.id)" @click="handleCreateUserAddTenants(tenant.id)">{{ tenant.name }}</button>
                        <button class="font-light" v-else @click="handleCreateUserAddTenants(tenant.id)">{{ tenant.name }}</button>
                    </div>
                    <p>Select From UserGroup</p>
                    <div v-for="userGroup in userGroups" :key="userGroup.id">
                        <button class="font-light bg-red-200" v-if="inputCreateUserUserGroups.includes(userGroup.id)" @click="handleCreateUserAddUserGroups(userGroup.id)">{{ userGroup.name }}</button>
                        <button class="font-light" v-else @click="handleCreateUserAddUserGroups(userGroup.id)">{{ userGroup.name }}</button>
                    </div>

                    <button class="shadow-lg" @click="handleCreateUser">Create</button>
                </div>

                <div>
                    <p class="font-bold ">Update A User</p>
                    <p>Select A User</p>
                    <div v-for="user in users" :key="user.id">
                        <button class="font-light bg-red-200" v-if="inputUpdateUserId == user.id " @click="handleUpdateUserId(user.id)">{{ user.name }}</button>
                        <button class="font-light" v-else @click="handleUpdateUserId(user.id)">{{ user.name }}</button>
                    </div>
                    <input v-model="inputUpdateUserName" placeholder="update name">
                    <p>Select From Tenants</p>
                    <div v-for="tenant in tenants" :key="tenant.id">
                        <button class="font-light bg-red-200" v-if="inputUpdateUserTenant.includes(tenant.id)" @click="handleUpdateUserAddTenants(tenant.id)">{{ tenant.name }}</button>
                        <button class="font-light" v-else @click="handleUpdateUserAddTenants(tenant.id)">{{ tenant.name }}</button>
                    </div>
                    <p>Select From UserGroup</p>
                    <div v-for="userGroup in userGroups" :key="userGroup.id">
                        <button class="font-light bg-red-200" v-if="inputUpdateUserUserGroups.includes(userGroup.id)" @click="handleUpdateUserAddUserGroups(userGroup.id)">{{ userGroup.name }}</button>
                        <button class="font-light" v-else @click="handleUpdateUserAddUserGroups(userGroup.id)">{{ userGroup.name }}</button>
                    </div>

                    <button class="shadow-lg" @click="handleUpdateUser">Update</button>
                </div>

                <div>
                    <p class="font-bold ">Delete A User</p>
                    <p>Select A User</p>
                    <div v-for="user in users" :key="user.id">
                        <button class="font-light bg-red-200" v-if="inputDeleteUserId == user.id " @click="handleDeleteUserId(user.id)">{{ user.name }}</button>
                        <button class="font-light" v-else @click="handleDeleteUserId(user.id)">{{ user.name }}</button>
                    </div>
                    <button class="shadow-lg" @click="handleDeleteUser">Delete</button>
                </div>
            </div>

    </div>
</template>


