<template>
  <div class="flex items-center justify-center w-full h-full">
    <div class="flex flex-col items-center justify-center">
      <h1 class="text-4xl text-white">
        {{ $t('playlist.loadingText') }}...
      </h1>
      <FormsLoaderComponent class="h-20 mt-8" />
    </div>
  </div>
</template>
<script setup lang="ts">
import Playlist from '~~/core/models/Playlist.model'
const { t } = useI18n()

useHead({
  title: `${t('playlist.loadingText')}...`
})
const { getPlaylistState } = usePlaylistState()
const { getBillingState } = useBillingState()
const { getFranchise } = useFranchise()
const localePath = useLocalePath()

const playlist = usePlaylist()

const franchiseId = useRoute().params.franchiseId as string

onMounted(async () => {
  const playlistState = await getPlaylistState(franchiseId)
  const billingState = await getBillingState(franchiseId)
  const franchise = await getFranchise(franchiseId)

  playlist.value = new Playlist(playlistState, billingState, franchise)

  navigateTo(localePath(`/playlist/${franchiseId}`))
})
</script>
