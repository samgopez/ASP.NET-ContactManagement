using Project1.Models;
using Project1.Services;
using System.Collections.Generic;
using System.Web.Mvc;
using System;
using System.Linq;

namespace Project1.Controllers
{
    public class HomeController : Controller
    {

        static IContactService contactService = new ContactService();

        public ActionResult Index()
        {
            return View(contactService.GetContactList());
        }

        public ActionResult ContactDetails(int id)
        {
            Contact contact = contactService.GetContactById(id);

            return View(contact);
        }

        public ActionResult NewContact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewContact(Contact contact)
        {
            long contactId = contactService.AddContactToDb(contact);

            if (contactId > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult EditContact(int id)
        {
            Contact contact = contactService.GetContactById(id);

            return View(contact);
        }

        [HttpPost]
        public ActionResult EditContact(int id, Contact contact)
        {
            if (contactService.UpdateContactDb(contact))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult RemoveContact(int id)
        {
            contactService.DeleteContactDb(id);

            return RedirectToAction("Index");
        }

    }
}
