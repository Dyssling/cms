using OnatrixApp.Services;

namespace OnatrixApp.Helpers
{
    public static class UmbracoBuilderExtensions
    {
        public static IUmbracoBuilder AddMyDependencies(this IUmbracoBuilder builder)
        {
            builder.Services.AddScoped<ServiceBusService>();
            return builder;
        }
    }
}
