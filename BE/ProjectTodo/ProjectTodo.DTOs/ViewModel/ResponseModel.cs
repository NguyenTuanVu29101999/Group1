using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTodo.DTOs.ViewModel
{
    public class ResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public int StatusCode { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Data { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? NewId { get; set; }
    }
}
