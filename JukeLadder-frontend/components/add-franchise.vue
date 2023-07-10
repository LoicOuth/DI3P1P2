<template>
  <div>
    <button v-if="!props.franchise" class="btn-primary" @click="showAddFranchiseModal = true">
      {{ $t('administrator.franchise.createFranchise') }}
    </button>
    <img
      v-else
      alt="update"
      src="~/assets/icons/pen-solid.svg"
      class="h-6 mr-5 cursor-pointer"
      @click="showAddFranchiseModal = true"
    >
    <FormsModalComponent
      v-if="showAddFranchiseModal"

      :title="!props.franchise ? $t('administrator.franchise.createFranchise') :
        `${ $t('administrator.franchise.updateFranchise') } : ${formData.name}`"
      @close="closeModal"
    >
      <div class="flex flex-col mt-5">
        <div v-if="error" class="text-error">
          {{ error }}
        </div>
        <FormsFloatInput v-model="formData.name" :label="$t('administrator.franchise.addPopup.nameInput')" />
        <FormsAutocompleteInput v-model="formData.userId" :suggestions="users" option-key="email" option-label="email" :label="$t('administrator.franchise.addPopup.chooseInput')" />

        <div class="flex items-center mt-5">
          <button v-if="!props.franchise" class="w-full btn-primary" :disabled="isLoading" @click="handleCreate">
            {{ $t('administrator.franchise.createFranchise') }}
          </button>
          <button v-else class="w-full btn-primary" :disabled="isLoading" @click="handleUpdate">
            {{ $t('administrator.franchise.updateFranchise') }}
          </button>
          <FormsLoaderComponent v-if="isLoading" class="ml-5" />
        </div>
      </div>
    </FormsModalComponent>
  </div>
</template>

<script lang="ts" setup>
import Franchise from '~~/core/interfaces/Franchise.interface'

const { addFranchise, updateFranchise } = useFranchise()
const { getAll } = useUser()
const users = ref([])
const showAddFranchiseModal = ref(false)
const isLoading = ref(false)
const error = ref(null)

const emits = defineEmits(['refresh:franchisees'])
const props = defineProps({
  franchise: {
    type: Object as () => Franchise,
    default: null
  }
})

const closeModal = () => {
  if (!props.franchise) { formData.name = formData.userId = '' }
  showAddFranchiseModal.value = false
}

const formData = reactive({
  name: '',
  userId: '',
  theme: ''
})

const getAllUser = () => {
  getAll().then((response) => {
    users.value = response
  })
}

const handleCreate = () => {
  error.value = null
  isLoading.value = true

  if (users.value.some(el => el.email === formData.userId)) {
    addFranchise(formData as Franchise)
      .then(() => {
        emits('refresh:franchisees')
        closeModal()
      })
      .finally(() => { isLoading.value = false })
  } else {
    error.value = 'Choose a valid user !'
    isLoading.value = false
  }
}

const handleUpdate = () => {
  error.value = null
  isLoading.value = true

  if (users.value.some(el => el.email === formData.userId)) {
    updateFranchise(formData as Franchise, props.franchise.id)
      .then(() => {
        emits('refresh:franchisees')
        closeModal()
      })
      .finally(() => { isLoading.value = false })
  } else {
    error.value = 'Choose a valid user !'
    isLoading.value = false
  }
}

onMounted(async () => {
  await getAllUser()
  if (props.franchise) {
    formData.name = props.franchise.name
    formData.userId = props.franchise.userId
    formData.theme = props.franchise.theme
  }
})
</script>
