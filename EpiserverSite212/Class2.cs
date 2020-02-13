using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Rest;

namespace EpiserverSite212
{
    public class Class2
    {
    }
    class ApiKeyServiceClientCredentials : ServiceClientCredentials
    {
        private readonly string apiKey;

        public ApiKeyServiceClientCredentials(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            request.Headers.Add("Ocp-Apim-Subscription-Key", this.apiKey);
            return base.ProcessHttpRequestAsync(request, cancellationToken);
        }
    }

    public static class CognitvieServicesHelper
    {
        private static readonly string key = "TODO, YOUR KEY HERE";
        private static readonly string endpoint = "https://YOUR END POINT HERE.cognitiveservices.azure.com/";

        public static TextAnalyticsClient AuthenticateClient()
        {
            ApiKeyServiceClientCredentials credentials = new ApiKeyServiceClientCredentials(key);
            TextAnalyticsClient client = new TextAnalyticsClient(credentials)
            {
                Endpoint = endpoint
            };
            return client;
        }

    }

    public class CognitvieServicesService
    {
        public SentimentResult Analyse(string text, string lan)
        {
            var client = CognitvieServicesHelper.AuthenticateClient();
            SentimentResult result = client.Sentiment(text, lan);
            Console.WriteLine($"Sentiment Score: {result.Score:0.00}");

            return result;
        }
    }
}