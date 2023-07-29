//-----------------------------------------------------------------------
// <copyright file="IRabbitMQPersistentConnection.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.EventBusRabbitMQ;

/// <summary>
/// Defines the <see cref="RabbitMQConnection" />.
/// </summary>
public sealed class RabbitMQConnection : IRabbitMQConnection
{
    /// <summary>
    /// Defines the connectionFactory.
    /// </summary>
    private readonly IConnectionFactory connectionFactory;

    /// <summary>
    /// Defines the logger.
    /// </summary>
    private readonly ILogger<RabbitMQConnection> logger;

    /// <summary>
    /// Defines the retryCount.
    /// </summary>
    private readonly int retryCount;

    /// <summary>
    /// Defines the connection.
    /// </summary>
    private IConnection connection;

    /// <summary>
    /// Defines the Disposed.
    /// </summary>
    private bool disposed;

    /// <summary>
    /// Defines the SyncRoot.
    /// </summary>
    public readonly object SyncRoot = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="RabbitMQConnection"/> class.
    /// </summary>
    /// <param name="connectionFactory">The connectionFactory<see cref="IConnectionFactory"/>.</param>
    /// <param name="logger">The logger<see cref="ILogger{DefaultRabbitMQPersistentConnection}"/>.</param>
    /// <param name="retryCount">The retryCount<see cref="int"/>.</param>
    public RabbitMQConnection(IConnectionFactory connectionFactory, ILogger<RabbitMQConnection> logger, int retryCount = 5)
    {
        this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.retryCount = retryCount;
    }

    /// <summary>
    /// Gets a value indicating whether IsConnected.
    /// </summary>
    public bool IsConnected => this.connection is { IsOpen: true } && !disposed;

    /// <summary>
    /// The CreateModel.
    /// </summary>
    /// <returns>The <see cref="IModel"/>.</returns>
    public IModel CreateModel()
    {
        if (!IsConnected)
        {
            throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
        }

        return connection.CreateModel();
    }

    /// <summary>
    /// The Dispose.
    /// </summary>
    public void Dispose()
    {
        if (this.disposed) return;

        this.disposed = true;

        this.connection.ConnectionShutdown -= OnConnectionShutdown;
        this.connection.CallbackException -= OnCallbackException;
        this.connection.ConnectionBlocked -= OnConnectionBlocked;
        this.connection.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// The TryConnect.
    /// </summary>
    /// <returns>The <see cref="bool"/>.</returns>
    public bool TryConnect()
    {
        this.logger.LogInformation("RabbitMQ Client is trying to connect");

        lock (SyncRoot)
        {
            var policy = RetryPolicy.Handle<SocketException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    this.logger.LogWarning(ex, "RabbitMQ Client could not connect after {TimeOut}s ({ExceptionMessage})", $"{time.TotalSeconds:n1}", ex.Message);
                }
            );

            policy.Execute(() =>
            {
                this.connection = connectionFactory
                        .CreateConnection();
            });

            if (IsConnected)
            {
                this.connection.ConnectionShutdown += OnConnectionShutdown;
                this.connection.CallbackException += OnCallbackException;
                this.connection.ConnectionBlocked += OnConnectionBlocked;

                logger.LogInformation("RabbitMQ Client acquired a persistent connection to '{HostName}' and is subscribed to failure events", this.connection.Endpoint.HostName);

                return true;
            }
            else
            {
                this.logger.LogCritical("FATAL ERROR: RabbitMQ connections could not be created and opened");

                return false;
            }
        }
    }

    /// <summary>
    /// The OnConnectionBlocked.
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/>.</param>
    /// <param name="e">The e<see cref="ConnectionBlockedEventArgs"/>.</param>
    public void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
    {
        if (this.disposed) return;

        this.logger.LogWarning("A RabbitMQ connection is shutdown. Trying to re-connect...");

        TryConnect();
    }

    /// <summary>
    /// The OnCallbackException.
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/>.</param>
    /// <param name="e">The e<see cref="CallbackExceptionEventArgs"/>.</param>
    public void OnCallbackException(object sender, CallbackExceptionEventArgs e)
    {
        if (this.disposed) return;

        this.logger.LogWarning("A RabbitMQ connection throw exception. Trying to re-connect...");

        TryConnect();
    }

    /// <summary>
    /// The OnConnectionShutdown.
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/>.</param>
    /// <param name="reason">The reason<see cref="ShutdownEventArgs"/>.</param>
    public void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
    {
        if (this.disposed) return;

        this.logger.LogWarning("A RabbitMQ connection is on shutdown. Trying to re-connect...");

        TryConnect();
    }
}