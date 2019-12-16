using KiRaYa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KiRaYa.Controllers
{
    public class RantPaidController : Controller
    {
        // GET:List RantPaid
        public ActionResult Index()
        {
            return View(new RantPaid().GetAll());
        }
        // Create and edit Rantpaid
        public ActionResult CreateEdit( int RPID)
        {
            RantPaid objpad = new RantPaid();
            if (RPID > 0)
            {
                objpad = objpad.GetOne(RPID);
            }
            return View(objpad);
        }
        [HttpPost]
        public ActionResult CreateEdit(RantPaid objpad)
        {
            int i = objpad.Save();

            if (i > 0)
                return RedirectToAction("Index");
            return RedirectToAction("Error");
        }

        public ActionResult Delete(int ID)
        {
            RantPaid ObjCon = new RantPaid();
            int d = ObjCon.Dell(ID);
            if (d > 0)
                return RedirectToAction("Index");
            return RedirectToAction("Error");

        }
        public JsonResult GetAmt(int PRID)
        {
            List<Rental> ListRentals = new Rental().GetAll();
            Rental OnjRentals = ListRentals.Find(x => x.RantalID == PRID);
            List<RoomTable> ListRooms = new RoomTable().GetAll();
            RoomTable ObjRooms = ListRooms.Find(x => x.RID == OnjRentals.RID);

            List<RantPaid> ListRentPaids = new RantPaid().GetAll();
            RantPaid RentPaidsobj = ListRentPaids.Find(x => x.RantalID == PRID);
            int RentPaid = ObjRooms.RantAmt;



            return Json(new { RID = ObjRooms.RID, RoomNumber = ObjRooms.RoomNumber, RantAmt = RentPaid }, JsonRequestBehavior.AllowGet);
        }

    }
}