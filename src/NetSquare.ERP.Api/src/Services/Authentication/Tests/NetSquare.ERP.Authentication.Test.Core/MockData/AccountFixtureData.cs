//-----------------------------------------------------------------------
// <copyright file="GlobalUsings.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Test.Core.MockData;

/// <summary>
/// Defines the <see cref="BaseData" />.
/// </summary>
public class AccountFixtureData : BaseData
{
    /// <summary>
    /// Defines the UserId Fixture
    /// </summary>
    public const string UserIdFixture = "2d495f9d-695c-4c1c-bfc7-250e78540c18";

    /// <summary>
    /// Defines the AuthToken
    /// </summary>
    public const string AuthToken = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhZG1pbkBjYWxyb20uY29tIiwianRpIjoiZDZhZTMyODgtMjAzZS00YWI4LThjM2MtZmM3NmM2ZDlhZjMwIiwiZW1haWwiOiJhZG1pbkBjYWxyb20uY29tIiwic2lkIjoiOGU0NDU4NjUtYTI0ZC00NTQzLWE2YzYtOTQ0M2QwNDhjZGI5IiwibmFtZSI6IlN5c3RlbSBBZG1pbiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNjc3NjQ5MDMyLCJpc3MiOiJSZXZlbnVlTWFuYWdlbWVudFRvb2wiLCJhdWQiOiJSZXZlbnVlTWFuYWdlbWVudFRvb2xVc2VyIn0.5-5Xy_v8tfQAgyKzMAzQCfo3S1xIgVVPfKTopEDdXo0";


    /// <summary>
    /// Defines the CancellationToken Fixture
    /// </summary>
    public static readonly CancellationToken CancellationTokenFixture = new();

    /// <summary>
    /// Defines the AuthenticationRequest Fixture
    /// </summary>
    public static readonly AuthenticationRequest LoginRequestFixture = new()
    {
        Email = "admin@calrom.com",
        Password = "~Alpha123456"
    };

    public static readonly AuthenticationResponse AuthenticationResponseFixture = new(id: Guid.Parse("6e6b2dd5-cf44-4019-8d16-83ab457230f2"),
        email: "test@yahoo.com", token: AuthToken, userName:"test");
}