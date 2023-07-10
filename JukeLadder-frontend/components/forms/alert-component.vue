<template>
  <div
    class="block max-w-full mx-auto mb-3 text-sm rounded-lg shadow-lg pointer-events-auto w-80 bg-clip-padding"
    :class="props.alert.isSuccess ? 'bg-success' : 'bg-error'"
    role="alert"
  >
    <div
      class="flex items-center justify-between px-3 py-2 border-b border-white rounded-t-lg bg-clip-padding"
      :class="props.alert.isSuccess ? 'bg-success' : 'bg-error'"
    >
      <p class="flex items-center font-bold text-white">
        <img
          v-if="props.alert.isSuccess"
          class="w-4 h-4 mr-2"
          alt="icon"
          src="~/assets/icons/check-white.svg"
        >
        <img
          v-else
          class="w-4 h-4 mr-2"
          alt="icon"
          src="~/assets/icons/error-white.svg"
        >
        {{ props.alert.title }}
      </p>
      <div class="flex items-center">
        <img alt="close" class="h-5 cursor-pointer" src="~/assets/icons/xmark-solid-white.svg" @click="closeAlert">
      </div>
    </div>
    <div class="p-3 text-white break-words rounded-b-lg" :class="props.alert.isSuccess ? 'bg-success' : 'bg-error'">
      {{ props.alert.message }}
    </div>
  </div>
</template>

<script lang="ts" setup>
import Alert from '~~/core/models/Alert.model'

const alerts = useAlert()

const props = defineProps({
  alert: {
    type: Alert,
    required: true
  }
})

const closeAlert = () => {
  const index = alerts.value.findIndex((el: any) => el.id === props.alert.id)
  alerts.value.splice(index, 1)
}

onMounted(() => {
  setTimeout(() => closeAlert(), props.alert.showtime)
})
</script>
