using System;

namespace ResourceTemplate.Interface.TransferObjects
{
    public class Resource
    {
        public Guid ResourceId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}