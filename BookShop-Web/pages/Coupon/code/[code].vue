<template>
  <NuxtLayout name="admin">
    <div v-if="pending">
      Loading...
    </div>
    <div v-else-if="data?.isSucess" class="flex flex-col gap-2 ">
      <h2 class="text-2xl font-bold">Code : {{ coupon?.couponCode }}</h2>
      <main class=" flex flex-col gap-2 p-2">
        <h3 class="text-xl font-bold">About Code:</h3>
        <p>CouponId : {{ coupon?.couponId }}</p>
        <p>CouponCode : {{ coupon?.couponCode }}</p>
        <p>Discount : {{ coupon?.discountAmount }}%</p>
        <p>MinAmount : {{ coupon?.minAmount }}</p>
        <p>Last Update : {{ coupon?.lastUpdated }}</p>
      </main>
      <label class="peer flex items-center gap-1">
        <input type="checkbox" class=" w-5 h-5 accent-green-500 " :checked="editquery">
        Edit?
      </label>
      <form class="hidden peer-has-checked:flex flex-col gap-2" @submit.prevent="updateSubmit">
        <label>CouponId : <input ref="couponid" :value="`${coupon?.couponId}`"
            class="p-2 border-1 rounded w-full"></label>
        <label>CouponCode : <input ref="couponcode" :value="`${coupon?.couponCode}`"
            class="p-2 border-1 rounded w-full"></label>
        <label>Discount : <input ref="discount" :value="`${coupon?.discountAmount}`"
            class="p-2 border-1 rounded w-full"></label>
        <label>MinAmount : <input ref="minamout" :value="`${coupon?.minAmount}`"
            class="p-2 border-1 rounded w-full"></label>
        <button type="submit"
          class=" bg-blue-600 text-white hover:bg-blue-900 active:bg-blue-500 transition-all p-2 rounded">Update1</button>
      </form>
      <label class="peer/delete flex items-center gap-1">
        <input type="checkbox" class=" w-5 h-5 accent-red-500 " :checked="deletequery">
        Delete?
      </label>
      <button @click="deletesubmit"
        class="hidden peer-has-checked/delete:flex bg-red-400 p-2 rounded justify-center text-white hover:bg-red-800">Delete</button>
    </div>
    <div v-else-if="error" class="text-red-500 font-bold flex flex-col">
      <h3 class=" text-xl font-bold">Not Found Code : {{ code }}</h3>
      {{ error.message }}
      <NuxtLink class="text-lg underline" to="/Coupon">Get Back</NuxtLink>
    </div>
    <div v-else class="flex flex-col">
      <h3 class=" text-xl font-bold">Not Found Code : {{ code }}</h3>
      <NuxtLink class="text-lg underline" to="/Coupon">Get Back</NuxtLink>
    </div>
  </NuxtLayout>
</template>

<script lang="ts" setup>
import type { Response } from '~/type/Response'
import type { CouponResponse } from '~/type/CouponResponse'
const route = useRoute()
const router = useRouter()
const { edit } = route.query
const editquery = ref<boolean>(edit == "true")
const { deleted } = route.query
const deletequery = ref<boolean>(deleted == "true")
const config = useRuntimeConfig()
const code = route.params.code as string
const token = useCookie("token")
const { data, pending, error } = await useFetch<Response<CouponResponse>>(config.public.apiBase + "/api/CouponAPI/GetCode/" + code,
  {
    headers: {
      Authorization: "Bearer " + token.value
    }
  })
const coupon = computed(() => data.value?.result ?? null)
const couponid = ref<HTMLInputElement | null>(null)
const couponcode = ref<HTMLInputElement | null>(null)
const discount = ref<HTMLInputElement | null>(null)
const minamout = ref<HTMLInputElement | null>(null)

async function updateSubmit() {
  console.log("🚀 Sending:")
  const couponCode = couponcode.value?.value.trim() ?? ''
  const discountAmount = discount.value?.value.trim() ?? ''
  const minAmountVal = minamout.value?.value.trim() ?? ''
  const data: CouponResponse = {
    couponId: Number(couponid.value!.value),
    couponCode,
    discountAmount: Number(discountAmount),
    minAmount: minAmountVal ? Number(minAmountVal) : 0,
    lastUpdated: new Date()
  }
  console.log("🚀 Sending:", data)
  try {
    const response = await $fetch<Response<any>>(config.public.CouponBase, {
      method: "PUT",
      headers: { Authorization: "Bearer " + token.value },
      body: data,
    })
    if (response.isSucess == false) {
      alert(response.message)
    } else {
      alert("update สำเร็จ")
      window.location.reload();
    }
  } catch (error: any) {
    alert(error?.data?.message)
  }

}
async function deletesubmit() {
  if (confirm("are you sure for detele this code?") == true) {
    try {
      const data = await $fetch<Response<any>>(config.public.CouponBase, {
        query: { id: coupon.value?.couponId },
        headers: { Authorization: "Bearer " + token.value },
        method: "DELETE",
      })
      if (data.isSucess == false) {
        alert(data.message)
      } else {
        alert("Delete สำเร็จ")
        router.push("/Coupon")
      }
    } catch (error: any) {
      alert(error?.data?.message)
    }


  }
}
</script>

<style></style>