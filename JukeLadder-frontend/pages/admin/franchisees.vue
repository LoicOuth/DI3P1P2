<template>
  <div class="flex flex-col w-full">
    <AddFranchise @refresh:franchisees="refresh" />
    <FormsModalComponent v-if="deleteItem != null" title="Delete franchise" @close="deleteItem = null">
      <div class="flex flex-col">
        <h2>
          {{ $t('administrator.franchise.popupDelete.body') }}
          {{ deleteItem.name }} ?
        </h2>
        <button class="w-full mt-5 btn-danger" @click="handleDelete">
          {{ $t('administrator.franchise.popupDelete.btn') }}
        </button>
      </div>
    </FormsModalComponent>
    <div class="flex justify-center w-full mt-8">
      <table aria-describedby="franchises" des class="min-w-full border-collapse">
        <thead class="text-white bg-primary">
          <tr>
            <th scope="col" class="rounded-tl-md thead">
              {{ $t('administrator.franchise.table.name') }}
            </th>
            <th scope="col" class="thead">
              {{ $t('administrator.franchise.table.barManager') }}
            </th>
            <th scope="col" class="border-0 rounded-tr-md thead">
              {{ $t('administrator.franchise.table.action') }}
            </th>
          </tr>
        </thead>
        <tbody class="border-collapse bg-third">
          <tr v-if="pending">
            <FormsLoaderComponent />
          </tr>
          <tr v-for="item in franchisees" v-else :key="item.id.toString()" class="border-b">
            <td class="td">
              {{ item.name }}
            </td>
            <td class="td">
              {{ item.userId }}
            </td>
            <td class="flex justify-center td">
              <AddFranchise :franchise="item" @refresh:franchisees="refresh" />
              <img
                alt="delete"
                src="~/assets/icons/trash-solid.svg"
                class="h-6 cursor-pointer"
                @click="deleteItem = item"
              >
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script lang="ts" setup>
import Franchise from '../../core/interfaces/Franchise.interface'
useHead({
  title: 'Admin - Franchisees'
})

definePageMeta({
  layout: 'admin',
  middleware: ['auth-middleware', 'role-admin-middleware']
})

const deleteItem = ref<Franchise | null>(null)
const { getAllFranchisees, deleteFranchise } = useFranchise()

const handleDelete = () => {
  if (deleteItem.value) {
    deleteFranchise(deleteItem.value.id).then(() => {
      deleteItem.value = null
      refresh()
    })
  }
}

const {
  pending,
  data: franchisees,
  refresh
} = await useAsyncData(() => getAllFranchisees())

</script>
