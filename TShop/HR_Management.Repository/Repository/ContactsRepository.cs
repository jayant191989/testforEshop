using HR_Management.Context;
using HR_Management.Models;
using HR_Management.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Repository.Repository
{
   
    public class ContactsRepository : GenericRepository<Contact>, IContactRepository 
    {
        public ContactsRepository(ApplicationDbContext context)
              : base(context)
        {

        }
        public Contact GetById(Guid id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
            //  return FindBy(x => x.Id == id).FirstOrDefault();
        }

     
    }

    public interface IContactRepository : IGenericRepository<Contact>
    {
        Contact GetById(Guid id);
    }
}
