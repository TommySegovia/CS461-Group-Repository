@PatrickIannotti
Feature: TBD-143-ExpandedViewReports

Altering the reports page's visual design and adding an expanded view for viewing test history

	Scenario: Clicking on a test history card will show an exapnded view
		Given I am a user who has recorded tests
		And I am on the report page
		When I click on an individual test history card
		Then I will see a display that will show the test history in a more clear way

	Scenario: On the new expanded view page, users will see a graph and table
		Given I am a user who has recorded tests
		And I am on the report page
		When I click on an individual test history card
		Then I will see a graph and a table