import { Then, When } from '@badeball/cypress-cucumber-preprocessor'

When('I click on delete button', () => {
  cy.get('[alt="delete"]').first().click()
})

Then('I should see delete modal', () => {
  cy.get('h1').contains('Delete franchise').should('exist')
})

When('I click on confirm button', () => {
  cy.get('button').contains('Confirm').first().click().wait('@delete-franchise')
})

Then('I show an success deleting alert', () => {
  cy.get('[role="alert"]').get('p').contains('Deleted successfully').should('exist').matchImage()
})
