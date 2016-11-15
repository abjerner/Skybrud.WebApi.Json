using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace Skybrud.WebApi.Json.Meta {

    /// <summary>
    /// Class representing the a JSON response.
    /// </summary>
    public class JsonMetaResponse {

        #region Properties

        /// <summary>
        /// Gets or sets the meta data for the response.
        /// </summary>
        [JsonProperty(PropertyName = "meta")]
        public JsonMetaData Meta { get; set; }

        /// <summary>
        /// Gets or sets the pagination object.
        /// </summary>
        [JsonProperty(PropertyName = "pagination", NullValueHandling = NullValueHandling.Ignore)]
        public IJsonPagination Pagination { get; set; }

        /// <summary>
        /// Gets or sets the data object.
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public object Data { get; set; }

        #endregion

        #region Constructors

        public JsonMetaResponse() {
            Meta = new JsonMetaData();
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Sets the pagination object for the response using the specified parameters.
        /// </summary>
        /// <param name="pagination">The pagination object.</param>
        public void SetPagination(IJsonPagination pagination) {
            Pagination = pagination;
        }

        /// <summary>
        /// Creates a new success response object with a 200 status message.
        /// </summary>
        /// <param name="data">The data object.</param>
        public static JsonMetaResponse GetSuccess(object data) {
            return GetSuccess(data, HttpStatusCode.OK);
        }

        /// <summary>
        /// Creates a new success response with the specified <code>code</code>.
        /// </summary>
        /// <param name="data">The data object.</param>
        /// <param name="code">The status code of the response.</param>
        /// <returns></returns>
        public static JsonMetaResponse GetSuccess(object data, HttpStatusCode code) {
            return new JsonMetaResponse {
                Meta = { Code = code },
                Data = data
            };
        }

        /// <summary>
        /// Creates a new error response with the specified error message.
        /// </summary>
        /// <param name="error">The error message of the response.</param>
        public static JsonMetaResponse GetError(string error) {
            return GetError(HttpStatusCode.InternalServerError, error);
        }

        /// <summary>
        /// Creates a new error response with the specified status code and error message.
        /// </summary>
        /// <param name="code">The status code.</param>
        /// <param name="error">The error message of the response.</param>
        public static JsonMetaResponse GetError(HttpStatusCode code, string error) {
            return GetError(code, error, null);
        }

        /// <summary>
        /// Creates a new error response with the specified status code and error message.
        /// </summary>
        /// <param name="code">The status code.</param>
        /// <param name="error">The error message of the response.</param>
        /// <param name="data">The data object.</param>
        public static JsonMetaResponse GetError(HttpStatusCode code, string error, object data) {
            return new JsonMetaResponse {
                Meta = { Code = code, Error = error },
                Data = data
            };
        }

        /// <summary>
        /// Generates a new success response based on the specified collection.
        /// Pagination will be enforced using <code>offset</code> and
        /// <code>limit</code>, and the remaining items will be converted using
        /// the specified delegate <code>func</code>.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="func">The delegate that should be used for converting the collection items.</param>
        /// <param name="offset">The offset to be used for pagination.</param>
        /// <param name="limit">The limit to be used for pagination.</param>
        /// <param name="totalres">If you have another total than the total of the collection</param>
        public static JsonMetaResponse GetSuccessFromIEnumerable<TIn, TOut>(IEnumerable<TIn> collection, Func<TIn, TOut> func, int offset = 0, int limit = 10, int totalres = 0) {

            // Validate input
            if (collection == null) throw new ArgumentNullException("collection");
            if (func == null) throw new ArgumentNullException("func");
            
            // Convert to an array so we don't iterate over an enumerable
            TIn[] array = collection as TIn[] ?? collection.ToArray();

            // Get the total amount of items in the collection or from param totalRes
            int total = totalres > 0 ? totalres : array.Count();

            // Enforce pagination and then convert remaining objects
            IEnumerable<TOut> data = array.Skip(offset).Take(limit).Select(func);

            // Generate a new response object
            JsonMetaResponse model = GetSuccess(data);

            // Add the pagination info to the response
            model.Pagination = new JsonPagination {
                Limit = limit,
                Offset = offset,
                Total = total
            };

            // Return the response object
            return model;

        }
        
        public static JsonMetaResponse GetSuccessFromObject<TIn, TOut>(TIn data, Func<TIn, TOut> func) {
            
            // Validate input
            if (func == null) throw new ArgumentNullException("func");

            // Convert the specified data object
            var converted = func(data);

            // Return the response object
            return GetSuccess(converted);

        }

        #endregion

    }

}