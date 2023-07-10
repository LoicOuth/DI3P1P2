Feature: Payment page

Background:  
  When I am in payment page

   Scenario: Get fee parameter
      Then I should view two inputs with number of fee

   Scenario: Set fee parameter
      When I edit fields
         And I click on valid button
      Then I show an success set alert