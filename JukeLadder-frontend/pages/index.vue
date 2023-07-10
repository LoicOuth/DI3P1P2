<template>
  <div class="flex flex-col items-center h-full bg-primary">
    <div class="flex items-center justify-between w-1/3 p-5">
      <img class="mx-5 h-14" src="~/assets/icons/music-solid.svg" alt="Logo">
      <h1 class="ml-5 text-white">
        JukeLadder
      </h1>

      <ChangeLanguage />
    </div>
    <div class="flex items-center justify-center w-full mt-32">
      <a
        :href="`${KEYCLOACK_URL}auth?response_type=code&client_id=${KEYCLOACK_CLIENT_ID}&redirect_uri=${encodeURIComponent(`http://${currentHostname()}${localePath('/auth/callback')}`)}`"
        class="flex justify-center w-1/3 px-4 py-2 font-bold text-white border-2 rounded shadow-lg bg-secondary hover:bg-primary border-primary hover:border-secondary"
      >
        {{ $t('login.btn') }}
      </a>
    </div>
  </div>
</template>

<script lang="ts" setup>
definePageMeta({
  layout: false
})

const { currentHostname } = useUtils()
const localePath = useLocalePath()
const { KEYCLOACK_URL, KEYCLOACK_CLIENT_ID } = useRuntimeConfig()

</script>
