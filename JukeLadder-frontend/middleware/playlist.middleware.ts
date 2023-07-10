export default defineNuxtRouteMiddleware((to) => {
  const playlist = usePlaylist()
  const localePath = useLocalePath()

  if (playlist.value === null) {
    return navigateTo(localePath(`/playlist/${to.params.franchiseId}/load`))
  }
})
