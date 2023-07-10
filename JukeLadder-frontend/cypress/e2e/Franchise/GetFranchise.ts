import { Then, When } from '@badeball/cypress-cucumber-preprocessor'

When('I am in Franchise page', () => {
  cy.intercept('GET', 'franchise', { fixture: 'franchise.json' }).as(
    'get-franchise'
  )
  cy.intercept('POST', 'franchise', { fixture: 'franchise.json' }).as(
    'new-franchise'
  )
  cy.intercept('DELETE', '**/franchise/**', { fixture: 'franchise.json' }).as(
    'delete-franchise'
  )
  cy.intercept('PUT', '**/franchise/**', { fixture: 'franchise.json' }).as(
    'update-franchise'
  )

  cy.visit('http://localhost:8083/admin/franchisees').wait('@get-franchise')

  cy.url().should('include', '/franchisees')
})

Then('I should view table with one element', () => {
  cy.get('tbody').first().contains('test').should('exist')
  cy.matchImage()
})
