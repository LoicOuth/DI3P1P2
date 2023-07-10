<template>
  <div class="flex flex-wrap justify-center">
    <div class="flex flex-col items-center justify-center p-8 rounded-md shadow-lg w-60 bg-third">
      <h1>{{ $t('administrator.dashboard.widgetFranchise') }}</h1>
      <h1 class="mt-8 font-bold">
        {{ count }}
      </h1>
    </div>
  </div>
</template>
<script lang="ts" setup>
useHead({
  title: 'Admin - Dashboard'
})

definePageMeta({
  layout: 'admin',
  middleware: ['auth-middleware', 'role-admin-middleware']
})

const { getCountFranchisees } = useFranchise()

const {
  data: count
} = await useAsyncData(() => getCountFranchisees())

</script>
