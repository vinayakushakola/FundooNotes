using BusinessLayer.Interface;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fundoo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly IUserNoteBusiness _userNoteBusiness;

        public NoteController(IUserNoteBusiness userNoteBusiness)
        {
            _userNoteBusiness = userNoteBusiness;
        }

        [HttpPost]
        [Route("CreateNotes")]
        public IActionResult CreateNote(UserNoteRequest userNoteData)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);
                UserNoteResponseData data = _userNoteBusiness.CreateNote(userId, userNoteData);
                bool success = false;
                string message;
                if (userNoteData == null)
                {
                    message = "Try again";
                    return Ok(new { success, message });
                }
                else
                {
                    success = true;
                    message = "Notes Created Successfully";
                    return Ok(new { success, message, data });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpDelete]
        [Route("DeleteNotes")]
        public IActionResult DeleteNote(int noteID)
        {
            try
            {
                string data = _userNoteBusiness.DeleteNote(noteID);
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
                    return Ok(new { success, data });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateNotes")]
        public IActionResult UpdateNotes(int noteID, UpdateNoteRequest updateNoteRequest)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);
                UserNoteResponseData userUpdateData = _userNoteBusiness.UpdateNote(userId, noteID, updateNoteRequest);
                bool success = false;
                string message;
                if (userUpdateData == null)
                {
                    message = "Try again";
                    return Ok(new { success, message });
                }
                else
                {
                    success = true;
                    message = "Notes Updated Successfully";
                    return Ok(new { success, message, userUpdateData });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new { ex.Message });

            }
        }

        [HttpGet]
        [Route("GetAllTrashedNotes")]
        public IActionResult GetAllTrashedNotes()
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);

                List<UserNoteResponseData> userNoteResponseDataList = _userNoteBusiness.GetTrashedNotes(userId);

                if (userNoteResponseDataList != null)
                {
                    return Ok(userNoteResponseDataList.ToList());
                }
                else
                {
                    return Ok(userNoteResponseDataList.ToList());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet]
        [Route("GetAllArchievedNotes")]
        public IActionResult GetAllArchievedNotes()
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);

                List<UserNoteResponseData> userNoteResponseDataList = _userNoteBusiness.GetArchievedNotes(userId);

                if (userNoteResponseDataList != null)
                {
                    return Ok(userNoteResponseDataList.ToList());
                }
                else
                {
                    return Ok(userNoteResponseDataList.ToList());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }


        [HttpGet]
        [Route("GetAllUserNotes")]
        public IActionResult GetAllUserNotes()
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);

                List<UserNoteResponseData> userNoteResponseDataList = _userNoteBusiness.GetAllUserNotes(userId);

                if(userNoteResponseDataList != null)
                {
                    return Ok(userNoteResponseDataList.ToList());
                }
                else
                {
                    return Ok(userNoteResponseDataList.ToList());
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}