import Franchise from '../interfaces/Franchise.interface'

export default class Alert {
  public playlistState: boolean
  public billingState: boolean
  public franchise: Franchise
  public trackMinutes: number

  constructor (playlistState: boolean, billingState: boolean, franchise: Franchise, trackMinutes: number = 0) {
    this.playlistState = playlistState
    this.billingState = billingState
    this.franchise = franchise
    this.trackMinutes = trackMinutes
  }
}
