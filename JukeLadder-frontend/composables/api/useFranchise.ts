import Franchise from '~/core/interfaces/Franchise.interface'

export default function () {
  const { t } = useI18n()
  const apiFetch = useApiFetch()
  const url = '/billing/franchise'

  const getAllFranchisees = async (): Promise<Array<Franchise>> => {
    return await apiFetch<Array<Franchise>>(url)
  }

  const getFranchise = async (franchiseId: string) : Promise<Franchise> => {
    return await apiFetch<Franchise>(`${url}/${franchiseId}`)
  }

  const getFranchiseByEmail = async (email: string) : Promise<Franchise> => {
    return await apiFetch<Franchise>(`${url}/email?email=${email}`)
  }

  const getCountFranchisees = async (): Promise<number> => {
    return await apiFetch<number>(`${url}/count`)
  }

  const deleteFranchise = async (franchiseId: string) => {
    return await apiFetch(`${url}/${franchiseId}`, {
      method: 'DELETE',

      async onResponse ({ response }) {
        if (response.ok) {
          new AlertBuilder(t('administrator.franchise.addPopup.alert.deleteSuccess'))
            .setTitle(t('alertComponent.title.deletedSuccess'))
            .buildSuccess()
        } else {
          new AlertBuilder(`${t('administrator.franchise.addPopup.alert.deletedError')} : ${JSON.stringify(response._data)}`)
            .buildError()
        }
      }
    })
  }

  const addFranchise = async (franchise: Franchise): Promise<string> => {
    return await apiFetch<string>(url, {
      method: 'POST',
      body: franchise,

      async onResponse ({ response }) {
        if (response.ok) {
          new AlertBuilder(t('administrator.franchise.addPopup.alert.createdSuccess'))
            .setTitle(t('alertComponent.title.createdSuccess'))
            .buildSuccess()
        } else {
          new AlertBuilder(`${t('administrator.franchise.addPopup.alert.createdError')} : ${JSON.stringify(response._data)}`)
            .buildError()
        }
      }
    })
  }

  const updateFranchise = async (franchise: Franchise, franchiseId: string): Promise<string> => {
    return await apiFetch<string>(`${url}/${franchiseId}`, {
      method: 'PUT',
      body: franchise,

      async onResponse ({ response }) {
        if (response.ok) {
          new AlertBuilder(t('administrator.franchise.addPopup.alert.updatedSuccess'))
            .setTitle(t('alertComponent.title.updatedSuccess'))
            .buildSuccess()
        } else {
          new AlertBuilder(`${t('administrator.franchise.addPopup.alert.updatedError')} : ${JSON.stringify(response._data)}`)
            .buildError()
        }
      }
    })
  }

  return {
    getFranchise,
    getFranchiseByEmail,
    getAllFranchisees,
    getCountFranchisees,
    deleteFranchise,
    addFranchise,
    updateFranchise
  }
}
