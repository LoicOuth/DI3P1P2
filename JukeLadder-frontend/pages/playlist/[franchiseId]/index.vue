<template>
  <div class="w-full max-h-full lg:w-1/3">
    <div class="flex items-center justify-between p-3 border-b-2 border-black">
      <img class="mx-5 h-14" src="~/assets/icons/music-solid.svg" alt="Logo">
      <h1 v-once class="ml-5 text-white">
        {{ playlist.franchise.name }}
      </h1>

      <ChangeLanguage />
    </div>
    <div v-if="playlist.playlistState" class="overflow-y-auto container-playlist">
      <TrackItem
        v-for="track in tracks"
        :key="track.id"
        v-touch:longtap="() => pressHandler(track)"
        :track="track"
        :have-vote="!track.isReading"
        :manager="false"
        :add-track="false"
        @refresh:tracks="refreshTracks"
      />
    </div>
    <h1 v-else class="mt-20 text-center text-white">
      {{ $t('playlist.desactivated') }}
    </h1>

    <button v-if="playlist.playlistState" class="btn-float lg:right-auto" @click="navigateTo(localePath(`/playlist/${playlist.franchise.id}/tracks/add`))">
      <img src="~/assets/icons/plus-solid.svg" alt="addtracks" class="h-6">
    </button>

    <PayFee
      v-if="trackDemote"
      :track-added="trackDemote"
      :is-promote="false"
      @demote-tracks="demoteTrackToPlaylist"
      @close="trackDemote = undefined"
    />
  </div>
</template>
<script lang="ts" setup>
import Track from '~~/core/interfaces/Track.interface'

useHead({
  title: 'Playlist'
})

definePageMeta({
  middleware: ['playlist-middleware']
})

const localePath = useLocalePath()

let interval = null

const playlist = usePlaylist()
const { getTracks } = useTrack()
const { generateIdentifier } = useIdentifier()
const trackDemote = ref<Track>()

const {
  data: tracks,
  refresh: refreshTracks
} = await useAsyncData('getTracks', () => getTracks(playlist.value.franchise.id),
  {
    server: false
  })

const pressHandler = async (track:Track) => {
  trackDemote.value = track
}

const demoteTrackToPlaylist = async () => {
  await refreshTracks()
}

onMounted(async () => {
  interval = setInterval(async () => {
    playlist.value.trackMinutes = 0
    await refreshTracks()
    tracks.value.forEach((el) => { playlist.value.trackMinutes += el.duration })
  }, 5000)
  generateIdentifier()
  tracks.value.forEach((el) => { playlist.value.trackMinutes += el.duration })
})

onUnmounted(() => {
  clearInterval(interval)
})
</script>
