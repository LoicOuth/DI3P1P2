import Product from '~~/core/interfaces/Product.interface'

export default function () {
  const { t } = useI18n()
  const apiFetch = useApiFetch()
  const url = '/billing/product'

  const getProduct = async (productId: string) : Promise<Product> => {
    return await apiFetch<Product>(`${url}/${productId}`)
  }

  const setProduct = async (productId: string, product: Product) : Promise<Product> => {
    return await apiFetch(`${url}/${productId}`, {
      method: 'PUT',
      body: {
        productId: product.productId,
        newprice: product.amount,
        newcurrency: product.currency
      },
      async onResponse ({ response }) {
        if (response.ok) {
          new AlertBuilder(`${t('manager.payment.alert.updatedSuccess')} ${product.amount / 100} ${product.currency} !`)
            .setTitle(t('alertComponent.title.updatedSuccess'))
            .buildSuccess()
        } else {
          new AlertBuilder(`${t('manager.payment.alert.updatedError')} : ${JSON.stringify(response._data)}`)
            .buildError()
        }
      }
    })
  }
  return {
    getProduct,
    setProduct
  }
}
