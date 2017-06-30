using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace ApiDotNetCoreConcept.Controllers
{
   // [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/values")]
    public class ValuesController : Controller
    {
        private readonly IPrint _print;
        IConfiguration _iconfiguration;

        public ValuesController(IPrint print, IConfiguration iconfiguration)
        {
            _print = print;
            _iconfiguration = iconfiguration;
        }

        // GET api/values
        [HttpGet("teste")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Person), 200)]
        [ProducesResponseType(404)]
        public IEnumerable<string> Get()
        {
            return _print.printConsole();    
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
