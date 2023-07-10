<template>
  <div class="flex w-full">
    <div class="flex flex-col w-1/3">
      <FormsFloatInput v-model="search" :label=" $t('manager.catalog.placeholder')" />
      <div class="flex items-center w-full">
        <button class="w-full mr-5 btn-primary" :disabled="isLoading" @click="handleSearch">
          {{ $t('manager.catalog.search') }}
        </button>
        <FormsLoaderComponent v-if="isLoading" />
      </div>
    </div>
    <div class="flex flex-wrap w-2/3 ml-5 overflow-y-auto container-catalog">
      <div v-for="playlist in playlists" :key="playlist.id">
        <div class="rounded-md cursor-pointer playlist-item" :style="`background-image: url(${playlist.picture});`">
          <div class="flex flex-col px-5 pt-5 text-center text-white rounded-md layer">
            <h6>{{ playlist.title }}</h6>

            <div class="flex items-center w-full">
              <button class="w-full mt-5 btn-primary" :disabled="isLoadingAdd" @click="handleAdd(playlist)">
                {{ $t('manager.catalog.btnAdd') }}
              </button>
              <FormsLoaderComponent v-if="isLoadingAdd" />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import Playlist from '~~/core/interfaces/Playlist.interface'

useHead({
  title: 'Manager - Catalog'
})

definePageMeta({
  layout: 'manager',
  middleware: ['auth-middleware', 'role-manager-middleware']
})

const franchise = useManagerFranchise()
const { getPlaylist } = useDeezer()
const { addTracksToCatalog } = useTrack()

const search = ref('')
const playlists = ref([])
const isLoading = ref(false)
const isLoadingAdd = ref(false)

const handleSearch = async () => {
  isLoading.value = true
  await getPlaylist(search.value).then((response) => {
    playlists.value = response
  })
    .finally(() => (isLoading.value = false))
}

const handleAdd = (playlist: Playlist) => {
  isLoadingAdd.value = true
  addTracksToCatalog(playlist.id.toString(), franchise.value.id)
    .finally(() => (isLoadingAdd.value = false))
}

</script>
