using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarMechanic.Core.BusinessLogic;
using CarMechanic.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        
        /// <summary>
        /// Ügyfelek listája
        /// </summary>
        /// <returns>Ügyfelek</returns>
        [HttpGet]
        public IActionResult GetClientList()
        {
            try
            {
                var manager = new ClientManager();
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
        /// <returns>Ügyfél</returns>
        [HttpGet]
        public IActionResult GetClient(int clientId)
        {
            try
            {
                var manager = new ClientManager();
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
                 var manager = new ClientManager();
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
                 var manager = new ClientManager();
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
                var manager = new ClientManager();
                manager.SetClient(data);
                return Ok();

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
                var manager = new ClientManager();
                manager.RemoveClient(data);
                return Ok();

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }


    }
}