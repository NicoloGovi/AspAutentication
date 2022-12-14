using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiOauth.Controllers
{
    [RoutePrefix("api/v1")]
    public class TestController : ApiController
    {
        [Route("public")]
        [AllowAnonymous]
        [HttpGet]
        public string PublicAPI()
        {
            return "API eseguita con successo";
        }

        [Route("private")]
        [Authorize]
        [HttpGet]
        public string PrivateAPI()
        {
            return "API eseguita con successo";
        }
    }
}