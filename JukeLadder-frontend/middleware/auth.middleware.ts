import useUtils from '~~/composables/utils/useUtils'

export default defineNuxtRouteMiddleware(() => {
  const authUser = useAuthUser()
  const { PRODUCTION } = useRuntimeConfig()
  const { checkCookie } = useUtils()
  const localePath = useLocalePath()

  if (PRODUCTION) {
    if (authUser.value == null) {
      const user = checkCookie()
      if (user != null) {
        authUser.value = user
      } else {
        return navigateTo(localePath('/'))
      }
    }
  }
})
