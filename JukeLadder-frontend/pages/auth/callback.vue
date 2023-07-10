<template>
  <div class="flex items-center justify-center w-full h-full">
    <div v-if="!error" class="flex flex-col items-center justify-center">
      <h1 class="text-4xl">
        {{ $t('callback.loadingText') }}...
      </h1>
      <FormsLoaderComponent class="h-20 mt-8" />
    </div>
    <div v-else class="flex flex-col">
      <h1> {{ $t('callback.errorText') }}</h1>
      <NuxtLink :to="localePath('/')" class="flex justify-center mt-5 btn-primary">
        {{ $t('callback.btn') }}
      </NuxtLink>
    </div>
  </div>
</template>
<script lang="ts" setup>
definePageMeta({
  layout: false
})
const localePath = useLocalePath()
const auth = useAuth()
const route = useRoute()
const error = ref(false)
await useAsyncData(() => auth.login(route.query.code as string).catch(() => {
  error.value = true
}), {
  server: false
})
</script>
