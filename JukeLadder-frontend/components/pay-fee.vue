<template>
  <FormsModalComponent
    :title="isPromote ? `${$t('playlist.payFee.btnAdd')} ${props.trackAdded.title} ${$t('playlist.payFee.title')}` : `${$t('playlist.payFee.titleDemote')} ${props.trackAdded.title} ${$t('playlist.payFee.title')}`"
    @close="emits('close')"
  >
    <div v-if="!isPayToPromote" class="flex flex-col items-center">
      <p v-if="isPromote" class="text-center">
        {{ $t('playlist.payFee.part1') }} <br>

        {{ $t('playlist.payFee.part2') }} <span class="text-secondary">{{ secondToMinutes(playlist.trackMinutes) }} minutes</span>.<br>

        {{ $t('playlist.payFee.part3') }}
      </p>
      <p v-else>
        {{ $t('playlist.payFee.part4') }}
      </p>
      <button v-if="isPromote" class="flex justify-center w-full mt-8 btn-primary" @click="emits('addTracks')">
        <img src="~/assets/icons/plus-solid.svg" alt="addTrack" class="h-6 text-center">
        <div class="mx-5">
          {{ $t('playlist.payFee.btnAdd') }}
        </div>
      </button>

      <button v-if="(isPromote && props.billingState)" class="flex justify-center w-full my-3 btn-primary" @click="forcePromote()">
        <img src="~/assets/icons/credit-card-solid.svg" alt="card" class="h-6 text-center">
        <div class="mx-5">
          {{ $t('playlist.payFee.btnPay&Add') }}
        </div>
      </button>
      <button v-else-if="props.billingState" class="flex justify-center w-full my-3 btn-primary" @click="forcePromote()">
        <img src="~/assets/icons/credit-card-solid.svg" alt="card" class="h-6 text-center">
        <div class="mx-5">
          {{ $t('playlist.payFee.btnDemote') }}
        </div>
      </button>
    </div>
    <div v-else-if="!pending">
      <div v-if="errorMessage" class="flex justify-center p-2 my-5 bg-red-200 rounded text-error">
        {{ errorMessage }}
      </div>
      <fieldset class="px-5 py-3 my-4 border rounded-md border-primary">
        <legend><img alt="stripe-icon" class="h-8" src="~/assets/images/powered_stripe.png"></legend>
        <div class="flex flex-col w-full">
          <label class="mb-5">
            <div id="card-number" />
          </label>
          <div class="flex justify-between w-full mb-5">
            <label class="w-20">
              <div id="card-expiry" />
            </label>
            <label class="w-14">
              <div id="card-cardCvc" />
            </label>
          </div>

          <div class="flex justify-between w-full p-3 mt-3 border-t">
            <h3 class="font-semibold">
              Total  :
            </h3>
            <h3>{{ product.amount / 100 }} {{ product.currency }}</h3>
          </div>
        </div>
      </fieldset>
      <div class="flex items-center justify-center w-full">
        <button
          class="w-2/3 btn-primary"
          :disabled="isLoading"
          @click="payWithCard()"
        >
          {{ $t('playlist.payFee.btnPay') }}
        </button>
        <FormsLoaderComponent v-if="isLoading" class="ml-3" />
      </div>
    </div>
  </FormsModalComponent>
</template>

<script lang="ts" setup>
import { StripeCardNumberElement } from '@stripe/stripe-js'
import TrackSolar from '~~/core/interfaces/TrackSolar.interface'

const props = defineProps({

  trackAdded: {
    type: Object as () => TrackSolar,
    required: true
  },

  isPromote: {
    type: Boolean,
    required: true
  },

  billingState: {
    type: Boolean,
    default: true
  }

})

const emits = defineEmits(['addTracks', 'close', 'demoteTracks'])

const playlist = usePlaylist()
const { PROMOTE_PRODUCT_ID } = useRuntimeConfig()
const stripe = useStripe()
const { getProduct } = useProduct()
const { secondToMinutes } = useUtils()

const errorMessage = ref('')
const card = ref<StripeCardNumberElement>()
const isPayToPromote = ref<boolean>(false)
const isLoading = ref<boolean>(false)

const { pending, data: product, refresh } =
   await useAsyncData(() => getProduct(PROMOTE_PRODUCT_ID),
     {
       server: false
     })

const forcePromote = async () => {
  await refresh()
  isPayToPromote.value = true
  stripe.createCard().then((res) => {
    card.value = res.cardNumber
    card.value.mount('#card-number')
    res.cardExpiry.mount('#card-expiry')
    res.cardCvc.mount('#card-cardCvc')
  })
}

const payWithCard = async () => {
  isLoading.value = true
  let secret = ''
  if (props.isPromote) {
    secret = await stripe.createPayToPromote(props.trackAdded.franchiseId, props.trackAdded.title, props.trackAdded.artist, props.trackAdded.album, props.trackAdded.cover, props.trackAdded.duration, props.trackAdded.id)
  } else {
    secret = await stripe.createPayToDemote(props.trackAdded.franchiseId, props.trackAdded.title, props.trackAdded.artist, props.trackAdded.album, props.trackAdded.cover, props.trackAdded.duration, props.trackAdded.id)
  }
  stripe.payWithCard(secret, card.value!).then(
    (response) => {
      if (response.error) {
        errorMessage.value = response.error.message!
        new AlertBuilder('Error during payment !')
          .buildError()
      }
    }
  )
    .finally(() => (isLoading.value = false))
}
</script>
