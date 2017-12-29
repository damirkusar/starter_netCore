using System;

namespace ResourceTemplate.Data.Models
{
    public class Resource
    {
        public Guid ResourceId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}