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
    /// Szolgáltatás kontroller
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Szolgáltatások listája
        /// </summary>
        /// <returns>Szolgáltatások</returns>
        [HttpGet]
        public IActionResult GetServiceList()
        {
            try
            {
                var manager = new ServiceManager();
                return Ok(manager.GetServices());

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Szolgáltatás mentése / módosítás
        /// </summary>
        /// <param name="data">Szolgáltatás adatai</param>    
        [HttpPost]
        public IActionResult SetService([FromBody] Szolgaltatas data)
        {
            try
            {
                var manager = new ServiceManager();
                manager.SetService(data);
                return Ok();

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Szolgáltatás törlése
        /// </summary>
        /// <param name="data">Szolgáltatás adatai</param>    
        [HttpPost]
        public IActionResult RemoveService([FromBody] Szolgaltatas data)
        {
            try
            {
                var manager = new ServiceManager();
                manager.RemoveService(data);
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