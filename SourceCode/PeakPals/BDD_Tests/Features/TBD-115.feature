@PatrickIannotti
Feature: TBD-115-LoggingClimbs

Allowing users to log climbing attempts on any rock climb and being able to see a log of those climbs with the details within


	Scenario: A user goes to a climb page and sees a button that allows them to log a climb
		Given I am a user
		And I access a climb page of my choosing
		And I see a button that allows me to log a climb
		When I click on the log climb button
		Then I will be either redirected to a new page or a window will appear
		And it will have forms available to fill out

	Scenario: A user logs a climb and is redirected back to the climb page
		Given I am a user
		And I access a climb page of my choosing
		And I clicked on the log climb button
		When I fill out each form with relevant data
		And I hit the submit button on the page
		Then I should be redirected back to the climb page
		And I should recieve a confirmation message

	Scenario: A user sees a listing of their logged climbs
		Given I am a user that has logged a climb before
		When I access the main locations page
		Then I should see a listing of the climbing logs I have done

	Scenario: A user sees a blank listing area when they have never logged a climb
		Given I am a different user that has never logged a climb
		When I access the main locations page
		Then I should a blank listings area that tells me to log a climb to get results

