using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarMechanic.Core.BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarMechanic.Core.Web.Api.Controllers
{
    /// <summary>
    /// Riport kontroller
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        
        /// <summary>
        /// Dolgozói statisztika
        /// </summary>
        /// <returns>Statisztika</returns>
        [HttpGet]
        public IActionResult GetWorkerStatistic(int year, int month)
        {
            try
            {
                var manager = new ReportManager();
                return Ok(manager.GetWorkerStatistic(year, month));

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }
        
        
        /// <summary>
        /// Szervíz statisztika
        /// </summary>
        /// <returns>statisztika</returns>
        [HttpGet]
        public IActionResult GetServiceStatistic(int year, int month)
        {
            try
            {
                var manager = new ReportManager();
                return Ok(manager.GetServiceStatistic(year, month));

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }
        
    }
}