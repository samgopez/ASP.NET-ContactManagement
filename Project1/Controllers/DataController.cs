using Project1.Models;
using Project1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project1.Controllers
{
    public class DataController : ApiController
    {
        static IContactService contactService = new ContactService();

        [HttpPost]
        public object AddContact(Contact contact)
        {
            long generatedId = contactService.AddContactToDb(contact);

            return new { ContactId = generatedId };

        }

        [HttpGet]
        public IEnumerable<Contact> GetAllContactList()
        {
            IEnumerable<Contact> contactList = contactService.GetContactList();

            return contactList;
        }

        public Contact GetContact(int id)
        {
            Contact contact = contactService.GetContactById(id);

            return contact;
        }

        [HttpPut]
        public object UpdateContact(int id, Contact contact)
        {
            contact.ContactId = id;

            if (contactService.UpdateContactDb(contact))
            {
                return new { Status = true };
            }
            else
            {
                return new { Status = false };
            }
        }

        [HttpDelete]
        public object DeleteContact(int id)
        {
            if (contactService.DeleteContactDb(id))
            {
                return new { Status = true };
            }
            else
            {
                return new { Status = false };
            }
        }
    }
}
