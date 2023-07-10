<template>
  <div class="flex justify-between w-full">
    <h2>{{ props.label }}</h2>
    <div
      class="flex items-center h-8 p-1 duration-300 ease-in-out bg-gray-300 rounded-full cursor-pointer w-14"
      :class="{ 'bg-green-500': props.modelValue}"
      @click="emits('update:modelValue', !props.modelValue)"
    >
      <div
        class="w-6 h-6 duration-300 ease-in-out transform bg-white rounded-full shadow-md"
        :class="{ 'translate-x-6': props.modelValue,}"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
const props = defineProps({
  modelValue: {
    type: Boolean,
    required: true
  },
  label: {
    type: String,
    required: true
  }
})

const emits = defineEmits(['update:modelValue'])

</script>
