<template>
  <div class="relative z-0 w-full mb-5">
    <input
      type="text"
      placeholder=" "
      :value="props.modelValue"
      class="block w-full px-0 pt-3 pb-2 mt-0 bg-transparent border-0 border-b-2 border-gray-200 appearance-none focus:outline-none focus:ring-0 focus:border-secondary"
      @input="handleInput(($event.target as HTMLInputElement).value)"
    >
    <label class="absolute text-gray-500 duration-300 top-3 z-1 origin-0">{{ props.label }}</label>
    <div v-if="showDropdown" class="absolute w-full h-auto overflow-auto bg-white rounded-md shadow-md max-h-20">
      <div v-for="suggestion in filterList" :key="suggestion[props.optionKey]" class="p-1 mt-1 cursor-pointer hover:bg-primary hover:text-white">
        <div @click="handleChangeValue(suggestion[props.optionKey])">
          {{ suggestion[props.optionLabel] }}
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
const props = defineProps({
  suggestions: {
    type: Array<any>,
    required: true
  },
  modelValue: {
    type: String,
    required: true
  },
  label: {
    type: String,
    required: true
  },
  optionKey: {
    type: String,
    default: 'id'
  },
  optionLabel: {
    type: String,
    default: 'name'
  }
})
const emit = defineEmits(['update:modelValue'])

const filterList = ref([])
const showDropdown = ref(false)

const handleInput = (value: HTMLInputElement['value']) => {
  showDropdown.value = true
  filterList.value = props.suggestions.filter(el => el[props.optionLabel].includes(value))
  emit('update:modelValue', value)
}

const handleChangeValue = (email) => {
  showDropdown.value = false
  emit('update:modelValue', email)
}

onMounted(() => {
  filterList.value = props.suggestions
})
</script>
