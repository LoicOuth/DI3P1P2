export default function () {
  const { t } = useI18n()
  const apiFetch = useApiFetch()
  const url = '/playlist/playlistState'

  const getPlaylistState = async (franchiseId: string) : Promise<boolean> => {
    return await apiFetch<boolean>(`${url}/${franchiseId}`)
  }

  const updatePlaylistState = async (franchiseId: string, state: boolean) : Promise<void> => {
    return await apiFetch(url, {
      method: 'PUT',
      body: {
        franchiseId,
        enable: state
      },

      async onResponse ({ response }) {
        if (!response.ok) {
          new AlertBuilder(`${t('manager.dashboard.alert.playlistState')} : ${JSON.stringify(response._data)}`)
            .buildError()
        }
      }
    })
  }

  return {
    getPlaylistState,
    updatePlaylistState
  }
}
