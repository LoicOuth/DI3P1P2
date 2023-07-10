import { Then, When } from '@badeball/cypress-cucumber-preprocessor'

When('I access to the dynamic playlist', () => {
  cy.intercept('GET', '**/track/**', { fixture: 'tracks.json' }).as('get-tracks')
  cy.fixture('franchise').then((franchises) => {
    cy.intercept('GET', '**/franchise/**', franchises[0]).as('get-franchise')
  })

  cy.visit('http://localhost:8083/playlist/088b77d0-6166-4a61-944d-e1076b18548c').wait('@get-tracks').wait('@get-franchise')

  cy.url().should('include', '/playlist/088b77d0-6166-4a61-944d-e1076b18548c')
})

Then('I should view the tracks in the dynamic playlist', () => {
  cy.get('h2').first().should('contain', 'Harder, Better, Faster, Stronger')
})

Then('I should view the name of the bar', () => {
  cy.get('h1').first().should('contain.text', 'test')
  cy.matchImage()
})
