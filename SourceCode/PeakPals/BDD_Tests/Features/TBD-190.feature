@PatrickIannotti
Feature: TBD-190-CommunityClimbLogs

Going to a community page should have a list of climbing attempts if they have them and I should be able to add my own.

    Scenario: On a group page there should be a way to add comments and see them.
        Given that I am a user of this site
        And I am viewing a community group page that I am owner of
        And I see a climbing log that doesnt have my climb attempt
        When I go to log my climb attempt
        And I return back to my community group page
        Then the climbing log should have my climb attempt
