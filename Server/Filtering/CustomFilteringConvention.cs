using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;

namespace BusTicketsApp.Server.Filtering;

public class CustomFilteringConvention : FilterConvention
{
    protected override void Configure(IFilterConventionDescriptor descriptor)
    {
        descriptor.AddDefaults();
        descriptor.Provider(
            new QueryableFilterProvider(
                x => x
                    .AddDefaultFieldHandlers()
                    .AddFieldHandler<QueryableStringInvariantEqualsHandler>()));
    }
}