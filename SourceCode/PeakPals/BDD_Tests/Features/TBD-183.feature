@TommySegovia
Feature: TBD-183-Recommended-Climbs

Adding recommended climbs tab to the report/analysis page

    #     Before running the tests you must
    #         1. Register user with 
                    #email:     john@example.com
                    #password:  Abc123!
    #         2. Run the AuthorizeTestUsers.sql script

    # Scenario: User has not logged any tests so they should be prompted to log enough tests to generate recommendations
    #     Given I am a logged in user
    #     And I have not logged any tests
    #     When I go to the analysis page
    #     And I click on the recommendations tab
    #     Then I should see a message that I need to log more tests to generate recommendations

    Scenario: User has logged tests and has recommendations
        Given I am a logged in user
        And I have logged tests
        When I go to the analysis page
        And I click on the recommendations tab
        Then I should see that I have recommendations