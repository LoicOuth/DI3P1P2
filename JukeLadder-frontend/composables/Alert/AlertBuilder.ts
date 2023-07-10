import { Guid } from 'guid-ts'
import useAlert from './useAlert'
import Alert from '~~/core/models/Alert.model'

export class AlertBuilder {
  protected message: string
  protected title!: string
  protected isSuccess!: boolean
  protected showtime!: number

  constructor (message: string) {
    this.message = message
  }

  setTitle (title: string) : AlertBuilder {
    this.title = title
    return this
  }

  private setIsSuccess (isSuccess: boolean) : AlertBuilder {
    this.isSuccess = isSuccess
    return this
  }

  private setShowTime (showTime: number) : AlertBuilder {
    this.showtime = showTime
    return this
  }

  private build () : void {
    const alerts = useAlert()
    alerts.value.push(new Alert(
      Guid.newGuid(),
      this.message,
      this.title,
      this.isSuccess,
      this.showtime
    ))
  }

  buildSuccess () : void {
    this.setIsSuccess(true)
    this.setShowTime(3000)

    if (!this.title) { this.setTitle('Success') }

    this.build()
  }

  buildError () : void {
    this.setIsSuccess(false)
    this.setShowTime(3000)

    if (!this.title) { this.setTitle('Error') }

    this.build()
  }
}
