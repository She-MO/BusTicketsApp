using BusTicketsApp.Server.Auth;
using BusTicketsApp.Server.Data;
using CookieCrumble;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BusTicketsApp.Tests;

public sealed class SchemaTests
{
    [Fact]
    public async Task SchemaChanged()
    {
        // Arrange & act
        var schema = await new ServiceCollection()
    .AddDbContext<ApplicationDbContext>()
    .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
    .AddSingleton<TokenProvider>()
    .AddGraphQLServer()
    .AddGlobalObjectIdentification()
    .AddMutationConventions()
    .AddDbContextCursorPagingProvider()
    .AddPagingArguments()
    .AddFiltering()
    .AddSorting()
    .AddServerTypes()
    .AddAuthorization()
    .AddProjections()
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
    })
    .BuildSchemaAsync();

        // Assert
        schema.MatchSnapshot();
    }
}