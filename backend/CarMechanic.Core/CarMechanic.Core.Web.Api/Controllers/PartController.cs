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
    /// Alkatrész kontroller
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PartController : ControllerBase
    {

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly DbContextOptions<DomainModel.Models.CarMechanicContext> _options;

        /// <summary>
        /// Alkatrész kontoller kontruktor
        /// </summary>
        /// <param name="serviceProvider"></param>
        public PartController(IServiceProvider serviceProvider)
        {
            _options = serviceProvider.GetRequiredService<DbContextOptions<DomainModel.Models.CarMechanicContext>>();
        }

        /// <summary>
        /// Alkatrészek listája
        /// </summary>
        /// <returns>Alkatérszek</returns>
        [HttpGet]
        public IActionResult GetPartList()
        {
            try
            {
                var manager = new PartManager(_options);
                return Ok(manager.GetParts());

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Alkatrész adatai
        /// </summary>
        /// <param name="partId">Alkatrész azonosító</param>
        /// <returns>Alkatrész adatai</returns>
        [HttpGet]
        public IActionResult GetPart(int partId)
        {
            try
            {
                var manager = new PartManager(_options);
                return Ok(manager.GetPart(partId));

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Alaktrész mentése / módosítás
        /// </summary>
        /// <param name="data">Alaktrész adatai</param>    
        [HttpPost]
        public IActionResult SetPart([FromBody] Alkatresz data)
        {
            try
            {
                var manager = new PartManager(_options);
                var result = manager.SetPart(data);
                return Ok(result);

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Alkatrész törlése
        /// </summary>
        /// <param name="data">Alkatrész adatai</param>    
        [HttpPost]
        public IActionResult RemovePart([FromBody] Alkatresz data)
        {
            try
            {
                var manager = new PartManager(_options);
                manager.RemovePart(data);
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
        /// Alaktrész árazása
        /// </summary>
        /// <param name="data">Alkatrész adatok</param>    
        [HttpPost]
        public IActionResult SetPartPrice([FromBody] Alkatresz data)
        {
            try
            {
                var manager = new PartManager(_options);
                manager.SetPartPrice(data);
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