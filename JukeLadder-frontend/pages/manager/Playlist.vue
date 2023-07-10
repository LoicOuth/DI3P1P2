<template>
  <div class="flex justify-center w-full">
    <div class="flex flex-col items-center w-1/3 mt-10 mr-5">
      <button class="w-full mb-5 btn-primary" @click="navigateTo(localePath(`/manager/tracks/add`))">
        {{ $t('manager.playlist.btnAdd') }}
      </button>
      <div class="flex ">
        <label>
          {{ $t('manager.playlist.theme') }}
        </label>
        <select
          class="p-2 ml-5 border border-gray-300 rounded-md shadow-sm"
          @change="updateGenre(($event.target as HTMLInputElement).value)"
        >
          <option v-for="genre in genres" :key="genre" :selected="(franchise.theme === genre)">
            {{ genre }}
          </option>
        </select>
      </div>
    </div>
    <div class="w-2/3 overflow-y-auto">
      <draggable v-model="tracks" item-key="id" :move="checkMove" @change="onSort">
        <template #item="dragObject">
          <TrackItem
            :key="dragObject.element.id"
            :track="dragObject.element"
            :have-vote="!dragObject.element.isReading"
            :add-track="false"
            :manager="true"
            :class="{ 'cursor-move': dragObject.index !== 0 }"
            @refresh:tracks="refreshTracks"
          />
        </template>s
      </draggable>
    </div>
  </div>
</template>
<script setup lang="ts">
useHead({
  title: 'Manager - Playlist',
  script: [
    { src: 'https://e-cdn-files.dzcdn.net/js/min/dz.js' }
  ]
})

definePageMeta({
  layout: 'manager',
  middleware: ['auth-middleware', 'role-manager-middleware']
})

let interval = null

const franchise = useManagerFranchise()
const localePath = useLocalePath()
const { getTracks, setTrackPosition } = useTrack()
const { getGenre } = useDeezer()
const { updateFranchise } = useFranchise()

const {
  data: tracks,
  refresh: refreshTracks
} = await useAsyncData('getTracks', () => getTracks(franchise.value.id.toString()))

const {
  data: genres
} = await useAsyncData('getGenres', () => getGenre())

const updateGenre = async (genre: string) => {
  franchise.value.theme = genre
  await updateFranchise(franchise.value, franchise.value.id)
}

const onSort = (event) => {
  setTrackPosition(event.moved.newIndex, event.moved.element.id)
}

const checkMove = (event) => {
  return (event.draggedContext.index !== 0)
}

onMounted(async () => {
  interval = setInterval(async () => await refreshTracks(), 5000)
})

onUnmounted(() => {
  clearInterval(interval)
})
</script>
