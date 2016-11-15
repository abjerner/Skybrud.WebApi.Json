using Newtonsoft.Json;

namespace Skybrud.WebApi.Json.Meta {

    /// <summary>
    /// Solid implementation of the <see cref="IJsonPagination"/>.
    /// </summary>
    public class JsonPagination : IJsonPagination {

        /// <summary>
        /// Gets or sets the total amount of items.
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the limit.
        /// </summary>
        [JsonProperty(PropertyName = "limit")]
        public int Limit { get; set; }

        /// <summary>
        /// Gets or sets the limit.
        /// </summary>
        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; set; }

    }

}