<template>
  <div class="flex flex-col h-full">
    <div class="flex items-center justify-between w-full h-32 px-5 shadow-lg bg-primary">
      <img alt="logo" class="h-16 ml-12" src="/icons/music-solid.svg">

      <div class="flex items-center">
        <ChangeLanguage />
        <h1 class="mr-8 text-2xl text-white">
          {{ user?.email }}
        </h1>
        <img
          alt="logout"
          src="~/assets/icons/arrow-right-from-bracket-solid.svg"
          class="h-12 mr-5 cursor-pointer"
          @click="handleLogout"
        >
      </div>
    </div>
    <div class="flex h-full">
      <div class="hidden h-full shadow-lg md:block bg-primary w-60">
        <div class="flex flex-col items-center mt-5">
          <FormsNavLink :to="localePath('/manager/dashboard')" :text="$t('manager.sidebar.dashboard')" logo="chart-column-solid" />
          <FormsNavLink :to="localePath('/manager/payment')" :text="$t('manager.sidebar.payment')" logo="credit-card-solid-primary" class="mt-10" />
          <FormsNavLink :to="localePath('/manager/catalog')" :text="$t('manager.sidebar.catalog')" logo="book-solid" class="mt-10" />
          <FormsNavLink :to="localePath('/manager/playlist')" :text="$t('manager.sidebar.playlist')" logo="music-solid" class="mt-10" />
        </div>
      </div>
      <div class="w-full m-8">
        <slot />
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
const user = useAuthUser()
const auth = useAuth()
const localePath = useLocalePath()

const handleLogout = async () => {
  await auth.logout()
}
</script>
