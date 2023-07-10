<template>
  <div class="flex flex-col w-full">
    <h1>{{ franchise.name }}</h1>

    <div class="flex items-center justify-between mx-5">
      <h2 class="mt-5">
        Url playlist : <a class="hover:underline hover:text-secondary" :href="url">{{ url }}</a>
      </h2>

      <QrcodeVue :value="url" class="mt-5 rounded-md shadow-md" />
    </div>

    <fieldset class="flex w-full p-10 mt-10 border rounded-md shadow-md border-primary">
      <legend>Services</legend>
      <div class="flex items-center w-full justify-evenly">
        <FormsSwitchComponent
          v-model="playlistState"
          :label="$t('manager.dashboard.serviceStatus')"
          class="w-1/4"
          @update:model-value="handleChangePlaylistState"
        />
        <FormsSwitchComponent
          v-model="billingState"
          :label="$t('manager.dashboard.payFeature')"
          class="w-1/4"
          @update:model-value="handleChangeBillingState"
        />
      </div>
    </fieldset>
  </div>
</template>
<script lang="ts" setup>
useHead({
  title: 'Manager - Dashboard'
})

definePageMeta({
  layout: 'manager',
  middleware: ['auth-middleware', 'role-manager-middleware']
})

const { currentHostname } = useUtils()
const localePath = useLocalePath()

const franchise = useManagerFranchise()
const url = `http://${currentHostname()}${localePath(`/playlist/${franchise.value.id}`)}`

const { getPlaylistState, updatePlaylistState } = usePlaylistState()
const { getBillingState, updateBillingState } = useBillingState()

const
  {
    data: playlistState
  } =
  await useAsyncData('playlistState', () => getPlaylistState(franchise.value.id))

const {
  data: billingState
} = await useAsyncData('billingState', () => getBillingState(franchise.value.id))

const handleChangePlaylistState = async (value) => {
  playlistState.value = value
  await updatePlaylistState(franchise.value.id, value)
}

const handleChangeBillingState = async (value) => {
  billingState.value = value
  await updateBillingState(franchise.value.id, value)
}
</script>
