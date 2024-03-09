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

    public OpenBetaApiService(GraphQLHttpClient client)
    {
        _client = client;
    }

    public async Task<IActionResult> FindMatchingAreas(string userQuery)
    {
        var request =  new GraphQLRequest
        {
            Query = @"
                query FindMatchingAreas($userQuery: String) 
                {
                    areas(filer: {area_name: {match: $userQuery}}) 
                    {
                        area_name
                        meta_data
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
                }"
        };

        var response = await _client.SendQueryAsync<OpenBetaQueryResult>(request);

        if (response.Errors != null && response.Errors.Any())
        {
            return new BadRequestObjectResult(response.Errors);
        }
        else
        {
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