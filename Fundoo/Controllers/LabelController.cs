﻿using BusinessLayer.Interface;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fundoo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBusiness _labelBusiness;

        public LabelController(ILabelBusiness labelBusiness)
        {
            _labelBusiness = labelBusiness;
        }

        /// <summary>
        /// It is used to show all Labels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IActionResult GetAllLabelsData()
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);
                List<LabelResponseData> labelData = _labelBusiness.GetAllLabels(userId);
                return Ok(labelData.ToList());
            }
            catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It Creates Label
        /// </summary>
        /// <param name="labelRequest">Label Name</param>
        /// <returns>If Data Found, It return 200ok else return NotFound Response And If any exception occured return BadRequest</returns>
        [HttpPost]
        [Route("")]
        public IActionResult CreateNote(LabelRequest labelRequest)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);
                LabelResponseData data = _labelBusiness.CreateLabel(userId, labelRequest);
                bool success = false;
                string message;
                if (data == null)
                {
                    message = "Try again";
                    return Ok(new { success, message });
                }
                else
                {
                    success = true;
                    message = "Label Created Successfully";
                    return Ok(new { success, message, data });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It is used to Edit LabelName
        /// </summary>
        /// <param name="labelID">LabelID</param>
        /// <param name="updateLabelRequest">LabelName</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{labelID}")]
        public IActionResult EditLabel(int labelID, UpdateLabelRequest updateLabelRequest)
        {
            try
            {
                bool success = false;
                string message;
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);

                LabelResponseData data = _labelBusiness.EditLabel(userId, labelID, updateLabelRequest);

                if (data != null)
                {
                    success = true;
                    message = "Label Updated Successfully";
                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "Try Again!";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }


        /// <summary>
        /// It is used to Delete Label
        /// </summary>
        /// <param name="labelID">LabelID</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{labelID}")]
        public IActionResult DeleteLabel(int labelID)
        {
            try
            {
                bool success = false, data;
                string message;
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);
                data = _labelBusiness.DeleteLabel(userId, labelID);

                if (data)
                {
                    success = true;
                    message = "Label Deleted Successfully";
                    return Ok(new { success, message });
                }
                else
                {
                    message = "Try Again!";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}