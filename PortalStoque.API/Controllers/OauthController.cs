using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    [Authorize]
    public class OauthController : ApiController
    {
        public bool GetAuthTest()
        {
            return true;
        }
    }
}
