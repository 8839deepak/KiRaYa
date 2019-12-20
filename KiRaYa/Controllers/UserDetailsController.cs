using KiRaYa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KiRaYa.Controllers
{
    public class UserDetailsController : Controller
    {
        // GET: UserDetails
        public ActionResult Index()
        {
            UserDetails userDetails = new UserDetails();
            List<UserDetails> listuser = userDetails.GetAll();
            return View(listuser);
        }
        public ActionResult CreateEdit(int UserID)
        {
            UserDetails userDetails = new UserDetails();
            if (UserID>0)
            {
                userDetails = userDetails.GetOne(UserID);

            }
            return View(userDetails);
        }
        [HttpPost]
        public ActionResult CreateEdit(UserDetails userDetails)
        {
            int i = userDetails.Save();

            if (i > 0)
                return RedirectToAction("Index");
            return RedirectToAction("Error");
        }
        public ActionResult Delete(int ID)
        {
            UserDetails ObjCon = new UserDetails();
            int d = ObjCon.Dell(ID);
            if (d > 0)

                return RedirectToAction("Index");
            return RedirectToAction("Error");

        }
        public ActionResult Error()
        {
            return View();
        }
    }
}