//-----------------------------------------------------------------------
// <copyright file="GlobalUsings.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ERP.Authentication.UnitTests.Controllers;

/// <summary>
/// Defines the <see cref="AccountControllerTest" />.
/// </summary>
[TestClass]
public class AccountControllerTest
{
    /// <summary>
    /// The mediatorMock
    /// </summary>
    private Mock<IMediator>? mediatorMock;

    /// <summary>
    /// The AccountControllerSUT
    /// </summary>
    private AccountController? accountControllerSut;

    /// <summary>
    /// The loggerMock
    /// </summary>
    private Mock<ILogger<AccountController>>? loggerMock;

    /// <summary>
    /// The IHttpContextAccessor Mock
    /// </summary>
    private Mock<IHttpContextAccessor>? httpContextAccessorMock;

    /// <summary>
    /// AccountControllerTests_Init
    /// </summary>
    [TestInitialize]
    public void AccountControllerTests_Init()
    {
        mediatorMock = new Mock<IMediator>();
        loggerMock = new Mock<ILogger<AccountController>>();
        httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        accountControllerSut = new AccountController(mediatorMock.Object, loggerMock.Object, httpContextAccessorMock.Object);
    }

    /// <summary>
    /// Given_UsersCredentials_When_AccountLogin_Then_ReturnsToken
    /// </summary>
    [TestMethod]
    public void Given_UsersCredentials_When_Login_Then_ReturnsToken()
    {
        // ARRANGE
        var loginRequest = AccountFixtureData.LoginRequestFixture;
        var authenticationResponse = AccountFixtureData.AuthenticationResponseFixture;
        var cancelationToken = AccountFixtureData.CancellationTokenFixture;

        mediatorMock?.Setup(m => m.Send(loginRequest, cancelationToken)).ReturnsAsync(authenticationResponse).Verifiable();

        // ACT
        var response = accountControllerSut?.AccountLogin(loginRequest).Result;

        // ASSERT
        var responsePayload = response as AuthenticationResponse;
        Assert.AreEqual(authenticationResponse, responsePayload);
    }

    /// <summary>
    /// TestClean
    /// </summary>
    [TestCleanup]
    public void TestClean()
    {
        mediatorMock = null!;
        httpContextAccessorMock = null!;
        accountControllerSut = null!;
    }
}