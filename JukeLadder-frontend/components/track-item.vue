<template>
  <div
    class="flex flex-col justify-between my-3"
    :class="props.manager ? 'text-black' : 'text-white'"
    @click="emits('click')"
  >
    <div class="flex items-center p-4">
      <div v-if="(checkIsTrack(props.track) && props.track.position > 0)" class="mr-3 text-red-500">
        {{ props.track.position }}
      </div>
      <img class="mr-3 rounded-full h-14" :src="props.track.cover" alt="cover">
      <div class="w-full">
        <h2>
          {{ props.track.title }}
        </h2>
        <div class="flex justify-between w-full">
          <h3>
            {{ props.track.artist }}
          </h3>
          <h3 v-if="checkIsTrack(props.track) && props.track.isReading">
            <span class="text-secondary">{{ minutes.currentDuration }}</span>  / {{ minutes.totalDuration }}
          </h3>
          <div v-if="(haveVote && checkIsTrack(props.track) && props.track.datePromote == null && !props.manager)" class="flex">
            <div class="flex mr-3">
              <img
                src="~/assets/icons/thumbs-up-solid.svg"
                alt="like"
                class="h-6 ml-2 cursor-pointer"
                @click="handleVote(props.track.id, Vote.Up)"
              >
              <small class="mt-1 ml-1">{{ props.track.upvotes.length }}</small>
            </div>
            <div class="flex">
              <img
                src="~/assets/icons/thumbs-down-solid.svg"
                alt="dislike"
                class="h-6 cursor-pointer"
                @click="handleVote(props.track.id, Vote.Down)"
              >
              <small class="ml-1">{{ props.track.downvotes.length }}</small>
            </div>
          </div>
          <div v-else-if="checkIsTrack(props.track) && props.track.datePromote != null">
            {{ $t('trackItemComponent.promoted') }}
          </div>
          <ManagePlaylist v-if="(props.manager && checkIsTrack(props.track) && !props.track.isReading)" :track="props.track" :add-track="props.addTrack" @refresh:tracks="emits('refresh:tracks')" />
        </div>
        <div v-if="checkIsTrack(props.track) && props.track.isReading" class="w-full">
          <div id="dz-root" />
          <div class="w-full bg-black rounded-lg">
            <div id="bar" class="p-1 rounded-lg bg-secondary" :style="`width: ${progressBar}%;`" />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts" setup>
import { Vote } from '~~/core/enums/Vote.enum'
import Track from '~~/core/interfaces/Track.interface'
import TrackSolar from '~~/core/interfaces/TrackSolar.interface'

const props = defineProps<{
  track: Track | TrackSolar,
  haveVote: boolean,
  manager: boolean,
  addTrack: boolean
}>()
const { voteTrack, endTrack, updateProgressTrack } = useTrack()
const { secondToMinutes } = useUtils()
const { initPlayer, isDZLoad } = useDeezerPlayer()
const user = useAuthUser()

const emits = defineEmits(['refresh:tracks', 'click'])
const progressBar = ref(0)
const minutes = reactive({
  totalDuration: '00:00',
  currentDuration: '00:00'
})
let interval = null
let currentDur = 0

const checkIsTrack = (track: Track | TrackSolar) : track is Track => {
  return true
}

const handleVote = (id:string, action:Vote) => {
  voteTrack(id, action)
    .then(() => {
      emits('refresh:tracks')
    })
}

const onProgress = async (currentDuration, totalDuration) => {
  progressBar.value = 100 * currentDuration / totalDuration
  minutes.currentDuration = secondToMinutes(currentDuration)
  minutes.totalDuration = secondToMinutes(totalDuration)
  currentDur = currentDuration

  if (interval === null) {
    interval = setInterval(() => updateProgressTrack(props.track.id, currentDur), 5000)
  }
}

const onFinish = async () => {
  await endTrack(props.track.id, props.track.franchiseId)
  interval = null
}

watch(() => props.track, () => {
  if (checkIsTrack(props.track)) {
    if (props.track.isReading) {
      if (user.value) {
        if (user.value.roles.includes('manager')) {
          if (isDZLoad.value) {
            if (!DZ.player.getTrackList().some(el => el.id === props.track.deezerId)) {
              DZ.player.playTracks([props.track.deezerId])
            }
          }
        }
      } else {
        minutes.currentDuration = secondToMinutes(props.track.currentDuration)
        minutes.totalDuration = secondToMinutes(props.track.duration)

        progressBar.value = 100 * props.track.currentDuration / props.track.duration
      }
    }
  }
})

onMounted(() => {
  setTimeout(() => {
    if (checkIsTrack(props.track)) {
      if (props.track.isReading) {
        if (user.value) {
          if (user.value.roles.includes('manager')) {
            if (!isDZLoad.value) {
              initPlayer(onProgress, onFinish)
            }
          }
        }
      }
    }
  }, 100)
})

onUnmounted(() => {
  isDZLoad.value = false
})

</script>
