using Jwell.Modules.WebApi.Attributes;
using System.Collections.Generic;
using System.Web.Http;

namespace Jwell.Sample.Controllers
{
    public class UserController : BaseApiController
    {

        // GET api/<controller>
        [ApiIgnore]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [ApiIgnore]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [ApiIgnore]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [ApiIgnore]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [ApiIgnore]
        public void Delete(int id)
        {
        }
    }
}