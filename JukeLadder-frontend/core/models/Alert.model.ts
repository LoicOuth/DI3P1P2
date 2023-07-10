import { Guid } from 'guid-ts'
export default class Alert {
  public id: Guid
  public message: string
  public title: string
  public isSuccess: boolean
  public showtime: number

  constructor (id: Guid, message: string, title: string, isSuccess: boolean, showTime: number) {
    this.id = id
    this.message = message
    this.title = title
    this.isSuccess = isSuccess
    this.showtime = showTime
  }
}
