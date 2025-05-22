using Application.Persistence;
using Application.Services;
using Data.Context;
using Domain.Constants;
using Infrastructure.Configuration;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection ConfigureFakeLibrary(this WebApplicationBuilder builder)
    {
        #region Configuration Options

        builder.Services.Configure<ConnectionConfiguration>(builder.Configuration.GetSection(ConnectionConfiguration.Section));
        builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection(JwtConfiguration.Section));

        var connectionOptions = new ConnectionConfiguration ();
        var jwtOptions = new JwtConfiguration ();

        builder.Configuration.GetSection(ConnectionConfiguration.Section).Bind(connectionOptions);
        builder.Configuration.GetSection(JwtConfiguration.Section).Bind(jwtOptions);

        #endregion

        #region Database

        builder.Services.AddDbContext<FakeLibraryContext>(options => options.UseSqlServer(connectionOptions.FakeLibraryDB));

        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = false;
            options.Password.RequiredLength = 20;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
        })
        .AddEntityFrameworkStores<FakeLibraryContext>()
        .AddDefaultTokenProviders();

        #endregion

        #region Authentication & Authorization

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateActor = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                RequireExpirationTime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                ClockSkew = TimeSpan.Zero
            };
        
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Add(Security.TokenExpiredHeader, Security.HeaderValueTrue);
                    }
                    return Task.CompletedTask;
                }
            };
        });

        #endregion

        #region Services | Persistence Layer

        builder.Services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddScoped<IFakeLibraryUnitOfWork, FakeLibraryUnitOfWork>();
        builder.Services.AddTransient<ICredentialsGenerator, CredentialsGenerator>();
        builder.Services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
        builder.Services.AddScoped<IAuthManager, AuthManager>();

        #endregion

        return builder.Services;
    }
}
