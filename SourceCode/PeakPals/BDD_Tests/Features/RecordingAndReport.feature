@TommySegovia
Feature: RecordingAndReport

    Logging tests and being able to see that the previous tests have been successful logged to the report page

    #     Before running the tests you must
    #         1. Register user with 
                    #email:     john@example.com
                    #password:  Abc123!
    #         2. Run the AuthorizeTestUsers.sql script

    Scenario: Logging a pullup test with an invalid bodyweight and valid pullup weight
        Given I am a logged in user
        When I am on the "Record" page
        And I enter -10 into the bodyweight field
        And I enter 20 into the pullup weight field
        And I press the record button
        Then I should see that the form was not submitted

    Scenario: Logged entry should be displayed on the report page
        Given I am a logged in user
        When I have previously logged a pullup test
        And I am on the "Report" page
        Then I should see my previously logged pullup tests on the report page

    Scenario: Pullup test input weight field should be displayed
        Given I am a logged in user
        When I am on the "Record" page
        Then I should see a pullup test added weight input field

    Scenario: Logging a pullup test with a valid bodyweight and valid pullup weight
        Given I am a logged in user
        When I am on the "Record" page
        And I enter 150 into the bodyweight field
        And I enter 20 into the pullup weight field
        And I press the record button
        Then my test should be logged