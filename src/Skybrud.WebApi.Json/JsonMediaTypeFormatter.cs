using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Skybrud.WebApi.Json {

    public class JsonMediaTypeFormatter : System.Net.Http.Formatting.JsonMediaTypeFormatter {

        private string _callbackQueryParameter;

        public JsonMediaTypeFormatter() {
            SupportedMediaTypes.Add(DefaultMediaType);
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/javascript"));
            MediaTypeMappings.Add(new UriPathExtensionMapping("jsonp", DefaultMediaType));
        }

        public string CallbackQueryParameter {
            get { return _callbackQueryParameter ?? "callback"; }
            set { _callbackQueryParameter = value; }
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream stream, HttpContent content, TransportContext transportContext) {
            string callback;
            if (IsJsonpRequest(out callback)) {
                return Task.Factory.StartNew(() => {
                    StreamWriter writer = new StreamWriter(stream);
                    writer.Write(callback + "(");
                    writer.Flush();
                    base.WriteToStreamAsync(type, value, stream, content, transportContext).Wait();
                    writer.Write(")");
                    writer.Flush();
                });
            }
            return base.WriteToStreamAsync(type, value, stream, content, transportContext);
        }

        private bool IsJsonpRequest(out string callback) {
            callback = null;
            if (HttpContext.Current.Request.HttpMethod != "GET") return false;
            callback = HttpContext.Current.Request.QueryString[CallbackQueryParameter];
            return !String.IsNullOrEmpty(callback);
        }

    }

}