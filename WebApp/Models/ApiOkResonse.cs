using Newtonsoft.Json;

namespace WebApp.Models
{
    public class ApiOkResponse : ApiResponse
    {
        public ApiOkResponse(object result) : base(200)
        {
            this.Result = result;
        }

        [JsonProperty(PropertyName = "result")]
        public object Result { get; }
    }
}