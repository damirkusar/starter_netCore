using System;

namespace WebApiGateway.TransferObjects
{
    public class ResourceResponse
    {
        public Guid ResourceId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}