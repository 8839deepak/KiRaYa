using KiRaYa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KiRaYa.Controllers
{
    public class RoomTableController : Controller
    {
        // GET: RoomTable
        public ActionResult Index()
        {
            RoomTable objpad = new RoomTable();
            List<RoomTable> ListRant = new RoomTable().GetAll();
            if (Request.QueryString["DDID"] != null)
            {
                int DDID = int.Parse(Request.QueryString["DDID"]);
                ListRant = ListRant.FindAll(x => x.DDID == DDID);
            }

            return View(ListRant);
        }
        // Create and edit RoomTable
        public ActionResult CreateEdit(int RID)
        {
            RoomTable objpad = new RoomTable();
            if (RID > 0)
            {
                objpad = objpad.GetOne(RID);
            }
            return View(objpad);
        }
        [HttpPost]
        public ActionResult CreateEdit(RoomTable objpad)
        {
            int i = objpad.Save();

            if (i > 0)
                return RedirectToAction("Index");
            return RedirectToAction("Error");
        }

        public ActionResult Delete(int ID)
        {
            RoomTable ObjCon = new RoomTable();
            int d = ObjCon.Dell(ID);
            if (d > 0)
                return RedirectToAction("Index");
            return RedirectToAction("Error");

        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult FilterFunction()
        {
            RoomTable objpad = new RoomTable();
            List<RoomTable> ListRant = new RoomTable().GetAll();
            if (Request.QueryString["DDID"] != null)
            {
                int DDID = int.Parse(Request.QueryString["DDID"]);
                ListRant = ListRant.FindAll(x => x.DDID == DDID);
            }

            return View(ListRant);
        } 
         
         
    }
}