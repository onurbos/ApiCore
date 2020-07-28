using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCore.Controllers
{
    [Route("api/[controller]"), ApiController, Authorize(AuthenticationSchemes = "Bearer")]
    public class DefaultController : ControllerBase
    {
        // GET: api/New
        [Route("get/{id}"), HttpGet]
        public IEnumerable<string> Get(int id)
        {
            return new string[] { "value1", id.ToString() };
        }

        // POST: api/New
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/New/5
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
