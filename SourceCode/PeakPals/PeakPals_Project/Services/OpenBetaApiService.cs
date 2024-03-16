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

    public async Task<OpenBetaQueryResult> FindMatchingAreas(string userQuery, int numResults = 8)
    {
        if (string.IsNullOrEmpty(userQuery)) {
            return null;
        }

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
        response.Data.Areas = response.Data.Areas.Take(numResults).ToList();
        return response.Data;      
    }

    public async Task<OBArea> FindAreaById(string idQuery)
    {
        if (string.IsNullOrEmpty(idQuery)) {
            return null;
        }

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
        if (string.IsNullOrEmpty(idQuery)) {
            return null;
        }

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

    public async Task<OBClimb> FindClimbById(string idQuery)
    {
        if (string.IsNullOrEmpty(idQuery)) {
            return null;
        }

        var request = new GraphQLRequest
        {
            Query = @"
                query FindClimbById($idQuery: ID)
                {
                    climb(uuid: $idQuery)
                    {
                        uuid
                        name
                        ancestors
                        metadata {
                            lat
                            lng
                        }
                        content {
                            description
                        }
                    }
                }",
            Variables = new { idQuery }
        };

        var response = await _client.SendQueryAsync<OBClimb>(request);
        return response.Data;
    }

}