using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fundoo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBusiness _labelBusiness;

        public LabelController(ILabelBusiness labelBusiness)
        {
            _labelBusiness = labelBusiness;
        }

        [HttpPost]
        [Route("CreateLabel")]
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
    }
}