<template>
  <NuxtLink
    :to="props.to"
    class="flex flex-col items-center w-full transition-all duration-500 border-r-4 hover:border-secondary"
    :class="$route.path === props.to ? 'border-secondary' : 'border-transparent'"
  >
    <img alt="link-logo" class="h-16" :src="`/icons/${props.logo}.svg`">
    <h2 class="mt-3 text-white">
      {{ props.text }}
    </h2>
  </NuxtLink>
</template>

<script lang="ts" setup>

const props = defineProps({
  to: {
    type: String,
    required: true
  },
  text: {
    type: String,
    required: true
  },
  logo: {
    type: String,
    required: true
  }
})

</script>
