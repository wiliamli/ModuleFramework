using Jwell.Framework.Mvc;
using System;
using System.Web.Http;

namespace Jwell.Sample.Controllers
{
    public class BaseApiController : ApiController
    {
        [HttpPost]
        public StandardJsonResult StandardAction(Action action)
        {
            var result = new StandardJsonResult();
            result.StandardAction(action);
            return result;
        }

        [HttpPost]
        public StandardJsonResult<T> StandardAction<T>(Func<T> action)
        {
            var result = new StandardJsonResult<T>();
            result.StandardAction(() =>
            {
                result.Value = action();
            });
            return result;
        }
    }
}