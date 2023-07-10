import { User } from '~~/core/models/User.model'

export default function () {
  return useState<User | null>('users', () => null)
}
