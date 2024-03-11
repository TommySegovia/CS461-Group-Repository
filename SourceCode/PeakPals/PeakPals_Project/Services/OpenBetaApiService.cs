using GraphQL;
using GraphQL.Client.Http;
using System;
using PeakPals_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace PeakPals_Project.Services;

public class OpenBetaApiService : IOpenBetaApiService
{
    private readonly GraphQLHttpClient _client;
    private readonly ILogger<OpenBetaApiService> _logger;

    public OpenBetaApiService(GraphQLHttpClient client, ILogger<OpenBetaApiService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<IActionResult> FindMatchingAreas(string userQuery)
    {
        var request =  new GraphQLRequest
        {
            Query = @"
                query FindMatchingAreas($userQuery: String!) 
                {
                    areas(filter: {area_name: {match: $userQuery}}) 
                    {
                        area_name
                        ancestors
                        metadata
                        {
                            lat
                            lng
                        }
                        children
                        {
                            area_name
                            metadata
                            {
                                lat
                                lng
                            }
                        }
                    }
                }",
            Variables = new { userQuery }
        };

        var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(20));
        GraphQLResponse<OpenBetaQueryResult> response = null;

        try {
            // actual request and response from API
            response = await _client.SendQueryAsync<OpenBetaQueryResult>(request, cancellationTokenSource.Token);
        } 
        catch (OperationCanceledException) {
            return new StatusCodeResult(StatusCodes.Status408RequestTimeout);
        }

        if (response.Errors != null && response.Errors.Any())
        {
            _logger.LogError($"GraphQL response contained errors: {string.Join(", ", response.Errors.Select(e => e.Message))}");
            return new BadRequestObjectResult(response.Errors);
            
        }
        else
        {
            response.Data.Areas = response.Data.Areas.Take(20).ToList();
            return new OkObjectResult(response.Data);
        }
    }

    public async Task<IActionResult> FindAreaById(string idQuery)
    {
        var request = new GraphQLRequest
        {
            Query = @"
                query FindAreaById($idQuery: String)
                {
                    area(uuid: $idQuery)
                    {
                        id
                        area_name
                        ancestors
                        metadata
                        {
                            lat
                            lng
                        }
                        content 
                        {
                            description
                        }
                        children
                        {
                            id
                            area_name
                            metadata
                            {
                                lat
                                lng
                            }
                        }
                        climbs
                        {
                            id
                            name
                            metadata
                            {
                                lat
                                lng
                            }
                        }
                    }
                }"
        };

        var response = await _client.SendQueryAsync<OBArea>(request);

        if (response.Errors != null && response.Errors.Any())
        {
            return new BadRequestObjectResult(response.Errors);
        }
        else
        {
            return new OkObjectResult(response.Data);
        }
    }

}