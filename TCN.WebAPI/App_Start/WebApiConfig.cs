using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using TCN.WebAPI.Filters;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;

namespace TCN.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Add handler to deal with preflight requests, this is the important part
            config.MessageHandlers.Add(new PreflightRequestsHandler()); // Defined above

            // No-Cache filter
            config.Filters.Add(new NoCacheHeaderFilter());

            // Mapping routes in attributes
            config.MapHttpAttributeRoutes();

            // Json formatter responses
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
        }
    }
}
