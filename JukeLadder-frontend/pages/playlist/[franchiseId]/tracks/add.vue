<template>
  <div class="flex flex-col w-full max-h-full lg:w-1/3">
    <div class="flex items-center mt-3 ml-2">
      <button @click="$router.go(-1)">
        <img src="~/assets/icons/arrow-left-solid.svg" class="h-6" alt="back">
      </button>
      <h2 class="ml-5 text-white">
        {{ $t('manager.playlist.manage.titleAdd') }}
      </h2>
    </div>
    <div class="flex flex-col w-full">
      <SearchTrack class="mt-8" @get-tracks="getTracks" />

      <div v-if="!pendingTracks" class="overflow-y-auto container-search">
        <TrackItem
          v-for="track in tracks"
          :key="track.id"
          class="cursor-pointer hover:border-r border-secondary"
          :track="track"
          :have-vote="false"
          :manager="false"
          :add-track="false"
          @refresh:tracks="refreshTracks"
          @click="trackAdded = track"
        />
      </div>

      <FormsLoaderComponent v-else />

      <PayFee
        v-if="trackAdded"
        :track-added="trackAdded"
        :is-promote="true"
        :billing-state="playlist.billingState"
        @add-tracks="addTrackToPlaylist"
        @close="trackAdded = undefined"
      />
    </div>
    <div class="object-bottom text-white">
      <footer>{{ $t('playlist.theme') }} : {{ playlist.franchise.theme }}</footer>
    </div>
  </div>
</template>

<script lang="ts" setup>
import PayFee from '~~/components/pay-fee.vue'
import TrackSolar from '~~/core/interfaces/TrackSolar.interface'

definePageMeta({
  middleware: ['playlist-middleware']
})

const playlist = usePlaylist()
const { getTrackWithSolarSuggessions, addTrack } = useTrack()
const trackAdded = ref<TrackSolar>()
let type = ''
let term = ''

const {
  pending: pendingTracks,
  data: tracks,
  refresh: refreshTracks
} = await useAsyncData(() => getTrackWithSolarSuggessions(
  type,
  term,
  playlist.value.franchise.theme,
  playlist.value.franchise.id
),
{
  server: false
})

const getTracks = async (field: string, value: string) => {
  type = field.toLowerCase()
  term = value
  await refreshTracks()
}

const addTrackToPlaylist = async () => {
  await addTrack(trackAdded.value!).then(async () => {
    navigateTo(`/playlist/${playlist.value.franchise.id}`, { replace: true })
    trackAdded.value = undefined
    tracks.value = []
  })
}
</script>
