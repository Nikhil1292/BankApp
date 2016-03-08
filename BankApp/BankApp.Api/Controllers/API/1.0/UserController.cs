using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankApp.Api.Controllers
{
    public class UserController : ApiController
    {
       
        [HttpGet]
        [Route("api/users")]
        public string GetUsers()
        {
            return "Got it";
        }
    }
}
