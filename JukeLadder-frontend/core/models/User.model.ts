export class User {
  public username: string
  public email: string
  public roles: Array<string>
  public accessToken: string
  public accessTokenExpire: Date
  public refreshToken: string
  public refreshTokenExpire: Date

  constructor (username: string, email: string, roles: Array<string>, accessToken: string, accessTokenExpire: Date, refreshToken: string, refreshTokenExpire: Date) {
    this.username = username
    this.email = email
    this.roles = roles
    this.accessToken = accessToken
    this.accessTokenExpire = accessTokenExpire
    this.refreshToken = refreshToken
    this.refreshTokenExpire = refreshTokenExpire
  }
}
