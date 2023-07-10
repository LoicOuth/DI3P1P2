import { Then, When } from '@badeball/cypress-cucumber-preprocessor'

When('I am in payment page', () => {
  cy.intercept('GET', 'fee', { fixture: 'feeParameter.json' }).as('get-feeParameter')
  cy.intercept('POST', 'fee', { fixture: 'feeParameter.json' }).as('add-feeParameter')

  cy.visit('http://localhost:8083/admin/payment').wait('@get-feeParameter')

  cy.url().should('include', '/payment')
})

Then('I should view two inputs with number of fee', () => {
  cy.get('input').first().should('have.value', 1000)
  cy.get('input').last().should('have.value', 10000)
  cy.matchImage()
})
