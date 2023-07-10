import { Then, When } from '@badeball/cypress-cucumber-preprocessor'

When('I click on add button', () => {
  cy.get('button').contains('Create franchise').first().click()
})

Then('I should see add modal', () => {
  cy.get('h1').contains('Add franchise').should('exist')
})

When('I fill the fields', () => {
  cy.get('input').first().type('test')
  cy.get('input').last().type('test')
})

When('I click on created button', () => {
  cy.get('button').contains('create').click().wait('@new-franchise')
})

Then('I show an success creating alert', () => {
  cy.get('[role="alert"]').get('p').contains('Created successfully').should('exist').matchImage()
})
