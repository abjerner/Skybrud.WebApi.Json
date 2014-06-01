using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;

namespace Skybrud.WebApi.Json {
    
    public class JsonOnlyConfigurationAttribute : Attribute, IControllerConfiguration {
        
        public virtual void Initialize(HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor) {
            var toRemove = controllerSettings.Formatters.Where(t => (t is System.Net.Http.Formatting.JsonMediaTypeFormatter) || (t is XmlMediaTypeFormatter)).ToList();
            foreach (var r in toRemove) {
                controllerSettings.Formatters.Remove(r);
            }
            controllerSettings.Formatters.Add(new JsonMediaTypeFormatter());
        }
    
    }

}