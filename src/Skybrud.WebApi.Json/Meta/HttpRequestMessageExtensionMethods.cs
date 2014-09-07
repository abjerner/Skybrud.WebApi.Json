using System.Net.Http;

namespace Skybrud.WebApi.Json.Meta {
    
    public static class HttpRequestMessageExtensionMethods {

        /// <summary>
        /// Creates a new <code>HttpResponseMessage</code> based on the specified
        /// <code>JsonMetaResponse</code>. The status code of the server response will
        /// automatically be derived from the <code>JsonMetaResponse</code>.
        /// </summary>
        /// <param name="request">The current request.</param>
        /// <param name="response">The meta response object to be returned.</param>
        public static HttpResponseMessage CreateResponse(this HttpRequestMessage request, JsonMetaResponse response) {
            return request.CreateResponse(response.Meta.Code, response);
        }

    }

}