<template>
  <div class="flex flex-col items-center w-full">
    <img src="~/assets/images/unauthorized.png" alt="403" class="mt-5">

    <NuxtLink to="/" class="text-white hover:text-secondary">
      {{ $t('forbidden.btn') }}
    </NuxtLink>
  </div>
</template>
