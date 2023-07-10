export default function () {
  const { API_BASE_URL } = useRuntimeConfig()
  const autUser = useAuthUser()

  const apiFetch = $fetch.create({
    baseURL: API_BASE_URL,
    headers: new Headers(
      {
        Authorization: `Bearer ${autUser.value?.accessToken}`
      }
    )
  })

  return apiFetch
}
