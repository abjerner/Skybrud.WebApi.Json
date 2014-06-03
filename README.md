**Skybrud.WebApi.Json** lets you add an attribute to your WebApi controllers forcing the server to always output the response as JSON.

Since this project is made for use in Umbraco, I have only tested in Umbraco, and currently only in Umbraco 7.0.4. However since there isn't anything Umbraco specific code, it should also work with standard WebApi controllers as well as other versions of Umbraco.

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
