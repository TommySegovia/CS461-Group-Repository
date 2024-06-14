using GraphQL;
using GraphQL.Client.Http;
using System;
using PeakPals_Project.Models;
using Microsoft.AspNetCore.Mvc;

#nullable enable

namespace PeakPals_Project.Services;

public interface IOpenBetaApiService
{
    public Task<OpenBetaQueryResult?> FindMatchingAreas(string userQuery, int numResults = 8);
    public Task<OBArea?> FindAreaById(string idQuery);
    public Task<OBArea?> FindAncestorNameByAreaId(string idQuery);
    public Task<OBClimb?> FindClimbById(string idQuery);


}