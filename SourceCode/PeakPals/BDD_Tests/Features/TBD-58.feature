@TommySegovia
Feature: TBD-58

    Edit Page should allow user to edit their profile information

    #     Before running the tests you must
    #         1. Register user with 
                    #email:     john@example.com
                    #password:  Abc123!
    #         2. Run the AuthorizeTestUsers.sql script

    Scenario: Edit Profile page should contain fields for first name
    Given I am a logged in user
    When I go to my profile as "john"
    And I click on edit profile
    Then the form should contain field for first name

    Scenario: First name update
    Given I am a logged in user
    When I go to my profile as "john"
    And I click on edit profile
    And I update my first name to "John"
    Then I should see that my first name on my profile has been updated to "John"

    Scenario: City update
    Given I am a logged in user
    When I go to my profile as "john"
    And I click on edit profile
    And I update my city to "New York City"
    Then I should see that my city on my profile has been updated to "New York City"
