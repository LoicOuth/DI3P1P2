export default function () {
  const { t } = useI18n()
  const apiFetch = useApiFetch()
  const url = '/billing/billingState'

  const getBillingState = async (franchiseId: string) : Promise<boolean> => {
    return apiFetch<boolean>(`${url}/${franchiseId}`)
  }

  const updateBillingState = async (franchiseId: string, state: boolean) : Promise<void> => {
    return await apiFetch(url, {
      method: 'PUT',
      body: {
        franchiseId,
        enable: state
      },

      async onResponse ({ response }) {
        if (!response.ok) {
          new AlertBuilder(`${t('manager.dashboard.alert.billingState')} : ${JSON.stringify(response._data)}`)
            .buildError()
        }
      }
    })
  }

  return {
    getBillingState,
    updateBillingState
  }
}
