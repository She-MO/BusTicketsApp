using System.Text;
using BusTicketsApp.Server.Auth;
using BusTicketsApp.Server.Data;
using DataAnnotatedModelValidations;
//using BusTicketsApp.Server.Users.UserInputValidators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var  myAllowSpecificOrigins = "_myAllowSpecificOrigins";
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        ClockSkew = TimeSpan.Zero
                    };
            });
builder.Services.AddAuthorization();
//builder.Services.AddTransient<LogInUserInputValidator>();
//builder.Services.AddTransient<RegisterUserInputValidator>();
builder.Services.AddSingleton<TokenProvider>()
    .AddDbContext<ApplicationDbContext>(
        options => options.UseNpgsql("Host=127.0.0.1;Username=bus_tickets_app;Password=strong_secret_password"))
    .AddCors(options =>
    {
        options.AddPolicy(name: myAllowSpecificOrigins,
            policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();

            });
    })
    .AddGraphQLServer()
    .AddGlobalObjectIdentification()
    .AddMutationConventions()
    .AddDbContextCursorPagingProvider()
    .AddPagingArguments()
    .AddFiltering()
    .AddSorting()
    .AddServerTypes()
    .AddAuthorization()
    .AddDataAnnotationsValidator();

builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddHttpContextAccessor();


var app = builder.Build();
app.UseAuthentication();
//app.UseAuthorization();
app.UseCors(myAllowSpecificOrigins);
app.UseWebSockets();
app.MapGraphQL();


await app.RunWithGraphQLCommandsAsync(args);
