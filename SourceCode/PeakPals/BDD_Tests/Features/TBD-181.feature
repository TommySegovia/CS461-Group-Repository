@TommySegovia
Feature: TBD-181-Joined-Community-Groups-On-Profile

Adding a joined community groups tab to the profile page for users 

    #     Before running the tests you must
    #         1. Register user with 
                    #email:     john@example.com
                    #password:  Abc123!
    #         2. Run the AuthorizeTestUsers.sql script

    Scenario: User is on their profile page and has joined a community group
        Given I am a logged in user
        When I go to my profile as "john"
        And I have joined "The RockBoxx" community group
        Then I should see "The RockBoxx" community group displayed on my profile page as "john"