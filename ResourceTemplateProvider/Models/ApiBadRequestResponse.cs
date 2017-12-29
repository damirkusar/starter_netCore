using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace ResourceProvider.Models
{
    public class ApiBadRequestResponse : ApiResponse
    {
        public ApiBadRequestResponse(ModelStateDictionary modelState) : base(400)
        {
            if (modelState.IsValid)
            {
                throw new ArgumentException("ModelState must be invalid", nameof(modelState));
            }

            this.Errors = modelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();
        }

        [JsonProperty(PropertyName = "errors")]
        public IEnumerable<string> Errors { get; }
    }
}