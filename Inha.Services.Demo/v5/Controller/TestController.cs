using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inha.Services.Demo.v5.Controller
{
    [ApiVersion("5.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Intercept("log-calls")]
    public class TestController : ControllerBase
    {
        // GET: api/Test
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Test/5
        [HttpGet("{id}/Tien", Name = "Gettttt")]
        public string Get(int id)
        {
            return id.ToString();
        }

        // POST: api/Test
        [HttpPost("New")]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Test/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
