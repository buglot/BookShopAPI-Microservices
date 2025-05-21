<template>
    <div class="flex flex-1 h-full justify-center items-center p-2">
        <div class=" w-3/4 md:w-1/4 h-full bg-green-500 flex flex-col p-2 text-black rounded-xl gap-5">
            <h1 class=" text-3xl font-bold">Register</h1>
            <form class="w-full gap-5 flex flex-col" @submit.prevent="registerOnsubmit">
                <input ref="name" class=" p-2 w-full bg-white text-black rounded-xl" placeholder="Name" required />
                <input type="email" ref="email" class=" p-2 w-full bg-white text-black rounded-xl" placeholder="Email"
                    required />
                <input type="password" ref="password" class=" p-2 w-full bg-white text-black rounded-xl"
                    placeholder="Password" required />
                <input type="password" ref="rePassword" class=" p-2 w-full bg-white text-black rounded-xl"
                    placeholder="rePassword" required />
                <input ref="phonenumber" class=" p-2 w-full bg-white text-black rounded-xl" placeholder="Phone number"
                    required />
                <button class=" bg-blue-300 hover:bg-red-900 rounded-xl shadow-lg p-2">Register</button>
            </form>
            <NuxtLink to="/login" class=" w-full text-center hover:underline">กลับไป login</NuxtLink>
        </div>

    </div>
</template>
<script lang="ts" setup>
import type { RegisterRequest } from "@/type/RegisterRequest";
import type { Response } from "@/type/Response";
import type { FetchError } from 'ofetch';
const password = ref<HTMLInputElement>()
const rePassword = ref<HTMLInputElement>()
const email = ref<HTMLInputElement>()
const name = ref<HTMLInputElement>()
const phonenumber = ref<HTMLInputElement>()
const nav = useRouter()
async function registerOnsubmit() {
    const config = useRuntimeConfig()
    if (rePassword.value!.value == password.value!.value) {
        const dataRequest: RegisterRequest = {
            name: name.value!.value,
            password: rePassword.value!.value,
            phoneNumber: phonenumber.value!.value,
            email: email.value!.value,
            role: "user"
        }

        try {
            const data = await $fetch<Response<null>>(config.public.AuthBaseApiRegister, {
                method: "POST",
                body: dataRequest,
            });

            if (!data.isSucess) {
                alert(data.message);
            } else {
                nav.push("/login");
            }

        } catch (error: any) {
            console.error("Fetch error:", error);
            alert("vbvbvb "+error?.data?.message || "เกิดข้อผิดพลาดบางอย่าง");
        }

    } else {
        alert("รหัสไม่ตรงกัน")
    }

}
</script>

<style></style>