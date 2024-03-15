using GraphQL;
using GraphQL.Client.Http;
using System;
using PeakPals_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace PeakPals_Project.Services;

public interface IOpenBetaApiService
{
    public Task<OpenBetaQueryResult> FindMatchingAreas(string userQuery);
    public Task<OBArea> FindAreaById(string idQuery);
    public Task<OBArea> FindAncestorNameByAreaId(string idQuery);

    // public Climb FindClimbById();

}