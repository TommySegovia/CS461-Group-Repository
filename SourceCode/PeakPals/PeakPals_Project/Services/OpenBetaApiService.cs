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

    
    public async Task<OpenBetaQueryResult> FindMatchingAreas(string userQuery)
    {
        var request =  new GraphQLRequest
        {
            Query = @"
                query FindMatchingAreas($userQuery: String!) 
                {
                    areas(filter: {area_name: {match: $userQuery}}) 
                    {
                        area_name
                        uuid
                        ancestors           
                        metadata
                        {
                            lat
                            lng
                        }
                    }
                }",
            Variables = new { userQuery }
        };

        var response = await _client.SendQueryAsync<OpenBetaQueryResult>(request);
        response.Data.Areas = response.Data.Areas.Take(8).ToList();
        return response.Data;      
    }

    public async Task<OBArea> FindAreaById(string idQuery)
    {
        var request = new GraphQLRequest
        {
            Query = @"
                query FindAreaById($idQuery: ID)
                {
                    area(uuid: $idQuery)
                    {
                        uuid
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
                            uuid
                            area_name
                            metadata
                            {
                                lat
                                lng
                            }
                        }
                        climbs
                        {
                            uuid
                            name
                            metadata
                            {
                                lat
                                lng
                            }
                        }
                    }
                }",
            Variables = new { idQuery }
        };

        var response = await _client.SendQueryAsync<OBArea>(request);
        return response.Data;
    }

    public async Task<OBArea> FindAncestorNameByAreaId(string idQuery)
    {
        var request = new GraphQLRequest
        {
            Query = @"
                query FindAreaById($idQuery: ID)
                {
                    area(uuid: $idQuery)
                    {
                        uuid
                        area_name
                    }
                }",
            Variables = new { idQuery }
        };

        var response = await _client.SendQueryAsync<OBArea>(request);
        return response.Data;
    }

}