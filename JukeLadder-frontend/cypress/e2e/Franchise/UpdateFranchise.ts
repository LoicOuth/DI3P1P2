import { Then, When } from '@badeball/cypress-cucumber-preprocessor'

When('I click on update button', () => {
  cy.get('[alt="update"]').first().click()
})

Then('I should see update modal', () => {
  cy.get('h1').contains('Update franchise').should('exist')
})

When('I edit the fields', () => {
  cy.get('input').first().type('test1')
})

When('I click on updated button', () => {
  cy.get('button').contains('Update').click().wait('@update-franchise')
})

Then('I show an success updating alert', () => {
  cy.get('[role="alert"]').get('p').contains('Updated successfully').should('exist').matchImage()
})
