
using MentorshipHub.Api.ConfigurationToFile;
using MentorshipHub.EF.Data;
using MentorshipHub.Core.DTOHelpers;
using MentorshipHub.Core.IServices;
using MentorshipHub.Core.Models;
using MentorshipHub.EF.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MentorshipHub.EF.Migrations;

namespace MentorshipHub.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            // Connect to the database

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),o=> o.MigrationsAssembly(typeof(AppIdentityDbContext).Assembly.FullName));
            });

            // Add Identity and Handle Password Policy

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
            })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            // Add JWT AuthenticationServices
            builder.Services.Configure<Jwt>(builder.Configuration.GetSection("Jwt"));


            //JWT Settings
            builder.Services.AddAuthentication(options =>
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
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]?? string.Empty)),
                        ClockSkew = TimeSpan.Zero
                    };

                });
            builder.Services.AddScoped<TokenGenerator>();
            builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            //builder.Services.AddScoped<IAdminService, AdminService>();
            //builder.Services.AddScoped<IMenteeService, MenteeService>();
            builder.Services.AddScoped<IMentorService, MentorService>();
            builder.Services.AddScoped<IUserService,MentorshipHub.EF.Services.UserService>();


            // image configuration
            builder.Services.Configure<ImageSettings>(builder.Configuration.GetSection("ImageSettings"));
            builder.Services.Configure<TaskSettings>(builder.Configuration.GetSection("TaskSettings"));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
                RequestPath = "/Images"
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "TaskSubmissions")),
                RequestPath = "/TaskSubmissions"
            });


            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();


            app.MapControllers();

            app.Run();
        }
    }
}
