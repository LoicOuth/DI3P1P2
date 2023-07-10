import { Guid } from 'guid-ts'
export default interface FeeParameter {
   id: Guid,
   minimum: number,
   maximum: number
}
