using NUnit.Framework;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;
using PeakPals_Project.Services;
using GraphQL;
using GraphQL.Client.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using PeakPals_Project.Models;
using GraphQL.Client;
using System.Text;
using System.Net;
using Moq.Protected;
using GraphQL.Client.Serializer.Newtonsoft;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

namespace NUnit_Tests;

[TestFixture]
public class OpenBetaApiServiceTests
{
    private Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private HttpClient _httpClient;
    private GraphQLHttpClient _graphQLClient;
    private OpenBetaApiService _service;
    private ILogger<OpenBetaApiService> _logger;

    [SetUp]
    public void SetUp()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_mockHttpMessageHandler.Object)
        {
            BaseAddress = new Uri("http://localhost/")
        };
        _graphQLClient = new GraphQLHttpClient(new GraphQLHttpClientOptions(), new NewtonsoftJsonSerializer(), _httpClient);
        _service = new OpenBetaApiService(_graphQLClient, _logger);
    }

    // Sets up the HTTP response with the provided content.
    private void SetupHttpResponse(string responseContent)
    {
        var httpContent = new StringContent(responseContent, Encoding.UTF8, "application/json");
        var httpResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = httpContent
        };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponse);
    }

    [Test]
    public async Task FindMatchingAreas_ReturnsAreas()
    {
        // Arrange
        var responseContent = @"
        {
            ""Data"": 
            {
                ""Areas"": 
                [
                    {
                        'Id': '1',
                        'Area_Name':'Area 1'
                    },
                    {
                        'Id': '2',
                        'Area_Name':'Area 2'
                    }
                ]
            }
        }";
        SetupHttpResponse(responseContent);

        // Act
        var result = await _service.FindMatchingAreas("query");


        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(2, result.Areas.Count);
        Assert.AreEqual("Area 1", result.Areas[0].Area_Name);
        Assert.AreEqual("Area 2", result.Areas[1].Area_Name);
    }

    [Test]
    public async Task FindAreaByID_ReturnsArea()
    {
        // Arrange
        var responseContent = @"
        {
            ""Data"": {
                ""Area"": {
                    ""Id"": ""1"",
                    ""Area_Name"": ""Area 1"",
                    ""Ancestors"": [],
                    ""Metadata"": {
                        ""Lat"": 2,
                        ""Lng"": 1

                    },
                    ""Content"": {
                        ""Description"": ""Description 1""
                    },
                    ""Children"": [],
                    ""Climbs"": []
                }
            }
        }";
        SetupHttpResponse(responseContent);

        // Act
        var data = await _service.FindAreaById("1");

        // Assert
        Assert.NotNull(data);
        Assert.That(data.Area.Area_Name, Is.EqualTo("Area 1"));
        Assert.That(data.Area.Content.Description, Is.EqualTo("Description 1"));
        Assert.That(data.Area.Children.Count, Is.EqualTo(0));
        Assert.That(data.Area.Climbs.Count, Is.EqualTo(0));
        Assert.That(data.Area.Metadata.Lat, Is.EqualTo(2));
        Assert.That(data.Area.Metadata.Lng, Is.EqualTo(1));
        Assert.That(data.Area.Ancestors.Count, Is.EqualTo(0));

    }

    [Test]
    public async Task FindMatchingAreas_EmptyQuery_ReturnsNull()
    {
        // Arrange
        string query = string.Empty;

        // Act
        var result = await _service.FindMatchingAreas(query);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public async Task FindAreasByID_EmptyQuery_ReturnsNull()
    {
        // Arrange
        string query = string.Empty;

        // Act
        var result = await _service.FindAreaById(query);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public async Task FindAreaAncestorsByID_EmptyQuery_ReturnsNull()
    {
        // Arrange
        string query = string.Empty;

        // Act
        var result = await _service.FindAncestorNameByAreaId(query);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public async Task FindClimbByID_EmptyQuery_ReturnsNull()
    {
        // Arrange
        string query = string.Empty;

        // Act
        var result = await _service.FindClimbById(query);

        // Assert
        Assert.IsNull(result);
    }

    

    
}