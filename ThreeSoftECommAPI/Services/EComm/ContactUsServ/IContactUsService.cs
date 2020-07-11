using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.ContactUsServ
{
   public interface IContactUsService
    {
        Task<ContactUs> GetContactUsAsync();
        //Task<ContactUs> GetCategoryByIdAsync(Int32 categoryId);
        Task<int> CreateContactUsAsync(ContactUs contactUs);
        Task<int> UpdateContactUsAsync(ContactUs contactUs);
    }
}
