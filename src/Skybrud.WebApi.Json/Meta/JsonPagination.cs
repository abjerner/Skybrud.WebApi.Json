using Newtonsoft.Json;

namespace Skybrud.WebApi.Json.Meta {

    public class JsonPagination : IJsonPagination {

        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        [JsonProperty(PropertyName = "limit")]
        public int Limit { get; set; }

        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; set; }

    }

}