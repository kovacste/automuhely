using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarMechanic.Core.BusinessLogic;
using CarMechanic.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarMechanic.Core.Web.Api.Controllers
{
    /// <summary>
    /// Ügyfél kontoller
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly DbContextOptions<DomainModel.Models.CarMechanicContext> _options;

        /// <summary>
        /// Ügyfél kontoller kontruktor
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ClientController(IServiceProvider serviceProvider)
        {
            _options = serviceProvider.GetRequiredService<DbContextOptions<DomainModel.Models.CarMechanicContext>>();
        }

        /// <summary>
        /// Ügyfelek listája
        /// </summary>
        /// <returns>Ügyfelek</returns>
        [HttpGet]
        public IActionResult GetClientList()
        {
            try
            {
                var manager = new ClientManager(_options);
                return Ok(manager.GetClientList());

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Ügyfél adatai
        /// </summary>
        /// <param name="clientId">Ügyfél azonosító</param>
        /// <returns>Ügyfél adata</returns>
        [HttpGet]
        public IActionResult GetClient(int clientId)
        {
            try
            {
                var manager = new ClientManager(_options);
                return Ok(manager.GetClient(clientId));

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Települések listája
        /// </summary>
        /// <returns>Települések</returns>
        [HttpGet]
         public IActionResult GetCities()
         {
             try
             {
                 var manager = new ClientManager(_options);
                return Ok(manager.GetCities());

             }
             catch (Exception ex)
             {
                 logger.Error(ex);
                 return BadRequest(ex.Message);
             }
         }

        /// <summary>
        /// Közterületek listája
        /// </summary>
        /// <returns>Közterületek</returns>
        [HttpGet]
        public IActionResult GetStreetTypes()
         {
             try
             {
                 var manager = new ClientManager(_options);
                return Ok(manager.GetStreetTypes());

             }
             catch (Exception ex)
             {
                 logger.Error(ex);
                 return BadRequest(ex.Message);
             }
         }

        /// <summary>
        /// Ügyfél mentése / módosítás
        /// </summary>
        /// <param name="data">ügyfél adatai</param>    
        [HttpPost]
        public IActionResult SetClient([FromBody] Ugyfel data)
        {
            try
            {
                var manager = new ClientManager(_options);
                var result = manager.SetClient(data);
                return Ok(result);

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Ügyfél törlése
        /// </summary>
        /// <param name="data">Ügyfél adatai</param>    
        [HttpPost]
        public IActionResult RemoveClient([FromBody] Ugyfel data)
        {
            try
            {
                var manager = new ClientManager(_options);
                manager.RemoveClient(data);
                return Ok();

            }
            catch (DbUpdateException e)
            {
                if (e.InnerException.Message.Contains("FK_"))
                    return BadRequest("DATA_IN_USE");
                else
                    return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Az ügyfél beléptetése
        /// </summary>
        /// <param name="loginName">loginnév</param>
        /// <param name="password">jelszó</param>
        /// <returns>Ügyfél adata</returns>
        [HttpGet]
        public IActionResult Authenticate(string loginName, string password)
        {
            try
            {
                var manager = new ClientManager(_options);
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