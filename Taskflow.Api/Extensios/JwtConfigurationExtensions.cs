using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Taskflow.Api.Services.Auth;

namespace Taskflow.Api.Extensions
{
    public static class JwtConfigurationExtensions
    {
        public static IServiceCollection AddJwtWithBlacklist(
            this IServiceCollection services,
            IConfiguration config)
        {
            var secretKey = config["Jwt:Key"];
            if (string.IsNullOrWhiteSpace(secretKey))
                throw new InvalidOperationException("Jwt:Key não configurado.");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(secretKey)
                        )
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            var tokenService =
                                context.HttpContext.RequestServices.GetRequiredService<ITokenService>();

                            string? token = null;
                            if (context.SecurityToken is JwtSecurityToken jwtToken)
                            {
                                token = jwtToken.RawData;
                            }

                            if (string.IsNullOrWhiteSpace(token))
                            {
                                var authHeader = context.HttpContext.Request.Headers["Authorization"].ToString();
                                if (!string.IsNullOrWhiteSpace(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                                {
                                    token = authHeader.Substring("Bearer ".Length).Trim();
                                }
                            }

                            if (string.IsNullOrWhiteSpace(token) || tokenService.IsTokenRevoked(token))
                            {
                                context.Fail("Token revoked or invalid.");
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
    }
}