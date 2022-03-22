# Sork
This package is meant to do the boring stuff for you.
<br>

## SorkLog
Adds logging to an endpoint with minimal setup
It will log in accordance with the statuscode returned

|StatusCode|LoggedAs|
|--------|---|
|2xx|Information|
|3xx|Information|
|4xx|Warning|
|5xx|Error|

```js
{ 
    'route': 'RouteData',
    'action': 'DisplayName',
    'responseCode': 'StatusCode',
    'timeOfCallUTC': 'DateTime.Now.UTC'
}
```

### How to use

add it to your startup
```cs
namespace Tutorial
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<SorkLog>();
        }
    }
}
```

then you can just append it in your controller(s), either on controller level or on method level

```cs
namespace Tutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(SorkLog))] // Either here for use with all methods
    public class TutorialController : ControllerBase
    {
        private readonly IRepo _db;

        public TutorialController(IRepo db)
        {
            _db = db;
        }

        [HttpGet]
        [ServiceFilter(typeof(SorkLog))] // Or here for method specific use
        public IActionResult Get()
        {
            var result = await _db.GetExample();

            if(result is null)
                return BadRequest();
            
            return Ok(result);
        }
    }
}
```
