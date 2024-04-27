@PatrickIannotti
Feature: ClimbAreaInfo

    The area and climb pages should contain additional information to give visitors more context for any place they wish to climb

  

    Scenario: Going to the area page and viewing additional information about the area
        Given I am a visitor to this site
        When I navigate to any area page
        Then I will see detailed information on the area if available
        And that will include: author metadata, total climbs, and organizations

    Scenario: Going to the climb page and viewing additional information about the area
        Given I am a visitor to this site
        When I navigate to any climb page
        Then I will see detailed information on the climb if available
        And that will include: location info, protection info, first ascent, and grade scale


