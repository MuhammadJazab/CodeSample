//-----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Gateway.Api.Controllers;

// <summary>
/// Defines the <see cref="HomeController" />.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return Ok("Gateway started successfully");
    }
}