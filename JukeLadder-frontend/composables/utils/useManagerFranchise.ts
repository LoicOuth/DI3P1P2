import Franchise from '~~/core/interfaces/Franchise.interface'

export default function () {
  return useState<Franchise | null>('franchise', () => null)
}
