export default defineNuxtRouteMiddleware((to) => {
  const authUser = useAuthUser()
  const { PRODUCTION, KEYCLOACK_ADMIN_ROLE } = useRuntimeConfig()
  const localePath = useLocalePath()

  if (PRODUCTION) {
    if (to.name?.toString().includes(KEYCLOACK_ADMIN_ROLE)) {
      if (!authUser.value?.roles.includes(KEYCLOACK_ADMIN_ROLE)) {
        return navigateTo(localePath('/forbidden'))
      }
    }
  }
})
