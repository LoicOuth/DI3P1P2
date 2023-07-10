Feature: Franchise page

Background:  
  When I am in Franchise page

  Scenario: Get franchise
    Then I should view table with one element

  Scenario: Add franchise
    When I click on add button
    Then I should see add modal
    When I fill the fields
      And I click on created button
    Then I show an success creating alert
      
  Scenario: Delete franchise
    When I click on delete button
    Then I should see delete modal
    When I click on confirm button
    Then I show an success deleting alert

  Scenario: Update franchise
    When I click on update button
    Then I should see update modal
    When I edit the fields
      And I click on updated button
    Then I show an success updating alert