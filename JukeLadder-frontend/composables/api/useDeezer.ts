import Playlist from '~~/core/interfaces/Playlist.interface'

export default function () {
  const apiFetch = useApiFetch()
  const url = '/catalog/deezer'

  const getGenre = async () : Promise<Array<string>> => {
    return await apiFetch<Array<string>>(`${url}/genres`)
  }

  const getPlaylist = async (query: string) : Promise<Array<Playlist>> => {
    return await apiFetch<Array<Playlist>>(`${url}/search/playlist?query=${query}`)
  }

  return {
    getGenre,
    getPlaylist
  }
}
