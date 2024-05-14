@PatrickIannotti
Feature: TBD-114

    The location page will have a map loaded that the user can interact with as an additional way to search for areas.

    #     Before running the tests you must

    Scenario: Going to the locations page and a map is loaded
        Given I am a visitor on this site
        When I go to the location page
        Then I will see a map loaded on the page
