# Sork
Final project for my education. <br>
This package is meant to do boring generic logging for you.
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
<br>

```js
{ 
    'trace': 'Trace',
    'path': 'Route',
    'time': 'DateTime.Now.UTC',
    'statusCode': 'StatusCode'
}
```
<br>

### How to use ``SorkLogFilter``

inject ``SorkLogFilter`` in your startup
```cs
namespace Tutorial
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<SorkLogFilter>();
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
    [ServiceFilter(typeof(SorkLogFilter))] // Either here for use with all methods
    public class TutorialController : ControllerBase
    {
        private readonly IRepo _db;

        public TutorialController(IRepo db)
        {
            _db = db;
        }

        [HttpGet]
        [ServiceFilter(typeof(SorkLogFilter))] // Or here for method specific use
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
<br>

### How to use ``SorkLogMiddelware``

add the middleware in startup

```cs
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSorkLog();

    }
```
<br>