import { AlertBuilder } from '../Alert/AlertBuilder'
import { User } from './../../core/models/User.model'
import ResponseAuth from '~~/core/interfaces/ResponseAuth.interface'

export default function () {
  // cookies
  const accessTokenCookie = useCookie('accessToken')
  const refreshTokenCookie = useCookie('refreshToken')
  const localePath = useLocalePath()

  const runtimeConfig = useRuntimeConfig()
  const authUser = useAuthUser()
  const { parseJwt, currentHostname } = useUtils()

  const login = async (code: string) : Promise<void> => {
    await $fetch<ResponseAuth>(`${runtimeConfig.public.KEYCLOACK_URL}token`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/x-www-form-urlencoded'
      },
      body: new URLSearchParams({
        grant_type: 'authorization_code',
        redirect_uri: `http://${currentHostname()}${localePath('/auth/callback')}`,
        code,
        client_id: runtimeConfig.public.KEYCLOACK_CLIENT_ID,
        client_secret: runtimeConfig.public.KEYCLOACK_CLIENT_SECRET
      }),

      async onResponse ({ response }) {
        const accessTokenParse = parseJwt(response._data.access_token)
        const refreshTokenParse = parseJwt(response._data.refresh_token)
        const user = new User(
          accessTokenParse.preferred_username,
          accessTokenParse.email,
          accessTokenParse.realm_access.roles,
          response._data.access_token,
          new Date(accessTokenParse.exp),
          response._data.refresh_token,
          new Date(refreshTokenParse.exp)
        )

        if (user.roles.includes(runtimeConfig.public.KEYCLOACK_ADMIN_ROLE)) {
          accessTokenCookie.value = user.accessToken
          refreshTokenCookie.value = user.refreshToken

          authUser.value = user

          navigateTo(localePath('/admin/dashboard'))
        } else if (user.roles.includes(runtimeConfig.public.KEYCLOACK_MANAGER_ROLE)) {
          accessTokenCookie.value = user.accessToken
          refreshTokenCookie.value = user.refreshToken

          authUser.value = user

          navigateTo(localePath('/manager/dashboard'))
        } else {
          new AlertBuilder('Error on role').buildError()

          throw new Error('Error on role')
        }
      },

      async onRequestError () {
        new AlertBuilder('Error during login').buildError()
      }
    })
  }

  const logout = async () : Promise<void> => {
    await $fetch(`${runtimeConfig.public.KEYCLOACK_URL}logout`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/x-www-form-urlencoded',
        Authorization: `Bearer ${authUser.value?.accessToken}`
      },

      body: new URLSearchParams({
        refresh_token: authUser.value!.refreshToken,
        client_id: runtimeConfig.public.KEYCLOACK_CLIENT_ID,
        client_secret: runtimeConfig.public.KEYCLOACK_CLIENT_SECRET
      }),

      async onResponse () {
        authUser.value = null
        accessTokenCookie.value = null
        refreshTokenCookie.value = null
        navigateTo(localePath('/'))
      }
    })
  }

  const refreshToken = async () : Promise<void> => {
    await $fetch<ResponseAuth>(`${runtimeConfig.public.KEYCLOACK_URL}token`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/x-www-form-urlencoded'
      },
      body: new URLSearchParams({
        refresh_token: authUser.value!.refreshToken,
        client_id: runtimeConfig.public.KEYCLOACK_CLIENT_ID,
        grant_type: 'refresh_token'
      }),

      async onResponse ({ response }) {
        accessTokenCookie.value = response._data.access_token

        authUser.value!.accessToken = accessTokenCookie.value
      },

      async onRequestError () {
        navigateTo(localePath('/'))
      }
    })
  }

  return {
    login,
    logout,
    refreshToken
  }
}
