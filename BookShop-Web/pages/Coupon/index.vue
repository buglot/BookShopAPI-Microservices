<template>
    <NuxtLayout name="admin">
        <div class="flex flex-col gap-3">
            <h1 class="text-3xl font-bold">Coupon List</h1>
            <div v-if="pending">Loading...</div>
            <h2 v-else-if="error?.statusCode == 401" class="text-2xl text-red-600">
                ไม่มีสิทธิเข้าถึง กรุณา login
            </h2>
            <div v-else-if="error" class="text-red-600">
                Error: {{ error.message }}
            </div>
            <div class=" flex flex-col gap-2" v-else>
                <h2 class=" text-2xl">Total : {{ coupons.length }}</h2>
                <form @submit.prevent="searching">
                    <input ref="search" class="w-full p-1 border-1" placeholder="Search code : ABCD" />
                </form>
                <div class="flex gap-2 bg-green-500 p-2 rounded shadow-md">
                    <a class="w-1/3">Code</a><a class="w-1/3">Discount</a><a class="w-1/3">MinAmount</a>
                </div>
                <NuxtLink :to="`/Coupon/code/${coupon.couponCode}`"
                    class=" hover:bg-red-900 shadow-md w-full flex p-3 rounded-md gap-2 hover:text-white"
                    v-for="coupon in coupons" :key="coupon.couponId">
                    <a class="w-1/3">{{ coupon.couponCode }}</a>
                    <a class="w-1/3"> ฿{{ coupon.discountAmount }}</a>
                    <a class="w-1/3">{{ coupon.minAmount ? "฿" + coupon.minAmount : "-" }}</a>
                </NuxtLink>
            </div>
        </div>
    </NuxtLayout>
</template>

<script setup lang="ts">
import type { CouponResponse } from '@/type/CouponResponse'
import type { Response } from '@/type/Response'
import { useRouter } from 'vue-router'
const search = ref<HTMLInputElement>()
const config = useRuntimeConfig()
const route = useRouter()
const token = useCookie("token")
const { data, pending, error } = await useLazyFetch<Response<CouponResponse[]>>(`${config.public.apiBase}/api/CouponAPI`,
    {
        headers: { Authorization: "Bearer " + token.value }
    }
)
if (token.value == null) {
    navigateTo("/")
}
const coupons = computed(() => data.value?.result ?? [])
function searching() {
    route.push("/Coupon/code/" + search.value?.value)
}
</script>
