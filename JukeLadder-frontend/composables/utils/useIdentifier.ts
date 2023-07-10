import { load } from '@fingerprintjs/fingerprintjs'

export default function () {
  const identifier = useState<string | null>('identifier', () => null)

  const generateIdentifier = () => {
    load().then(fp => fp.get()).then((result) => {
      identifier.value = result.visitorId
    })
  }

  return {
    identifier,
    generateIdentifier
  }
}
