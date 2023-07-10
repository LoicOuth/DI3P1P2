import User from '~~/core/interfaces/User.interface'

export default function () {
  const apiFetch = useApiFetch()
  const urlUser = '/billing/user'

  const getAll = async () => {
    return await apiFetch<Array<User>>(urlUser)
  }

  return {
    getAll
  }
}
