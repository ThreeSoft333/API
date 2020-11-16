using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Contracts.V1.Requests.EComm.NotificationReq;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Services.EComm.NotificationServ;
using ThreeSoftECommAPI.Services.EComm.UserNotifCountServ;
using static ThreeSoftECommAPI.Contracts.V1.ApiRoutes;

namespace ThreeSoftECommAPI.Controllers.V1
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NotificationController:Controller
    {
        private readonly INotificationService _notificationService;
        private readonly IUserNotificationCountService _userNotificationCountService;
        public NotificationController(INotificationService notificationService, IUserNotificationCountService userNotificationCountService)
        {
            _notificationService = notificationService;
            _userNotificationCountService = userNotificationCountService;
        }

        [HttpGet(ApiRoutes.NotificationRout.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _notificationService.GetNotificationAsync());
        }

        [HttpGet(ApiRoutes.NotificationRout.Get)]
        public async Task<IActionResult> Get([FromRoute] Int32 id)
        {
            var Catg = await _notificationService.GetNotificationByIdAsync(id);

            if (Catg == null)
                return NotFound(new ErrorResponse
                {
                    message = "Not Found",
                    status = NotFound().StatusCode
                });

            return Ok(Catg);
        }

        [HttpPost(ApiRoutes.NotificationRout.Create)]
        public async Task<IActionResult> Create([FromBody] NotificationRequest notificationRequest)
        {
            try
            {
                var notf = new Notification
                {
                    TitleAr = notificationRequest.TitleAr,
                    TitleEn = notificationRequest.TitleEn,
                    BodyAr = notificationRequest.BodyAr,
                    BodyEn = notificationRequest.BodyEn,
                    ImageUrl = notificationRequest.ImageUrl,
                    CreateDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")
                };

                var status = await _notificationService.CreateNotificationAsync(notf);

                if (status == -1)
                {
                    return Conflict(new ErrorResponse
                    {
                        message = "Dublicate Entry",
                        status = Conflict().StatusCode
                    });
                }

                if (status == 1)
                {
                    _userNotificationCountService.WhenAddNewNotification();
                    var response = new
                    {
                        message = "Successfully Insert",
                        status = Ok().StatusCode
                    };
                    return Ok(response);
                }
                return NotFound(new ErrorResponse
                {
                    message = "Not Found",
                    status = NotFound().StatusCode
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    message = ex.Message,
                    status = BadRequest().StatusCode
                });
            }
        }

        [HttpPost(ApiRoutes.NotificationRout.Update)]
        public async Task<IActionResult> Update([FromRoute] Int32 id, [FromBody] NotificationRequest notificationRequest )
        {
            var notf = new Notification
            {
                Id = id,
                TitleAr = notificationRequest.TitleAr,
                TitleEn = notificationRequest.TitleEn,
                BodyAr = notificationRequest.BodyAr,
                BodyEn = notificationRequest.BodyEn,
                ImageUrl = notificationRequest.ImageUrl
            };

            var status = await _notificationService.UpdateNotificationAsync(notf);

            if (status == -1)
            {
                return Conflict(new ErrorResponse
                {
                    message = "Dublicate Entry",
                    status = Conflict().StatusCode
                });
            }

            if (status == 1)
            {
                var response = new
                {
                    message = "Successfully Update",
                    status = Ok().StatusCode
                };
                return Ok(response);
            }

            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpDelete(ApiRoutes.NotificationRout.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Int32 id)
        {
            var deleted = await _notificationService.DeleteNotificationAsync(id);

            if (deleted)
                return Ok(new SuccessResponse
                {
                    message = "Successfully Deleted",
                    status = Ok().StatusCode
                });
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpPost(ApiRoutes.NotificationRout.Upload), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            string folderPath = "wwwroot/Resources/Images/NotificationImg/";
            bool exists = Directory.Exists(folderPath);

            if (!exists)
                Directory.CreateDirectory(folderPath);

            var file = Request.Form.Files[0];
            var folderName = Path.Combine(folderPath);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (file.Length > 0)
            {
                var fileName = DateTime.Now.Ticks + "_" + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok(new { dbPath });
            }
            return BadRequest();
        }

        [HttpPost(ApiRoutes.NotificationRout.updateusernotificationcount)]
        public IActionResult UpdateUserNotificationCount([FromRoute] string userid)
        {
            try
            {
                var update = _userNotificationCountService.Update(userid);

                if (update)
                {
                    var response = new
                    {
                        message = "Successfully Update",
                        status = Ok().StatusCode
                    };
                    return Ok(response);
                }

                return NotFound(new ErrorResponse
                {
                    message = "Not Found",
                    status = NotFound().StatusCode
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    message = ex.Message,
                    status = BadRequest().StatusCode
                });
            }
        }

    }
}
