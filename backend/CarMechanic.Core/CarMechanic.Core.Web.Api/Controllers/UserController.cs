using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CarMechanic.Core.BusinessLogic;
using CarMechanic.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CarMechanic.Core.Web.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        

        /// <summary>
        /// A felhasználó beléptetése
        /// </summary>
        /// <param name="loginName">login név</param>
        /// <param name="password">jelszó</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Authenticate(string loginName, string password)
        {
            try
            {
                var manager = new UserManager();
                return Ok(manager.AuthenticateUser(loginName, password));

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }


        }

    }
}