[Test]
public async Task FindNearbyClimbs_EmptyQuery_ReturnsNull()
{
    // Arrange
    string query = string.Empty;

    // Act
    // Using the new service method find climbs nearby the current climb.
    var result = await _service.FindNearbyClimbs(query);

    // Assert
    // Check if it returns null if the string query was empty.
    Assert.IsNull(result);
}

[Test]
public void FindAllNearbyClimbs_WhenCalledAndWhenGivenNullString_ReturnsWithBadRequest()
{
    // Arrange
    string query = null;

    // Act
    var response = _apiController.FindAllNearbyClimbs(query);

    // Using the api controller, call this new method with a null string and check to see if we get a bad request response.

    // Assert
    Assert.IsInstanceOf<BadRequestObjectResult>(response.Result.Result);

}

[Test]
public void FindAllNearbyClimbs_WhenCalledAndResponseIsNull_ReturnsWithNotFoundError()
{
    // Arrange
    string query = "192123123391023";


    // Act
    
    var response = _apiController.FindAllNearbyClimbs(query);

    // Using the api controller, call this new method with a invalid string that won't get any results
    // And see if it indeed returns a not found error.

    // Assert
    Assert.IsInstanceOf<NotFoundObjectResult>(response.Result.Result);
}

[Test]
public void FindAllNearbyClimbs_WhenCalledAndCorrect_ReturnsWithOkObject()
{

    // Arrange
    var mockService = new Mock<IOpenBetaApiService>();

    // make a new response object directly related to nearby climbs (needs implementation)
    // mockService.Setup(s => s.FindMatchingAreas(It.IsAny<string>(), 8)).ReturnsAsync( // new response object goes here );

    var apiController = new LocationsApiController(mockService.Object, _logger);
    string query = "hello";


    // Act
    var response = apiController.FindAllNearbyClimbs(query);

    // Using a setup that will return an OkObjectResult, check if the controller indeed returns that.

    // Assert
    Assert.IsInstanceOf<OkObjectResult>(response.Result.Result);
}
