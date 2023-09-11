
using Assignment2_Durham_Ryan.Data;
using Assignment2_Durham_Ryan.Interfaces;
using Assignment2_Durham_Ryan.Middleware;
using Assignment2_Durham_Ryan.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Assignment2_Durham_Ryan
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            string connectionName = string.Empty;

            if (args.Length == 0 ) { }

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("EFBasics"));
            });

            //builder.Services.AddSingleton<IValidator, Validator>();
            //builder.Services.AddScoped<IValidator, Validator>();
            //builder.Services.AddTransient<IValidator, Validator>();
            builder.Services.AddTransient<IValidator>(validator => new Validator(true));


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            /*******************************************
             *  Start JWT Security Configuration
             *  ****************************************/
            var secret = "MyVerySuperSecureSecretSharedKey";
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
            var issuer = "http://www.uc.edu/IT3047C";
            var audience = "WebServerApplicationDevelopment";

            builder.Services.AddAuthentication(OptionsBuilderConfigurationExtensions =>
            {
                OptionsBuilderConfigurationExtensions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = secretKey,

                    ValidateIssuer = true,
                    ValidIssuer = issuer,

                    ValidateAudience = true,
                    ValidAudience = audience,

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            /*****************************************
             *  End JWT Security Configuration
             *  **************************************/

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<QueryStringMiddleware>();

            app.UseQueryStrings();

            app.Use(async (context, next) =>
            {
                Console.WriteLine("Hello from In-line middleware");

                context.Response.ContentType = "application/json"; // Adjust content type as needed
                string customResponse = "{\"message\": \"Hello from In-line middleware\"}";
                await context.Response.WriteAsync(customResponse);

                await next(context);

                Console.WriteLine("Responding to request");
            });

            app.UseWhen((context) => context.Request.Query.Count > 0, (appBuilder) =>
            {
                app.UseMiddleware<AlertMiddleware>();

            });

            //app.Map("/api/departments", (context) =>
            //{
            //
            //});

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}