import { Then, When } from '@badeball/cypress-cucumber-preprocessor'

When('I edit fields', () => {
  cy.get('input').first().type('1001')
})

When('I click on valid button', () => {
  cy.get('button').contains('Valid').click().wait('@add-feeParameter')
})

Then('I show an success set alert', () => {
  cy.get('[role="alert"]').get('p').contains('Fee set successfully').should('exist').matchImage()
})
