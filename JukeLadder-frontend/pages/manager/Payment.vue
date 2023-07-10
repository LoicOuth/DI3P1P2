<template>
  <div class="flex flex-col items-center">
    <FormsLoaderComponent v-if="isLoading" :is-bar="true" class="w-full mb-5" />
    <fieldset class="flex items-center justify-center w-full p-10 border rounded-md shadow-md border-primary">
      <legend>{{ $t('manager.payment.promoteTitle') }}</legend>
      <FormsFloatInput v-model="promoteProduct.amount" :label="$t('manager.payment.price')" type="number" class="w-1/3" />
      <h2 class="mx-10">
        {{ (promoteProduct.amount / 100) }} {{ promoteProduct.currency }}
      </h2>
      <button class="flex btn-primary" :disabled="isLoading" @click="handleValid(PROMOTE_PRODUCT_ID, promoteProduct)">
        {{ $t('manager.payment.btn') }}
      </button>
    </fieldset>
    <fieldset class="flex items-center justify-center w-full p-10 mt-20 border rounded-md shadow-md border-primary">
      <legend>{{ $t('manager.payment.demoteTitle') }}</legend>

      <FormsFloatInput v-model="demoteProduct.amount" :label="$t('manager.payment.price')" type="number" class="w-1/3" />
      <h2 class="mx-10">
        {{ (demoteProduct.amount / 100) }} {{ promoteProduct.currency }}
      </h2>
      <button class="flex btn-primary" :disabled="isLoading" @click="handleValid(DEMOTE_PRODUCT_ID, demoteProduct)">
        {{ $t('manager.payment.btn') }}
      </button>
    </fieldset>
  </div>
</template>

<script setup lang="ts">
import Product from '~~/core/interfaces/Product.interface'
useHead({
  title: 'Manager - Payment'
})

definePageMeta({
  layout: 'manager',
  middleware: ['auth-middleware', 'role-manager-middleware']
})
const { DEMOTE_PRODUCT_ID, PROMOTE_PRODUCT_ID } = useRuntimeConfig()
const { getProduct, setProduct } = useProduct()
const isLoading = ref(false)

const { data: promoteProduct } =
  await useAsyncData('getPromote', () => getProduct(PROMOTE_PRODUCT_ID))

const { data: demoteProduct } =
  await useAsyncData('getDemote', () => getProduct(DEMOTE_PRODUCT_ID))

const handleValid = (productId: string, product: Product) => {
  isLoading.value = true
  setProduct(productId, product)
    .finally(() => { isLoading.value = false })
}

</script>
