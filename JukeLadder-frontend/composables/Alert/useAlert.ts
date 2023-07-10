import Alert from '~~/core/models/Alert.model'

export default function () {
  return useState<Array<Alert>>('alerts', () => [])
}
