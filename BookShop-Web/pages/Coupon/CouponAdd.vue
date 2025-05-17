<template>
    <div class="flex flex-col gap-2">
        <h1 class="text-3xl font-bold">AddCoupon</h1>
        <form class="flex flex-col gap-2" v-on:submit.prevent="addCouponSubmit">
            <label>
                Code*
                <input ref="code" required class="w-full p-2 border-1 rounded" placeholder="ABCD">
            </label>
            <label>
                Discount*
                <input ref="discount" required class="w-full p-2 border-1 rounded" placeholder="0">
            </label>
            <label class="peer/option">
                <input type="checkbox" id="checkbox-addid" class="w-4 h-4 accent-green-600" />
                More Options
            </label>
            <div class="hidden peer-has-checked/option:flex peer-has-checked/option:flex-col gap-1 ">
                <label>
                    MinAmount
                    <input ref="minamout" class="w-full p-2 border-1 rounded" placeholder="0">
                </label>
            </div>
            <button type="submit" class="shadow-md bg-blue-600 hover:bg-blue-900 p-3 rounded text-white">Add</button>
        </form>

    </div>
</template>

<script lang="ts" setup>
import type { CouponResponse } from '~/type/CouponResponse';
import type { Response } from '~/type/Response';
const config = useRuntimeConfig()
const code = ref<HTMLInputElement | null>(null)
const discount = ref<HTMLInputElement | null>(null)
const minamout = ref<HTMLInputElement | null>(null)
const route = useRouter()
async function addCouponSubmit() {
    const couponCode = code.value?.value.trim() ?? ''
    const discountAmount = discount.value?.value.trim() ?? ''
    const minAmountVal = minamout.value?.value.trim() ?? ''

    if (!couponCode || !discountAmount) {
        alert('กรุณากรอก Code และ Discount')
        return
    }

    const data: CouponResponse = {
        couponId: 0,
        couponCode,
        discountAmount: Number(discountAmount),
        minAmount: minAmountVal ? Number(minAmountVal) : 0,
        lastUpdated:new Date()
    }

    console.log("🚀 Sending:", data)

    const { data:response,error } = await useFetch<Response>(config.public.CouponBase, {
        method: "POST",
        body: data, // ✅ ไม่ต้อง stringify เอง
    })

    if (response.value?.isSucess==false) {
        alert(response.value?.message)
    } else {
        alert("เพิ่มคูปองสำเร็จ")
        route.back()
    }
}
</script>
