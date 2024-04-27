@PatrickIannotti
Feature: TBD-108-LocationImages

Adding images to both climb and area pages to give users more visual context on what the climbs are and where to find them


    Scenario: Visiting an area page and viewing the images on the page
        Given I am a visitor of the site
        When I go to the area page I am interested in
        Then I will see images being displayed on the area page if they are any

    Scenario: Visting an a climb page and seeing the images on the page
        Given I am a visitor of the site
        When I go to the climb page I am interested in
        Then I will see images being displayed on the climb page if they are any

    Scenario: Going to an area or climb and clicking on the more images button and more images appearing
        Given I am a visitor of the site
        And I am on the area or climb page of my choice
        And there are images being displayed
        When I click on the button to display more images
        Then I will see a new window display pop-up to show the rest of the images

