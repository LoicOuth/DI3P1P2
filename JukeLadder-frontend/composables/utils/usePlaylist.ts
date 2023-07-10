import Playlist from '~~/core/models/Playlist.model'

export default function () {
  return useState<Playlist | null>('playlist', () => null)
}
