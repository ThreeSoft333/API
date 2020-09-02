using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Contracts.V1.Requests.EComm.ContactUsReq;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Services.EComm.ContactUsServ;

namespace ThreeSoftECommAPI.Controllers.V1
{
    public class ContactUsController:Controller
    {
        private readonly IContactUsService _contactUsService;
        public ContactUsController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        [HttpGet(ApiRoutes.ContactUsRout.Get)]
        public async Task<IActionResult> Get()
        {
            var ContUs = await _contactUsService.GetContactUsAsync();

            if (ContUs == null)
                return NotFound(new ErrorResponse
                {
                    message = "Not Found",
                    status = NotFound().StatusCode
                });

            return Ok(ContUs);
        }

        [HttpPost(ApiRoutes.ContactUsRout.Create)]
        public async Task<IActionResult> Create([FromBody] CreateContactUsRequest createContact)
        {
            var contactUs = new ContactUs
            {
                 Email = createContact.Email,
                 Phone = createContact.Phone,
                 WebSite = createContact.WebSite,
                 Address = createContact.Address
            };

            var status = await _contactUsService.CreateContactUsAsync(contactUs);

            if (status == 1)
            {
                var response = new CategoryResponse { Id = contactUs.Id };
                return Ok(response);
            }
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpPost(ApiRoutes.ContactUsRout.Update)]
        public async Task<IActionResult> Update([FromRoute] Int32 id, [FromBody] CreateContactUsRequest createContact)
        {
            var contactUs = new ContactUs
            {
                Id = id,
                Email = createContact.Email,
                Phone = createContact.Phone,
                WebSite = createContact.WebSite,
                Address = createContact.Address
            };

            var status = await _contactUsService.UpdateContactUsAsync(contactUs);

          

            if (status == 1)
                return Ok(contactUs);
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

    }
}
