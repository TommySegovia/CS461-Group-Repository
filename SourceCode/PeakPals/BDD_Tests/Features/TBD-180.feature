@TommySegovia
Feature: TBD-180-Tags-On-Logged-Climbs

Adding logged climbs to the profile page

    #     Before running the tests you must
    #         1. Register user with 
                    #email:     john@example.com
                    #password:  Abc123!
    #         2. Run the AuthorizeTestUsers.sql script

    Scenario: User logs a climb
        Given I am a logged in user
        When I go to a climb page
        And I log a climb with the following details
            | Attempts       | Rating | Grade | Tag    |
            | 1              | 5      | 12    | Crimpy |
        And I go to my profile as "john"
        Then I should see a climb logged on my profile page