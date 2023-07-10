export default function () {
  const runtimeConfig = useRuntimeConfig()
  const isDZLoad = useState<boolean>('isDZLoad', () => false)

  const initPlayer = (callbackProgress: Function, callbackEnd: Function) => {
    DZ.init({
      appId: runtimeConfig.DEEZER_APP_ID,
      channelUrl: runtimeConfig.DEEZER_CHANNEL_URL,
      player: {
        onload: () => {
          isDZLoad.value = true

          DZ.Event.subscribe('player_position', (arg: any) => {
            callbackProgress(arg[0], arg[1])
          })

          DZ.Event.subscribe('track_end', () => {
            callbackEnd()
          })
        }
      }
    })
  }

  return {
    initPlayer,
    isDZLoad
  }
}
