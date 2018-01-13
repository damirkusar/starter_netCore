using System;
using Microsoft.AspNetCore.Http;
using RestSharp;

namespace WebApiGateway.Adaptor
{
    public class CustomRestClient: RestClient, ICustomRestClient
    {
        public RestRequest CreateRequest(HttpContext httpContext, string baseUrl, string resourceUrl, Method method)
        {
            this.BaseUrl = new Uri($"{baseUrl}/api");
            var request = new RestRequest(resourceUrl, method);
            var auth = httpContext.Request.Headers["Authorization"];
            request.AddHeader("Authorization", auth);

            return request;
        }
    }
}