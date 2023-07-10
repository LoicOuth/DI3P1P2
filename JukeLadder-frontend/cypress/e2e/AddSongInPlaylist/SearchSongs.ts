import { When, Then } from '@badeball/cypress-cucumber-preprocessor'

When('I am in add song page', () => {
  cy.wait(1000)
  cy.intercept('GET', '/track/suggestions**', { fixture: 'suggestions.json' }).as('get-suggestions')
  cy.intercept('GET', '/track**', { fixture: 'tracks.json' }).as('get-tracks')

  cy.visit('http://localhost:8083/playlist/088b77d0-6166-4a61-944d-e1076b18548c/tracks/add')

  cy.url().should('include', '/playlist/088b77d0-6166-4a61-944d-e1076b18548c/tracks/add')
})

When('I fill the search song field', () => {
  cy.get('input').first().type('Daft').wait('@get-suggestions')
})

Then('I should view the suggestion', () => {
  cy.get('div').get('.cursor-pointer').first().should('contain.text', 'Daft Punk')
})

When('I click on a suggestion', () => {
  cy.get('div').get('.cursor-pointer').first().click().wait('@get-tracks')
})

Then('I should view the list of the track', () => {
  cy.get('h2').last().should('contain.text', 'Harder, Better, Faster, Stronger')
  cy.wait(1000)
  cy.matchImage()
})
