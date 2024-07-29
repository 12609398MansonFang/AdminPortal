<!-- Login.vue -->
<template>
    <form @submit.prevent="login">
      <label>Username:</label>
      <input v-model="username" required>
      <label>Password:</label>
      <input v-model="password" type="password" required>
      <button type="submit">Login</button>
    </form>
  </template>
  
  <script setup lang="ts">
  import { ref } from 'vue';
  import router from '../router/index'; // Import your router instance
  
  const username = ref('');
  const password = ref('');
  
  const login = async () => {
    // Simulate authentication; replace with your actual authentication logic
    if (username.value === 'admin' && password.value === 'password') {
      // Set auth-token cookie (example)
      document.cookie = 'auth-token=your-auth-token; max-age=3600; path=/'; // Adjust max-age as needed
      router.push('/dashboard'); // Navigate to dashboard after successful login
    } else {
      alert('Invalid credentials');
    }
  };
  </script>
  
