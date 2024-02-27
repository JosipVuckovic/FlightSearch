<script setup lang="ts">
import FlightSearch from "@/components/FlightSearch.vue";
</script>

<template>

<main>
  <div>
    <FlightSearch/>
  </div>
  
  </main>
</template>

<style scoped>
html,
body {
  height: 100vh;
  width: 100vw;
  margin: 0;
}

main{
  height: 80vh;
  width: 80vw;
  
  background-size: cover;
}

#app {
  height: inherit;
  width: inherit;
}
</style>
