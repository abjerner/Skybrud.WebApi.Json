**Skybrud.WebApi.Json** lets you add an attribute to your WebApi controllers forcing the server to always output the response as JSON.

Since this project is made for use in Umbraco, I have only tested in Umbraco, and currently only in Umbraco 7.0.4. However since there isn't anything Umbraco specific code, it should also work with standard WebApi controllers as well as other versions of Umbraco.

#### Download

Skybrud.WebApi.Json can be downloaded from NuGet:

http://www.nuget.org/packages/Skybrud.WebApi.Json/

#### How to use in Umbraco ####

```C#
using Skybrud.WebApi.Json;
using Umbraco.Web.WebApi;

namespace WebApplication1.Controllers {

    [JsonOnlyConfiguration]
    public class JsonTestController : UmbracoApiController {

        public object GetTest() {
            return new {
                meta = new {
                    code = 200
                },
                data = "Yay! We have some JSON!"
            };
        }

    }

}
```


```C#
using Skybrud.WebApi.Json;
using Skybrud.WebApi.Json.Meta;
using Umbraco.Web;
using Umbraco.Web.WebApi;

namespace WebApplication1.Controllers {

    [JsonOnlyConfiguration]
    public class JsonTestController : UmbracoApiController {

        private UmbracoHelper _helper = new UmbracoHelper(UmbracoContext.Current);

        public object GetTest() {
            
            var content = _helper.TypedContent(1024);
            
            if(content != null) {
                return Request.CreateResponse(JsonMetaResponse.GetSuccessFromObject(content, TestModel.GetFromContent));
            } else {
                return Request.CreateResponse(JsonMetaResponse.GetError(HttpStatusCode.NotFound, "Siden fandtes ikke."));
            }
        }
    }
}

namespace WebApplication1.Models {
    public class TestModel
    {
        [JsonProperty("id")]
        public int Id { get; set;}
        
        [JsonProperty("name")]
        public string Name { get; set;}
        
        [JsonProperty("created")]
        public DateTime Created { get; set;}
        
        
        public static TestModel GetFromContent(IPublishedContent a) {
            return new TestModel
            {
                Id = a.Id,
                Name = a.Name,
                Created = a.CreateDate
            }
        }
    }
}
```
