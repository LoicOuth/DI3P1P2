import Track from '@/core/interfaces/Track.interface'
import TrackSolar from '@/core/interfaces/TrackSolar.interface'
import SolrSuggestions from '~~/core/interfaces/SolrSuggestions.interface'

import { Vote } from '@/core/enums/Vote.enum'

export default function () {
  const { t } = useI18n()
  const apiFetch = useApiFetch()
  const { identifier } = useIdentifier()
  const urlPlaylist = '/playlist/track'
  const urlTrack = '/track'

  const getTracks = async (franchiseId: string) : Promise<Track[]> => {
    return await apiFetch<Track[]>(`${urlPlaylist}/${franchiseId}`)
  }

  const addTrack = async (track: TrackSolar) : Promise<void> => {
    await apiFetch(urlPlaylist, {
      method: 'POST',
      body: track,

      async onResponse ({ response }) {
        if (response.ok) {
          new AlertBuilder(t('manager.playlist.alert.trackSuccess'))
            .setTitle(t('alertComponent.title.addedSuccess'))
            .buildSuccess()
        }
      },
      async onRequestError ({ response }) {
        new AlertBuilder(`${t('manager.playlist.alert.trackError')} : ${JSON.stringify(response._data)}`)
          .buildError()
      }
    })
  }

  const voteTrack = async (trackId: string, action: Vote) : Promise<void> => {
    await apiFetch(urlPlaylist, {
      method: 'PUT',
      body: {
        trackId,
        identifier: identifier.value,
        action
      },

      async onRequestError ({ response }) {
        new AlertBuilder(JSON.stringify(response._data))
          .buildError()
      }
    })
  }

  const endTrack = async (trackId: string, franchiseId: string) : Promise<void> => {
    await apiFetch(`${urlPlaylist}/${trackId}/end`, {
      method: 'POST',
      body: {
        franchiseId
      }
    })
  }

  const deleteTrack = async (trackId: string) : Promise<void> => {
    await apiFetch(`${urlPlaylist}/${trackId}`, {
      method: 'DELETE',

      async onResponse ({ response }) {
        if (response.ok) {
          new AlertBuilder(t('manager.playlist.alert.trackDeleted'))
            .setTitle(t('alertComponent.title.deletedSuccess'))
            .buildSuccess()
        }
      }
    })
  }

  const updateProgressTrack = async (trackId: string, currentDuration: number) => {
    await apiFetch(`${urlPlaylist}/${trackId}`, {
      method: 'PUT',
      body: {
        newDuration: currentDuration
      }
    })
  }

  const getSolarSuggestions = async (query: string) : Promise<Array<SolrSuggestions>> => {
    if (query === '') { return [] }
    return await apiFetch<Array<SolrSuggestions>>(`${urlTrack}/suggestions?query=${query}`)
  }

  const getTrackWithSolarSuggessions = async (field: string, value: string, theme: string, franchiseId: string) : Promise<TrackSolar[]> => {
    if (field === '' || value === '') { return [] }
    return await apiFetch(`${urlTrack}?field=${field}&value=${value}&genre=${theme}&franchiseId=${franchiseId}`)
  }

  const getAllTracksByFranchise = async (franchiseId: string): Promise<Array<Track>> => {
    return await apiFetch(`${urlPlaylist}/${franchiseId}`)
  }

  const addTracksToCatalog = async (idPlaylist: string, franchiseId: string) => {
    return await apiFetch(urlTrack, {
      method: 'POST',
      body: {
        idPlaylist,
        franchiseId
      },

      async onResponse ({ response }) {
        if (response.ok) {
          new AlertBuilder(t('manager.catalog.alert.addedSuccess'))
            .setTitle(t('alertComponent.title.addedSuccess'))
            .buildSuccess()
        }
      },

      async onResponseError ({ response }) {
        new AlertBuilder(`${t('manager.catalog.alert.addedSuccess')} : ${JSON.stringify(response._data)}`)
          .buildError()
      }
    })
  }

  const setTrackPosition = async (newPosition: number, trackId: string) => {
    return await apiFetch(`${urlPlaylist}/${trackId}/position`, {
      method: 'PUT',
      body: {
        newPosition
      },

      async onResponse ({ response }) {
        if (response.ok) {
          new AlertBuilder(t('manager.playlist.alert.positionSuccess'))
            .setTitle(t('alertComponent.title.updatedSuccess'))
            .buildSuccess()
        }
      },

      async onResponseError ({ response }) {
        new AlertBuilder(`${t('manager.playlist.alert.positionError')} : ${JSON.stringify(response._data)}`)
          .buildError()
      }
    })
  }

  return {
    addTrack,
    voteTrack,
    getAllTracksByFranchise,
    getSolarSuggestions,
    getTrackWithSolarSuggessions,
    endTrack,
    deleteTrack,
    updateProgressTrack,
    addTracksToCatalog,
    setTrackPosition,
    getTracks
  }
}
