import jwtDecode from 'jwt-decode'
import { User } from './../../core/models/User.model'
import Token from '~~/core/interfaces/Token.interface'

export default function () {
  const accessTokenCookie = useCookie('accessToken')
  const refreshTokenCookie = useCookie('refreshToken')

  const parseJwt = (accessToken: string): Token => {
    return jwtDecode(accessToken)
  }

  const checkCookie = (): User | null => {
    if (accessTokenCookie.value && refreshTokenCookie.value) {
      const parseAccessTokenCookie = parseJwt(accessTokenCookie.value)
      const parserefreshTokenCookie = parseJwt(refreshTokenCookie.value)

      return new User(
        parseAccessTokenCookie.preferred_username,
        parseAccessTokenCookie.email,
        parseAccessTokenCookie.realm_access.roles,
        accessTokenCookie.value,
        new Date(parseAccessTokenCookie.exp),
        refreshTokenCookie.value,
        new Date(parserefreshTokenCookie.exp)
      )
    }

    return null
  }

  const secondToMinutes = (second: number) : string => {
    return `${Math.floor(second / 60).toString()}:${(second - Math.floor(second / 60) * 60).toFixed(0)}`
  }

  const currentHostname = () : string => {
    return window.location.port ? `${window.location.hostname}:${window.location.port}` : window.location.hostname
  }

  return {
    parseJwt,
    checkCookie,
    secondToMinutes,
    currentHostname
  }
}
