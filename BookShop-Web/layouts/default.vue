<template>
  <div class=" flex min-h-screen">
    <NavBar />
    <main class="p-3 fixed w-5/6 right-0 h-full overflow-auto">
      <slot />
    </main>

  </div>
</template>

<script lang="ts" setup>
import NavBar from "@/component/NavBar.vue";
</script>
