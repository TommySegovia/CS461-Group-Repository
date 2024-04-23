@PatrickIannotti
Feature: ClimbingAreas

    Using the locations pages to search for an area using text input, clicking on the results, and viewing the area pages.

    #     Before running the tests you must

    Scenario: Going to the locations page, clicking the search button, and a sub-window appears
        Given I am a visitor on the site
        And I am on the locations search page
        When I click on the 'Search Locations' button
        Then I expect a display window to appear, allowing me to search for rock climbing areas

    Scenario: Searching for an area using the search bar, clicking on a result, and being redirected to the area page
        Given I am a visitor on the site
        And I am on the locations search page
        And I click on the 'Search Locations' button
        And a window appears allowing me to search for areas and climbs
        When I enter in 'Arrow Canyon' into the search bar
        And I click the search icon button
        Then I should recieve search results
        When I click on a search result
        Then I will be redirected to an area page

    Scenario: Attempt to search for an area using invalid text and getting back an validation error
        Given I am a visitor on the site
        And I am on the locations search page
        And I click on the 'Search Locations' button
        And a window appears allowing me to search for areas and climbs
        When I enter in the invalid text '&37!' into the search bar
        And I click the search icon button
        Then I should recieve an error that states the input is invalid

    Scenario: Viewing an area page and seeing basic information about the area and clicking on a sub-area and being redirected to that sub-area's page
        Given I am a visitor on the site
        And I am on the 'Arrow Canyon' area page that interests me
        Then I should see a page displaying basic information about the selected area including name, coordinates, and description
        When I click on a child box name that is a sub-area of this page's area
        Then I will be redirected to that sub-area page
