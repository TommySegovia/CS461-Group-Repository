@TommySegovia
Feature: TBD-178-Tags-On-Logged-Climbs

Adding the ability to add tags while logging a climb

    #     Before running the tests you must
    #         1. Register user with 
                    #email:     john@example.com
                    #password:  Abc123!
    #         2. Run the AuthorizeTestUsers.sql script

    Scenario: User logs a climb with a tag
        Given I am a logged in user
        When I go to a climb page
        And I log a climb with the following details
            | Attempts       | Rating | Grade | Tag    |
            | 1              | 5      | 12    | Crimpy |
        And I go to the locations page
        Then I should see the tag "Crimpy" on my logged climb