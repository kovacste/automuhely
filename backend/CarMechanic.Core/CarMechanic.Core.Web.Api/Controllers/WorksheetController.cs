﻿using System;
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
        /// Munkalap mentése / módosítása
        /// </summary>
        /// <param name="data">Munkalap adatai</param>    
        [HttpPost]
        public IActionResult SetWorkSheet([FromBody] Munkalap data)
        {
            try
            {
                var manager = new WorksheetManager();
                manager.SetWorkSheet(data);
                return Ok();

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
                manager.SetWorkSheetDetails(data);
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
    }
}