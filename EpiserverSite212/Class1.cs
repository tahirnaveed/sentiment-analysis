using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using Newtonsoft.Json;

namespace EpiserverSite212
{
    [InitializableModule]
    [ModuleDependency(typeof(FrameworkInitialization))]
    public class WebApiConfig : IInitializableModule
    {
        public static string ApiRoute = "api";

        public void Initialize(InitializationEngine context)
        {
            // Enable Web API routing
            GlobalConfiguration.Configure(config =>
            {
                // Attribute routing
                config.MapHttpAttributeRoutes();

                var formatters = GlobalConfiguration.Configuration.Formatters;
                var jsonFormatter = formatters.JsonFormatter;
                var settings = jsonFormatter.SerializerSettings;

                var enumConverter = new Newtonsoft.Json.Converters.StringEnumConverter();
                jsonFormatter.SerializerSettings.Converters.Add(enumConverter);

                settings.Formatting = Formatting.Indented;

                config.Formatters.Remove(config.Formatters.XmlFormatter);

                // config.MapHttpAttributeRoutes();

                config.Routes.MapHttpRoute(
                    name: "DefaultEpiApi",
                    routeTemplate: ApiRoute + "/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional });
            });
        }

        public void Uninitialize(InitializationEngine context)
        {

        }
    }

    public class ContentHelperController : ApiController
    {
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("please-analyse")]

        public IHttpActionResult Get(string sentence)
        {
           

            if (string.IsNullOrWhiteSpace(sentence))
            {
                return BadRequest(nameof(sentence));
            }

            var input = sentence.Trim();
            var result = new CognitvieServicesService().Analyse(sentence, "en");

            
            return Ok(new
            {
                input = sentence,
                score = result.Score,
            });
        }
    }
}