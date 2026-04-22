using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SayyadCo.Application.Common.Interfaces;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Interfaces;
using SayyadCo.Application.Mappings;
using SayyadCo.Domain.Interfaces;
using SayyadCo.Domain.Interfaces.Repositories;
using SayyadCo.Infrastructure.Data;
using SayyadCo.Infrastructure.Identity;
using SayyadCo.Infrastructure.Repositories;
using SayyadCo.Infrastructure.Services.Auth;
using SayyadCo.Infrastructure.Services.Email;
using SayyadCo.Infrastructure.Services.GameAccess;
using System.Text;

namespace SayyadCo.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;

                options.Password.RequireDigit = false;

                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;

                options.User.RequireUniqueEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings!.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(AuthProfile).Assembly);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IGameAccessService, GameAccessService>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<ISectoinRepository, SectionRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IOtpRepository, OtpRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<ISectionGameRepository, SectionGameRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IUserGameRoleRepository, UserGameRoleRepository>();
            services.AddScoped<IAcademicYearRepository, AcademicYearRepository>();
            services.AddScoped<ISectionGameAcademicYearRepository, SectionGameAcademicYearRepository>();

            services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            services.AddHttpClient<IFacebookAuthService, FacebookAuthService>();

            services.AddHttpContextAccessor();

            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.Configure<GoogleAuthSettings>(configuration.GetSection("GoogleAuthSettings"));
            services.Configure<FacebookAuthSettings>(configuration.GetSection("FacebookAuthSettings"));

            return services;
        }
    }
}
