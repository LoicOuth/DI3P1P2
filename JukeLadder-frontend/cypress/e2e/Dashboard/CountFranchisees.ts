import { Then, When } from '@badeball/cypress-cucumber-preprocessor'
import Franchise from '~~/core/interfaces/Franchise.interface'

When('I am in Dashboard page', () => {
  cy.fixture('franchise').then((franchises: Array<Franchise>) => {
    cy.intercept('GET', '**/franchise/count', franchises.length.toString()).as('get-count-franchisees')
  })

  cy.visit('http://localhost:8083/admin/dashboard').wait('@get-count-franchisees')
  cy.wait(1000)

  cy.url().should('include', '/dashboard')
})

Then('I should view count of franchisees', () => {
  cy.get('h1').last().contains('1').should('exist')
  cy.matchImage()
})
