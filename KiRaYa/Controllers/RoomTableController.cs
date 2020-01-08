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
        // GET: RoomTable only for availabel room

        public ActionResult Index()
        {
           
            RoomTable objpad = new RoomTable();
            List<RoomTable> ListRant = new RoomTable().GetAll();
            if (Request.QueryString["Status"] != null )
            {
               
                ListRant = ListRant.FindAll(x => x.Status == true);

            }
            else
            {

                //int RID = int.Parse(Request.QueryString["Status"]);
                // int status = bool.Parse(Request.QueryString["Status"]);
                ListRant = ListRant.FindAll(x => x.Status == false);
            }


            return View(ListRant);
        }
        // list for all the rooms only for  admin
        public ActionResult AdminIndex()
        {
            RoomTable objpad = new RoomTable();
            List<RoomTable> ListRat = new RoomTable().GetAll();
            return View(ListRat);
        }
        //list for used room 

        public ActionResult UsedIndex()
        {
            RoomTable objpad = new RoomTable();
            List<RoomTable> ListRant = new RoomTable().GetAll();
            if (Request.QueryString["Status"] != null)
            {     
                ListRant = ListRant.FindAll(x => x.Status == true);
            }
            else
            {
                ListRant = ListRant.FindAll(x => x.Status == true);
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
                return RedirectToAction("AdminIndex");
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
            if (Request.QueryString["DDID"] != null && Request.QueryString["Status"] != null)
            {

                int DDID = int.Parse(Request.QueryString["DDID"]);
                ListRant = ListRant.FindAll(x => x.DDID == DDID && x.Status == false);

            }

            return View(ListRant);
        }


    }
}