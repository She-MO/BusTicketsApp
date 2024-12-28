using System.Text;
using AppAny.HotChocolate.FluentValidation;
using BusTicketsApp.Server.Auth;
using BusTicketsApp.Server.Data;
using BusTicketsApp.Server.Filtering;
using BusTicketsApp.Server.Trips.TripInputValidators;
using FluentValidation;
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
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsAdmin",
        policy => policy.RequireClaim("Role", "Admin"));
    options.AddPolicy("IsManagerOrAdmin",
        policy => policy.RequireClaim("Role", "Admin", "Manager"));
});
//builder.Services.AddTransient<LogInUserInputValidator>();
//builder.Services.AddTransient<RegisterUserInputValidator>();
builder.Services.AddSingleton<TokenProvider>()
    .AddDbContext<ApplicationDbContext>(
        options => options.UseNpgsql(builder.Configuration["Database:DefaultConnectionString"]!))
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
    //.AddProjections()
    //.AddDataAnnotationsValidator()
    .AddFluentValidation(options =>
    {
        options.UseDefaultErrorMapper();
    })
    .ModifyCostOptions(options =>
    {
        options.MaxFieldCost = 20_000;
        options.MaxTypeCost = 10_000;
        options.EnforceCostLimits = true;
        options.ApplyCostDefaults = true;
        options.DefaultResolverCost = 10.0;
    })
    .ModifyPagingOptions(options =>
    {
        options.IncludeTotalCount = true;
        options.MaxPageSize = 100;
    });
builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddValidatorsFromAssemblyContaining<AddTripInputValidator>();


var app = builder.Build();
app.UseAuthentication();
//app.UseAuthorization();
app.UseCors(myAllowSpecificOrigins);
app.UseWebSockets();
app.MapGraphQL();


await app.RunWithGraphQLCommandsAsync(args);
