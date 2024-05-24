@PatrickIannotti
Feature: TBD-176

    This user story is about having pagination available for climbing logs.

 

    Scenario: The climbing log will have pagination buttons available
        Given I am a user with climbs logged
        When I move to the location page
        And I see that my climbing log has multiple entries
        Then I will see pagination buttons below the log

    Scenario: Clicking on the pagination buttons
        Given I am a user with climbs logged
        And I go to the location page
        And I see my climbing log that has multiple entries
        When I click on the pagination buttons below the log
        Then I will see that the entries have changed to a new page
