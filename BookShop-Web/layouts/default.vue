<template>
  <div class=" flex flex-col min-h-screen">
    <NavBarMain/>
    <main class="flex-1 flex flex-col">
      <slot />
    </main>

  </div>
</template>

<script lang="ts" setup>
import NavBarMain from "@/component/NavBarMain.vue";
</script>
