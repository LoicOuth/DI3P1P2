<template>
  <div class="flex justify-center">
    <FormsLoaderComponent v-if="pending" />
    <div v-else-if="feeParameter != null" class="flex items-center w-full">
      <FormsFloatInput v-model="feeParameter.minimum" :label="$t('administrator.payment.min')" type="number" class="flex-1" />
      <FormsFloatInput v-model="feeParameter.maximum" :label="$t('administrator.payment.max')" type="number" class="flex-1 ml-5 mr-5" />

      <div class="flex mx-10">
        <h2>{{ $t('administrator.payment.from') }} {{ (feeParameter.minimum / 100 ) }} EUR</h2>
        <h2 class="ml-5">
          {{ $t('administrator.payment.to') }} {{ (feeParameter.maximum / 100 ) }} EUR
        </h2>
      </div>
      <button class="flex-1 btn-primary" @click="handleValid">
        {{ $t('administrator.payment.btn') }}
      </button>
      <FormsLoaderComponent v-if="isLoading" class="mx-3" />
    </div>
  </div>
</template>
<script lang="ts" setup>
useHead({
  title: 'Admin - Payment'
})

definePageMeta({
  layout: 'admin',
  middleware: ['auth-middleware', 'role-admin-middleware']
})

const { getFeeParameter, setFeeParameter } = useFeeParameter()
const isLoading = ref(false)

const { pending, data: feeParameter, refresh } =
   await useAsyncData(() => getFeeParameter())

const handleValid = () => {
  isLoading.value = true
  setFeeParameter(feeParameter.value.minimum, feeParameter.value.maximum)
    .then(async () => {
      await refresh()
    })
    .finally(() => { isLoading.value = false })
}

</script>
