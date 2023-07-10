<template>
  <div class="flex">
    <FormsModalComponent v-if="deleteItem != null" :title="$t('manager.playlist.manage.title')" @close="deleteItem = null">
      <div class="flex flex-col">
        <h2>
          {{ $t('manager.playlist.manage.body') }}
          {{ deleteItem.title }} ?
        </h2>
        <button class="w-full mt-5 btn-danger" @click="handleDelete">
          {{ $t('manager.playlist.manage.btn') }}
        </button>
      </div>
    </FormsModalComponent>
    <button
      v-if="(props.addTrack == true)"
      class="w-full mt-5 btn-primary"
      @click="handleAdd(props.track)"
    >
      {{ $t('manager.playlist.manage.btnAdd') }}
    </button>
    <img
      v-if="(props.addTrack == false)"
      alt="delete"
      src="~/assets/icons/trash-solid.svg"
      class="h-6 cursor-pointer"
      @click="(deleteItem = props.track)"
    >
  </div>
</template>

<script lang="ts" setup>
import Track from '~~/core/interfaces/Track.interface'

const localePath = useLocalePath()
const { deleteTrack, addTrack } = useTrack()
const deleteItem = ref<Track | null>(null)

const props = defineProps({
  track: {
    type: Object as () => Track,
    required: true
  },
  addTrack: {
    type: Boolean,
    default: false
  }
})

const handleDelete = () => {
  deleteTrack(props.track.id)
    .then(() => {
      emits('refresh:tracks')
    })
}

const handleAdd = (track:Track) => {
  addTrack(track!).then(async () => {
    navigateTo(localePath('/manager/playlist'), { replace: true })
  })
}

const emits = defineEmits(['refresh:tracks'])
</script>
