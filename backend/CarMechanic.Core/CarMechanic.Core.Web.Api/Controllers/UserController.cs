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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly DbContextOptions<DomainModel.Models.CarMechanicContext> _options;
        /// <summary>
        /// Ügyfél kontoller kontruktor
        /// </summary>
        /// <param name="serviceProvider"></param>
        public UserController(IServiceProvider serviceProvider)
        {
            _options = serviceProvider.GetRequiredService<DbContextOptions<DomainModel.Models.CarMechanicContext>>();
        }

        /// <summary>
        /// A felhasználó beléptetése
        /// </summary>
        /// <param name="loginName">login név</param>
        /// <param name="password">jelszó</param>
        /// <returns>Felhasználó adatai</returns>
        [HttpGet]
        public IActionResult Authenticate(string loginName, string password)
        {
            try
            {
                var manager = new UserManager(_options);
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