<template>
  <div class="relative flex justify-center w-full py-4">
    <FormsFloatInput
      v-model="search"
      :class="!props.contrast ? 'text-white' : 'text-black'"
      :label="$t('searchComponent.placeholder')"
      @input="searchSolarSuggestions"
    />
    <Transition>
      <div
        v-if="!pendingSuggestions && solarSuggestions!.length > 0"
        class="absolute flex flex-wrap w-full p-2 overflow-auto bg-white rounded-md max-h-40 top-16"
        :class="{'shadow-md': props.contrast}"
      >
        <div
          v-for="solarSuggestion in solarSuggestions"
          :key="solarSuggestion.term"
          class="w-2/4 p-2 rounded-md cursor-pointer hover:bg-secondary"
          @click="getTracks(solarSuggestion.type, solarSuggestion.term)"
        >
          <div class="flex flex-col">
            <h4>{{ solarSuggestion.term }}</h4>
            <h6>{{ solarSuggestion.type }}</h6>
          </div>
        </div>
      </div>
    </Transition>
  </div>
</template>
<script lang="ts" setup>
const { getSolarSuggestions } = useTrack()
const search = ref('')
let timeOut: NodeJS.Timeout | null = null

const emits = defineEmits(['getTracks'])

const props = defineProps({
  contrast: {
    type: Boolean,
    default: false
  }
})

const {
  pending: pendingSuggestions,
  data: solarSuggestions,
  refresh: refreshSuggestions
} = await useAsyncData(() => getSolarSuggestions(search.value),
  {
    server: false
  }
)

const searchSolarSuggestions = async () => {
  if (timeOut) {
    clearTimeout(timeOut)
    timeOut = null
  }
  timeOut = setTimeout(async () => {
    await refreshSuggestions()
  }, 300)
}

const getTracks = (field: string, value: string) => {
  emits('getTracks', field, value)
  search.value = value
  solarSuggestions.value = []
}

</script>
