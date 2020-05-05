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
    /// Munkalap kontroller
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WorksheetController : ControllerBase
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Munkalapok listája
        /// </summary>
        /// <returns>Munkalapok</returns>
        [HttpGet]
        public IActionResult GetWorkSheets()
        {
            try
            {
                var manager = new WorksheetManager();
                return Ok(manager.GetWorkSheets());

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Munkalap adatai
        /// </summary>
        /// <returns>Munkalap adatai</returns>
        [HttpGet]
        public IActionResult GetWorkSheet(int worksheetId)
        {
            try
            {
                var manager = new WorksheetManager();
                return Ok(manager.GetWorkSheet(worksheetId));

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Munkalap adatai
        /// </summary>
        /// <returns>Munkalap adatai</returns>
        [HttpGet]
        public IActionResult GetWorkSheetWithClientId(int clientId)
        {
            try
            {
                var manager = new WorksheetManager();
                return Ok(manager.GetWorkSheetWithClientId(clientId));

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Munkalapok listája
        /// </summary>
        /// <returns>Munkalapok</returns>
        [HttpGet]
        public IActionResult GetWorkSheetDetails(int worksheetId)
        {
            try
            {
                var manager = new WorksheetManager();
                return Ok(manager.GetWorkSheetDetails(worksheetId));

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Munkalap rendelések listája
        /// </summary>
        /// <returns>Munkalap rendelések</returns>
        [HttpGet]
        public IActionResult GetWorkSheetOrders(int worksheetId)
        {
            try
            {
                var manager = new WorksheetManager();
                return Ok(manager.GetWorkSheetOrders(worksheetId));

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Munkalap tétel adatai
        /// </summary>
        /// <returns>Munkalap tétel</returns>
        [HttpGet]
        public IActionResult GetWorkSheetDetail(int worksheetId)
        {
            try
            {
                var manager = new WorksheetManager();
                return Ok(manager.GetWorkSheetDetail(worksheetId));

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Munkalap mentése / módosítása
        /// </summary>
        /// <param name="data">Munkalap adatai</param>    
        [HttpPost]
        public IActionResult SetWorkSheet([FromBody] Munkalap data)
        {
            try
            {
                var manager = new WorksheetManager();
                var result = manager.SetWorkSheet(data);
                return Ok(result);

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Munkalap tétel mentése / módosítása
        /// </summary>
        /// <param name="data">Munkalap tétel adatai</param>    
        [HttpPost]
        public IActionResult SetWorkSheetDetail([FromBody] MunkalapTetel[] data)
        {
            try
            {
                var manager = new WorksheetManager();
                var result = manager.SetWorkSheetDetails(data);
                return Ok(result);

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Munkalap rendelés mentése / módosítása
        /// </summary>
        /// <param name="data">Munkalap rendelés adatai</param>    
        [HttpPost]
        public IActionResult SetWorkSheetOrder([FromBody] MunkalapRendeles[] data)
        {
            try
            {
                var manager = new WorksheetManager();
                var result = manager.SetWorkSheetOrders(data);
                return Ok(result);

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Munkalap törlése
        /// </summary>
        /// <param name="data">Munkalap adatai</param>    
        [HttpPost]
        public IActionResult RemoveWorkSheet([FromBody] Munkalap data)
        {
            try
            {
                var manager = new WorksheetManager();
                manager.RemoveWorkSheet(data);
                return Ok();

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Munkalap teétel törlése
        /// </summary>
        /// <param name="data">Munkalap adatai</param>    
        [HttpPost]
        public IActionResult RemoveWorkSheetDetail([FromBody] MunkalapTetel data)
        {
            try
            {
                var manager = new WorksheetManager();
                manager.RemoveWorkSheetDetail(data);
                return Ok();

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Munkalap teétel törlése
        /// </summary>
        /// <param name="data">Munkalap adatai</param>    
        [HttpPost]
        public IActionResult RemoveWorkSheetOrder([FromBody] MunkalapRendeles data)
        {
            try
            {
                var manager = new WorksheetManager();
                manager.RemoveWorkSheetOrder(data);
                return Ok();

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Munkalap lezárása
        /// </summary>
        /// <param name="worksheetId">munkalap azonosító</param>
        /// <param name="user">felhasználó loginnév</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CloseWorkSheet(int worksheetId, string user)
        {
            try
            {
                var manager = new WorksheetManager();
                manager.CloseWorkSheet(worksheetId, user);
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