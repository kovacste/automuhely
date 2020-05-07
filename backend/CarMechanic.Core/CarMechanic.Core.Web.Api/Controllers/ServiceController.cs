using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarMechanic.Core.BusinessLogic;
using CarMechanic.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
        private readonly DbContextOptions<DomainModel.Models.CarMechanicContext> _options;

        /// <summary>
        /// Szolgáltatás kontoller kontruktor
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ServiceController(IServiceProvider serviceProvider)
        {
            _options = serviceProvider.GetRequiredService<DbContextOptions<DomainModel.Models.CarMechanicContext>>();
        }

        /// <summary>
        /// Szolgáltatások listája
        /// </summary>
        /// <returns>Szolgáltatások</returns>
        [HttpGet]
        public IActionResult GetServiceList()
        {
            try
            {
                var manager = new ServiceManager(_options);
                return Ok(manager.GetServices());

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Szolgáltatás adatai
        /// </summary>
        /// <param name="serviceId">szolgáltatás azonosító</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetService(int serviceId)
        {
            try
            {
                var manager = new ServiceManager(_options);
                return Ok(manager.GetService(serviceId));

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
                var manager = new ServiceManager(_options);
                var result = manager.SetService(data);
                return Ok(result);

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
                var manager = new ServiceManager(_options);
                manager.RemoveService(data);
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
        /// Szolgáltatás árazása
        /// </summary>
        /// <param name="serviceId">Szolgáltatás azonosító</param>    
        /// <param name="price">Új ára</param>    
        [HttpPost]
        public IActionResult SetServicePrice(int serviceId, decimal price)
        {
            try
            {
                var manager = new ServiceManager(_options);
                manager.SetServicePrice(serviceId, price);
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