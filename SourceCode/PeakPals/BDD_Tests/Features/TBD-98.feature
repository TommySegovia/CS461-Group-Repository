@PatrickIannotti
Feature: TBD-98-CommunityComments

Going to a community page should have comments and the user should be able to their own comments and see them displayed.

    Scenario: On a group page there should be a way to add comments and see them.
        Given that I am a user
        And I am viewing a community page that I joined
        And I see a comment section
        When I enter in text into the comment form
        And I hit a submit button
        Then the comment will be displayed on the page
