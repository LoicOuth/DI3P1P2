<template>
  <div class="w-full h-screen">
    <div class="alert-container">
      <FormsAlertComponent v-for="alert in alerts" :key="alert.id.toString()" :alert="alert" />
    </div>
    <NuxtLayout>
      <NuxtLoadingIndicator color="#FCA311" />
      <NuxtPage />
    </NuxtLayout>
  </div>
</template>

<script lang="ts" setup>
const alerts = useAlert()

</script>
