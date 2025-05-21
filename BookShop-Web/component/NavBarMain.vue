<template>
  <nav class=" flex p-3 w-full h-16 bg-green-600  shadow-md justify-between items-center">
    <NuxtLink to="/" class=" font-bold ">BookShop</NuxtLink>
    <form>
      <label>
        <input type="text" placeholder="Searching" class=" p-2 rounded bg-white" />
      </label>
    </form>
    <div class=" flex gap-5 h-full">
      <div class="w-[90px] h-full items-center rounded flex justify-center hover:bg-green-800">
        <Icon name="maki:grocery" size="24"></Icon>
      </div>
      <label class=" peer" v-if="token">
        <input type="checkbox"  class=" hidden">
        {{ name }}
      </label>
      <NuxtLink v-else to="/login">
        Login
      </NuxtLink>
      <div @click="Logout" class="hidden peer-has-checked:flex">
        Logout
      </div>
      
    </div>
  </nav>
</template>

<script lang="ts" setup>
const token = useCookie("token");
const name = ref<string | null>();

onMounted(() => {
  name.value = localStorage.getItem("email");
});
function Logout() {
  token.value = null
  localStorage.clear()
}
</script>
