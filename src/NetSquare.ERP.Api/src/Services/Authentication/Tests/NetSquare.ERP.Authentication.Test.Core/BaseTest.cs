//-----------------------------------------------------------------------
// <copyright file="GlobalUsings.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Test.Core;

/// <summary>
/// Definition of BaseTest class
/// </summary>
/// <seealso cref="System.IDisposable" />
public class BaseTest : IDisposable
{
    /// <summary>
    /// The container builder
    /// </summary>
    public ContainerBuilder? containerBuilder;

    /// <summary>
    /// The container
    /// </summary>
    public Autofac.IContainer? container;

    /// <summary>
    /// Setups this instance.
    /// </summary>
    public void Setup()
    {
        var user = new ClaimsPrincipal(
            new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, "admin@calrom.com"),
                    new Claim(JwtRegisteredClaimNames.Jti, "032087F3-0DC3-408E-B510-D95D418C5BB8"),
                    new Claim(JwtRegisteredClaimNames.Email, "admin@calrom.com"),
                    new Claim(ClaimTypes.Role.ToString(), "araheem@calrom.com"),
                    new Claim(JwtRegisteredClaimNames.Sid, "D6E41081-ED5A-4C2B-B1BD-14A70B288089"),
                    new Claim(JwtRegisteredClaimNames.Name, "System Admin")
                }, "Basic"));

        var options = new DbContextOptionsBuilder<AuthenticationDbContext>()
           .UseInMemoryDatabase(Guid.NewGuid()
           .ToString())
           .Options;

        var logId = new Dictionary<object, object?>
        {
            {"logId", Guid.Parse("132087F3-0DC3-408E-B510-D95D418C5BB9") }
        };

        var moqHttpContextAccessor = new Mock<IHttpContextAccessor>();

        moqHttpContextAccessor.Setup(req => req.HttpContext!.User).Returns(user);
        moqHttpContextAccessor.Setup(req => req.HttpContext!.Items).Returns(logId);

        var mockedLogger = new Mock<ILogger<IAuthenticationDbContext>>();

        this.containerBuilder = new ContainerBuilder();

        this.containerBuilder.Register(c =>
        {
            var scope = c.Resolve<ILifetimeScope>();
            return new Mapper(
                c.Resolve<MapperConfiguration>(),
                scope.Resolve);
        }).As<IMapper>().SingleInstance();

        this.containerBuilder.RegisterModule(new RegisterRepositories());

        this.containerBuilder
            .RegisterType<AuthenticationDbContext>()
            .As<IAuthenticationDbContext>()
            .WithParameters(new List<Autofac.Core.Parameter>()
            {
                new NamedParameter("options", options),
                new NamedParameter("httpContextAccessor", moqHttpContextAccessor.Object),
                new NamedParameter("logger", mockedLogger.Object)
            });
    }

    /// <summary>
    /// Builds the container.
    /// </summary>
    public void BuildContainer()
    {
        this.container = this.containerBuilder?.Build();
        // create database
        var db = this.container?.Resolve<IAuthenticationDbContext>();
        db?.Database.EnsureCreated();
    }

    /// <summary>
    /// The CreateRepository.
    /// </summary>
    /// <typeparam name="TRepository"><see cref="CreateRepository{TRepository}"/></typeparam>
    /// <returns>return response repository</returns>
    protected TRepository CreateRepository<TRepository>() where TRepository : notnull
    {
        if (this.container == null)
        {
            throw new Exception("Container is null");
        }
        try
        {
            return this.container.Resolve<TRepository>();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Defines the in-memory CreateMapper
    /// </summary>
    /// <typeparam name="TMapper">Mapper Profile <see cref="CreateMapper{TMapper}"/></typeparam>
    /// <returns>Mapper in-memory <see cref="CreateMapper{TMapper}"/></returns>
    /// <exception cref="NullReferenceException"><see cref="NullReferenceException"/></exception>
    /// <exception cref="Exception"><see cref="Exception"/></exception>
    protected TMapper CreateMapper<TMapper>() where TMapper : notnull
    {
        if (this.container is null)
        {
            throw new NullReferenceException("The container is null");
        }
        try
        {
            return this.container.Resolve<TMapper>();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Dispose the container
    /// </summary>
    /// <param name="disposing">The disposing<see cref="bool"/></param>
    protected virtual void Dispose(bool disposing)
    {
        this.container?.Dispose();
        this.container = null;
    }

    /// <summary>
    /// Definition of Dispose Method
    /// </summary>
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }
}