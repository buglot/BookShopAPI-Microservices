<template>
  <div class="flex flex-1 h-full justify-center items-center p-2">
    <div class=" w-3/4 md:w-1/4 h-full bg-green-500 flex flex-col p-2 text-black rounded-xl gap-5">
      <h1 class=" text-3xl font-bold">Login</h1>
      <h2 v-if="msg" class=" text-2xl text-red-600">{{ msg }}</h2>
      <form class="w-full gap-5 flex flex-col" @submit.prevent="LoginAuth">
        <input ref="email" type="email" class=" p-2 w-full bg-white text-black rounded-xl" placeholder="Email"
          required />
        <input ref="pass" type="password" class=" p-2 w-full bg-white text-black rounded-xl" placeholder="Password"
          required />
        <label>
          <input ref="checkremember" type="checkbox" class="accent-red-600 w-4 h-4">
          Remember
        </label>
        <button class=" bg-white hover:bg-red-900 rounded-xl shadow-md p-2">Login</button>
      </form>
      <NuxtLink to="/register" class=" w-full text-center hover:underline">ยังไม่มีบัญชี</NuxtLink>
    </div>
  </div>
</template>

<script lang="ts" setup>
import type { LoginRequest } from "@/type/LoginRequest"
import type { Response } from "@/type/Response"
import type { LoginResponse } from "~/type/UserResponse"
const email = ref<HTMLInputElement>()
const pass = ref<HTMLInputElement>()
const checkremember = ref<HTMLInputElement>()
const msg = ref<string>()
const config = useRuntimeConfig()
async function LoginAuth() {
  const dataRequest: LoginRequest = {
    username: email.value!.value,
    password: pass.value!.value
  }
  if (email.value!.value != "" && pass.value!.value != "") {
    try {
      const data = await $fetch<Response<LoginResponse>>(config.public.AuthBaseApiLogin, {
        method: "POST",
        body: dataRequest,
      })
      if (!data.isSucess) {
        msg.value = data.message
      } else {
        const token = useCookie<string>("token", {
          maxAge: checkremember.value!.checked ? 60 * 60 * 24 * 7 : undefined,
          path: '/',
          sameSite: 'lax',
          secure: process.env.NODE_ENV === 'production',
        });
        token.value = data.result.token;
        localStorage.setItem("email", data.result.user.email);
        navigateTo("/")
      }
    } catch (error: any) {
      msg.value = error?.data?.message;
    }
  } else {
    msg.value = "กรอกข้อมูล"
  }
}
</script>

<style></style>