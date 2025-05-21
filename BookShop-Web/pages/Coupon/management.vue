<template>
  <NuxtLayout name="admin">
    <div class=" flex flex-col gap-3">
      <h1 class=" text-3xl font-bold">Management Coupon</h1>
      <h2 v-if="pending">Loading</h2>
      <h2 v-else-if="fetchError?.statusCode == 401" class="text-2xl text-red-600">
        ไม่มีสิทธิเข้าถึง กรุณา login
      </h2>
      <div class="flex flex-col gap-3" v-else >
        <div class=" *:bg-blue-400  *:p-2 *:rounded-md">
          <NuxtLink to="CouponAdd" class="shadow-md hover:bg-blue-800 hover:text-white hover:transition-all" @click="">
            Add Coupon
          </NuxtLink>
        </div>
        <table class="w-full table-auto bg-white rounded-lg shadow-md border border-gray-200">
          <thead class="bg-green-600 text-white">
            <tr>
              <th class="text-left px-4 py-3">
                <label class="inline-flex items-center gap-2">
                  <input type="checkbox" id="input-all-coupon" class="accent-white w-4 h-4">
                  <span>All</span>
                </label>
              </th>
              <th class="text-left px-4 py-3">ID</th>
              <th class="text-left px-4 py-3">Code</th>
              <th class="text-right px-4 py-3">Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="coupon in coupons" :key="coupon.couponId" class="hover:bg-gray-50 border-t">
              <td class="px-4 py-3">
                <input type="checkbox" class="w-4 h-4 accent-green-600" />
              </td>
              <td class="px-4 py-3 text-gray-800">{{ coupon.couponId }}</td>
              <td class="px-4 py-3 text-gray-800">{{ coupon.couponCode }}</td>
              <td class="px-4 py-3 text-right">
                <div class="flex justify-end gap-2">
                  <NuxtLink v-if="coupon?.couponCode!=null" :to="{
                    name: 'Coupon-code-code',
                    params: { code: coupon.couponCode },
                    query: { edit: 'true' }
                  }" 
                  class=" bg-blue-500 hover:bg-blue-600 text-white px-3 py-1 rounded text-sm">Edit</NuxtLink>
                  <NuxtLink :to="{
                    name: 'Coupon-code-code',
                    params: { code: coupon.couponCode },
                    query: { deleted: 'true' }
                  }"
                    class="bg-red-500 hover:bg-red-600 text-white px-3 py-1 rounded text-sm">Delete</NuxtLink>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>

  </NuxtLayout>

</template>

<script lang="ts" setup>
import type { CouponResponse } from '~/type/CouponResponse';
import type { Response } from '~/type/Response';
const config = useRuntimeConfig()
const token = useCookie("token")
const isLoading = ref<boolean>(false);
const { data, pending, error: fetchError } = await useFetch<Response<CouponResponse[]>>(config.public.apiBase + "/api/CouponAPI", {
  headers: { Authorization: "Bearer " + token.value }
});
const coupons = computed(() => data.value?.result ?? [])
isLoading.value = pending.value

</script>
