using Project1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Services
{
    interface IContactService
    {
        long AddContactToDb(Contact contact);
        IEnumerable<Contact> GetContactList();
        Contact GetContactById(int id);
        bool UpdateContactDb(Contact contact);
        bool DeleteContactDb(int id);
    }
}
