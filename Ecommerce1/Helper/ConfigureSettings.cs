using Ecommerce.Common.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Helper
{
    public static class ConfigureSettings
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // configure strongly typed settings objects
            var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));
            var appSettingsSection = configuration.GetSection("tokenSetting");

            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var jwtOption = appSettingsSection.Get<JwtIssuerOptions>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);


            // configure jwt authentication
            SecurityKey signingKey = new SymmetricSecurityKey(key);

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtOption.Issuer;
                options.Audience = jwtOption.Audience;
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                        ValidateAudience = true,
                        ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingKey,

                        RequireExpirationTime = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }
    }
}
