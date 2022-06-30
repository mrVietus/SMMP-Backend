using System;
using System.Collections.Generic;

namespace SMMP.Core.Models.Pipeline
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse()
        {
            Details = new Dictionary<string, object>();
        }

        public int Code { get; set; }
        public string DisplayMessage { get; set; }
        public string CorrelationId { get; set; }
        public IDictionary<string, object> Details { get; set; }
    }
}
