namespace IdentityProvider.Models
{
    public class ApiInternalServerErrorResponse : ApiResponse
    {
        public ApiInternalServerErrorResponse(string message) : base(500, message)
        {
        }
    }
}