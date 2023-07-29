//-----------------------------------------------------------------------
// <copyright file="GlobalUsings.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace EventBusRabbitMQ.Test;

/// <summary>
/// Defines the <see cref="RabbitMQConnectionTests " />.
/// </summary>
[TestClass]
public class RabbitMQConnectionTests
{
    /// <summary>
    /// Defines the connectionFactoryMock
    /// </summary>
    private Mock<IConnectionFactory>? connectionFactoryMock;

    /// <summary>
    /// Defines the loggerMock
    /// </summary>
    private Mock<ILogger<RabbitMQConnection>>? loggerMock;

    /// <summary>
    /// Defines the connectionMock
    /// </summary>
    private Mock<IConnection>? connectionMock;

    /// <summary>
    /// Defines the rabbitMQConnectionSut
    /// </summary>
    private RabbitMQConnection? rabbitMQConnectionSut;

    /// <summary>
    /// Defines the retryCount.
    /// </summary>
    private const int retryCount = 5;

    /// <summary>
    /// GroupCapacityControllerTests_Init
    /// </summary>
    [TestInitialize]
    public void RabbitMQConnectionTests_Init()
    {
        connectionFactoryMock = new();
        loggerMock = new();
        connectionMock = new();

        connectionFactoryMock?.Setup(x => x.CreateConnection()).Returns(connectionMock.Object);

        rabbitMQConnectionSut = new(connectionFactory: connectionFactoryMock?.Object!, logger: loggerMock?.Object!, retryCount: retryCount);

        Setup(true);
    }

    /// <summary>
    /// Setup
    /// </summary>
    private void Setup(bool isOpen)
    {
        var endpoint = new AmqpTcpEndpoint(new Uri(RabbitMQConnectionConstants.Host));
        connectionMock?.Setup(x => x.IsOpen).Returns(isOpen);
        connectionMock?.Setup(x => x.Endpoint).Returns(endpoint);

        connectionFactoryMock?.Setup(x => x.CreateConnection()).Returns(connectionMock?.Object!);
    }

    /// <summary>
    /// Given_Constructor_When_Initialized_Then_Create_RabbitMQConnection_Object
    /// </summary>
    [TestMethod]
    public void Given_Constructor_When_Initialized_Then_Create_RabbitMQConnection_Object()
    {
        // ARRANGE

        // ACT

        // ASSERT
        Assert.IsNotNull(rabbitMQConnectionSut);
    }

    /// <summary>
    /// Given_RabbitMQConnection_When_Called_TryConnect_Then_Connect_Successfully
    /// </summary>
    [TestMethod]
    public void Given_RabbitMQConnection_When_Called_TryConnect_Then_Connect_Successfully()
    {
        // ARRANGE

        // ACT
        var result = rabbitMQConnectionSut?.TryConnect();

        // ASSERT
        connectionFactoryMock?.Verify(cf => cf.CreateConnection(), Times.Once);
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Given_RabbitMQConnection_When_Called_CreateModel_Then_CreateModel_Successfully
    /// </summary>
    [TestMethod]
    public void Given_RabbitMQConnection_When_Called_CreateModel_Then_CreateModel_Successfully()
    {
        // ARRANGE
        Setup(true);

        // ACT
        rabbitMQConnectionSut?.TryConnect();
        rabbitMQConnectionSut?.CreateModel();

        // ASSERT
        connectionFactoryMock?.Verify(cf => cf.CreateConnection(), Times.Once);
        connectionMock?.Verify(c => c.IsOpen, Times.AtLeastOnce);
    }

    /// <summary>
    /// Given_RabbitMQConnection_When_Called_Dispose_Then_Dispose_Successfully
    /// </summary>
    [TestMethod]
    public void Given_RabbitMQConnection_When_Called_Dispose_Then_Dispose_Successfully()
    {
        // ARRANGE
        var endpoint = new AmqpTcpEndpoint(new Uri(RabbitMQConnectionConstants.Host));
        connectionMock?.Setup(x => x.IsOpen).Returns(true);
        connectionMock?.Setup(x => x.Endpoint).Returns(endpoint);

        connectionFactoryMock?.Setup(x => x.CreateConnection()).Returns(connectionMock?.Object!);


        // ACT
        rabbitMQConnectionSut?.TryConnect();
        rabbitMQConnectionSut?.Dispose();

        // ASSERT
        connectionMock?.Verify(x => x.Dispose(), Times.Once);
    }

    /// <summary>
    /// Given_RabbitMQConnection_When_Called_Dispose_Then_Dispose_Successfully
    /// </summary>
    [TestMethod]
    public void Given_RabbitMQConnection_When_Called_OnConnectionBlockedThen_Connect_Successfully()
    {
        // ARRANGE
        var endpoint = new AmqpTcpEndpoint(new Uri(RabbitMQConnectionConstants.Host));
        connectionMock?.Setup(x => x.IsOpen).Returns(true);
        connectionMock?.Setup(x => x.Endpoint).Returns(endpoint);

        connectionFactoryMock?.Setup(x => x.CreateConnection()).Returns(connectionMock?.Object!);

        // ACT
        var result = rabbitMQConnectionSut?.TryConnect();
        rabbitMQConnectionSut?.OnConnectionBlocked(default!, default!);

        // ASSERT
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Given_RabbitMQConnection_When_Called_Dispose_Then_Dispose_Successfully
    /// </summary>
    [TestMethod]
    public void Given_RabbitMQConnection_When_Called_OnCallbackException_Then_Connect_Successfully()
    {
        // ARRANGE
        var endpoint = new AmqpTcpEndpoint(new Uri(RabbitMQConnectionConstants.Host));
        connectionMock?.Setup(x => x.IsOpen).Returns(true);
        connectionMock?.Setup(x => x.Endpoint).Returns(endpoint);

        connectionFactoryMock?.Setup(x => x.CreateConnection()).Returns(connectionMock?.Object!);

        // ACT
        var result = rabbitMQConnectionSut?.TryConnect();
        rabbitMQConnectionSut?.OnCallbackException(default!, default!);

        // ASSERT
        Assert.IsTrue(result);
    }

    /// <summary>
    /// Given_RabbitMQConnection_When_Called_Dispose_Then_Dispose_Successfully
    /// </summary>
    [TestMethod]
    public void Given_RabbitMQConnection_When_Called_OnConnectionShutdown_Then_Connect_Successfully()
    {
        // ARRANGE
        Setup(true);

        // ACT
        var result = rabbitMQConnectionSut?.TryConnect();
        rabbitMQConnectionSut?.OnConnectionShutdown(null!, null!);

        // ASSERT
        Assert.IsTrue(result);
    }

    /// <summary>
    /// TestClean
    /// </summary>
    [TestCleanup]
    public void TestClean()
    {
        loggerMock = null!;
        connectionMock = null!;
        connectionFactoryMock = null!;
        rabbitMQConnectionSut = null!;
    }
}