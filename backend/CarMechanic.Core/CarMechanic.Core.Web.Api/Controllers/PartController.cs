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
    /// Alkatrész kontroller
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PartController : ControllerBase
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Alkatrészek listája
        /// </summary>
        /// <returns>Alkatérszek</returns>
        [HttpGet]
        public IActionResult GetPartList()
        {
            try
            {
                var manager = new PartManager();
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
        /// <returns>Alkatérszek</returns>
        [HttpGet]
        public IActionResult GetPart(int partId)
        {
            try
            {
                var manager = new PartManager();
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
                var manager = new PartManager();
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
                var manager = new PartManager();
                manager.RemovePart(data);
                return Ok();

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
        /// <param name="partId">Szolgáltatás azonosító</param>    
        /// <param name="purchasePrice">Új beszerzési ára</param>    
        /// <param name="salesPrice">Új eladási ára</param>    
        [HttpPost]
        public IActionResult SetPartPrice(int partId, decimal purchasePrice, decimal salesPrice)
        {
            try
            {
                var manager = new PartManager();
                manager.SetPartPrice(partId, purchasePrice, salesPrice);
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