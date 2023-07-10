import { loadStripe, PaymentIntentResult, StripeCardNumberElement } from '@stripe/stripe-js'

export default function () {
  const runtimeConfig = useRuntimeConfig()
  const stripe = loadStripe(runtimeConfig.STRIPE_SECRET_KEY)
  const apiFetch = useApiFetch()

  const createCard = async () => {
    const elements = (await stripe)!.elements()
    const classes = 'border-b p-2 border-secondary text-lg mt-1 w-full'
    return {
      cardNumber: elements.create('cardNumber', {
        showIcon: true,
        classes: {
          base: classes
        }
      }),
      cardExpiry: elements.create('cardExpiry', {
        classes: {
          base: classes
        }
      }),
      cardCvc: elements.create('cardCvc', {
        classes: {
          base: classes
        }
      })
    }
  }

  const payWithCard = async (clientSecret: string, card: StripeCardNumberElement) : Promise<PaymentIntentResult> => {
    return (await stripe)!.confirmCardPayment(clientSecret, {
      payment_method: {
        card
      }
    })
  }

  const createPayToPromote = async (franchiseId: string, title: string, artist: string, album: string, cover: string, duration: number, deezerId: string) => {
    return await createPaymentIntend(runtimeConfig.PROMOTE_PRODUCT_ID, franchiseId, title, artist, album, cover, duration, deezerId)
  }

  const createPayToDemote = async (franchiseId: string, title: string, artist: string, album: string, cover: string, duration: number, deezerId: string) => {
    return await createPaymentIntend(runtimeConfig.DEMOTE_PRODUCT_ID, franchiseId, title, artist, album, cover, duration, deezerId)
  }

  const createPaymentIntend = async (productId: string, franchiseId: string, title: string, artist: string, album: string, cover: string, duration: number, deezerId: string) => {
    const response = await apiFetch<string>('/billing/payementIntend',
      {
        method: 'POST',
        body: {
          productId,
          franchiseId,
          title,
          artist,
          album,
          cover,
          duration,
          id: deezerId
        }
      })
    return response
  }

  return {
    createPayToPromote,
    createPayToDemote,
    createCard,
    payWithCard
  }
}
