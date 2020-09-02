using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.ContactUsServ
{
    public class ContactUsService:IContactUsService
    {
        private readonly ApplicationDbContext _dataContext;

        public ContactUsService(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }

        public async Task<ContactUs> GetContactUsAsync()
        {
            return await _dataContext.ContactUs.FirstOrDefaultAsync();
        }

        public async Task<int> CreateContactUsAsync(ContactUs contactUs)
        {
            await _dataContext.ContactUs.AddAsync(contactUs);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public async Task<int> UpdateContactUsAsync(ContactUs contactUs)
        {
            _dataContext.ContactUs.Update(contactUs);
            var updated = await _dataContext.SaveChangesAsync();
            return updated;
        }
    }
}
