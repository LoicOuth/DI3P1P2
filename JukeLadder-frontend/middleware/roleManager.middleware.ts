export default defineNuxtRouteMiddleware((to) => {
  const authUser = useAuthUser()
  const { KEYCLOACK_MANAGER_ROLE, PRODUCTION } = useRuntimeConfig()
  const franchise = useManagerFranchise()
  const localePath = useLocalePath()

  if (PRODUCTION) {
    if (to.name?.toString().includes(KEYCLOACK_MANAGER_ROLE)) {
      if (!authUser.value?.roles.includes(KEYCLOACK_MANAGER_ROLE)) {
        return navigateTo(localePath('/forbidden'))
      }
    }

    if (franchise.value === null) {
      return navigateTo(localePath('/manager/load'))
    }
  }
})
