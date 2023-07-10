<template>
  <div class="flex items-center justify-center w-full h-full">
    <div v-if="!error" class="flex flex-col items-center justify-center">
      <h1 class="text-4xl">
        {{ $t('manager.load.loadingText') }}
      </h1>
      <FormsLoaderComponent class="h-20 mt-8" />
    </div>
    <div v-else class="flex flex-col">
      <h1> {{ $t('manager.load.errorText') }}</h1>
      <NuxtLink to="/" class="flex justify-center mt-5 btn-primary">
        {{ $t('manager.load.btnText') }}
      </NuxtLink>
    </div>
  </div>
</template>
<script setup lang="ts">
definePageMeta({
  layout: false,
  middleware: ['auth-middleware']
})

const error = ref(false)
const franchise = useManagerFranchise()
const authUser = useAuthUser()
const localePath = useLocalePath()

const { getFranchiseByEmail } = useFranchise()

onMounted(async () => {
  await getFranchiseByEmail(authUser.value.email)
    .then((response) => {
      franchise.value = response
      navigateTo(localePath('/manager/dashboard'))
    })
    .catch(err => (error.value = err))
})
</script>
