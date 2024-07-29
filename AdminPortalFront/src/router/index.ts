// src/router/index.ts
import { createRouter, createWebHistory } from 'vue-router'
import Login from '../components/Login.vue'
import Dashboard from '../components/Dashboard.vue'

const routes = [
    { path: '/login', name: 'Login', component: Login },
    { path: '/dashboard', name: 'Dashboard', component: Dashboard, meta: { requiresAuth: true } },
    { path: '/:pathMatch(.*)*', redirect: '/login' }
]

const router = createRouter({
    history: createWebHistory(),
    routes,
})

router.beforeEach((to, _, next) => {
    const isAuthenticated = document.cookie.includes('auth-token')
    if (to.meta.requiresAuth && !isAuthenticated) {
        next({ name: 'Login' })
    } else {
        next()
    }
})

export default router
