Feature: Add song in playlist

Scenario: Search songs in catalog
   When I am in add song page
   And I fill the search song field
   Then I should view the suggestion 
   When I click on a suggestion
   Then I should view the list of the track

Scenario: Add song in playlist
   When I click on a track item
   Then I should view a popup
   When I click on add button
   Then my song is add in playlist