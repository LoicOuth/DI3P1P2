export default interface Token {
   email: string,
   preferred_username: string,
   exp: number,
   realm_access:{
      roles: Array<string>
   }
}
