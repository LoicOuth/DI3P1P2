import { When, Then } from '@badeball/cypress-cucumber-preprocessor'

When('I click on a track item', () => {
  cy.get('div').get('.cursor-pointer').last().click()
})

Then('I should view a popup', () => {
  cy.get('div').get('.btn-primary').first().should('contain', 'Add')
})

When('I click on add button', () => {
  cy.intercept('POST', '/playlist/track', { fixture: 'tracks.json' }).as('new-track')

  cy.get('div').get('.btn-primary').first().click().wait('@new-track')
})

Then('my song is add in playlist', () => {
  cy.get('[role="alert"]').get('p').contains('Track added successfully').should('exist').matchImage()
})
