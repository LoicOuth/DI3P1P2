export default interface Track {
  id: string,
  franchiseId: string,
  deezerId: string,
  title: string,
  artist: string,
  album: string,
  cover: string,
  duration: number,
  upvotes: Array<string>,
  downvotes: Array<string>,
  isReading: boolean,
  currentDuration: number,
  datePromote: Date,
  position: number
}
