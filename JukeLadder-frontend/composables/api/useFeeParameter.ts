import { Guid } from 'guid-ts'
import useApiFetch from '../utils/useApiFetch'
import FeeParameter from '~~/core/interfaces/FeeParameter.interface'

export default function () {
  const { t } = useI18n()
  const apiFetch = useApiFetch()
  const url = '/billing/fee'

  const getFeeParameter = async () : Promise<FeeParameter> => {
    return await apiFetch<FeeParameter>(url)
  }

  const setFeeParameter = async (min: number, max: number) : Promise<Guid> => {
    return await apiFetch<Guid>(url, {
      method: 'POST',
      body: {
        min,
        max
      },

      async onResponse ({ response }) {
        if (response.ok) {
          new AlertBuilder(t('administrator.payment.alert.updatedSuccess'))
            .setTitle(t('alertComponent.title.updatedSuccess'))
            .buildSuccess()
        } else {
          new AlertBuilder(`${t('administrator.payment.alert.updatedError')} : ${JSON.stringify(response._data)}`)
            .buildError()
        }
      },

      async onRequestError () {

      }
    })
  }

  return {
    getFeeParameter,
    setFeeParameter
  }
}
