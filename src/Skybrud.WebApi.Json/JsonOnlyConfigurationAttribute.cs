﻿using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;

namespace Skybrud.WebApi.Json {

    [AttributeUsage(AttributeTargets.Class)]
    public class JsonOnlyConfigurationAttribute : Attribute, IControllerConfiguration {
        
        public virtual void Initialize(HttpControllerSettings settings, HttpControllerDescriptor descriptor) {
            var toRemove = settings.Formatters.Where(t => t is JsonMediaTypeFormatter || t is XmlMediaTypeFormatter).ToList();
            foreach (var r in toRemove) {
                settings.Formatters.Remove(r);
            }
            settings.Formatters.Add(new SkybrudJsonMediaTypeFormatter());
        }
    
    }

}