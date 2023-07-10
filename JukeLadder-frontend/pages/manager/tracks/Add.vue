<template>
  <div class="flex justify-center">
    <div class="flex flex-col w-full max-h-full lg:w-1/3">
      <div class="flex items-center mt-3 ml-2">
        <button @click="$router.go(-1)">
          <img src="~/assets/icons/arrow-left-solid.svg" class="h-6" alt="back">
        </button>
        <h2 class="ml-5">
          {{ $t('manager.playlist.manage.titleAdd') }}
        </h2>
      </div>
      <div class="flex flex-col w-full">
        <SearchTrack class="mt-8" :contrast="true" @get-tracks="getTracks" />
        <div v-if="!pendingTracks" class="overflow-y-auto container-tracks-manager">
          <TrackItem
            v-for="track in tracks"
            :key="track.id"
            class="cursor-pointer hover:border-r border-secondary"
            :track="track"
            :have-vote="false"
            :add-track="true"
            :manager="true"
            @refresh:tracks="refreshTracks"
            @click="trackAdded = track"
          />
        </div>
        <FormsLoaderComponent v-else />
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import TrackSolar from '~~/core/interfaces/TrackSolar.interface'

useHead({
  title: 'Manager - Add track'
})

definePageMeta({
  layout: 'manager',
  middleware: ['auth-middleware', 'role-manager-middleware']
})

const franchise = useManagerFranchise()
const { getTrackWithSolarSuggessions } = useTrack()
const trackAdded = ref<TrackSolar>()
let type = ''
let term = ''

const {
  pending: pendingTracks,
  data: tracks,
  refresh: refreshTracks
} = await useAsyncData(() => getTrackWithSolarSuggessions(type, term, franchise.value.theme, franchise.value.id),
  {
    server: false
  })

const getTracks = async (field: string, value: string) => {
  type = field.toLowerCase()
  term = value
  await refreshTracks()
}
</script>
