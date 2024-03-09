using GraphQL;
using GraphQL.Client.Http;
using System;
using PeakPals_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace PeakPals_Project.Services;

public interface IOpenBetaApiService
{
    public Task<IActionResult> FindMatchingAreas(string userQuery);
    public Task<IActionResult> FindAreaById(string idQuery);

    // public Climb FindClimbById();

}