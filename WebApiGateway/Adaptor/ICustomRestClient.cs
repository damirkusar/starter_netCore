using Microsoft.AspNetCore.Http;
using RestSharp;

namespace WebApiGateway.Adaptor
{
    public interface ICustomRestClient: IRestClient
    {
        RestRequest CreateRequest(HttpContext httpContext, string baseUrl, string resourceUrl, Method method);
    }
}