using KiRaYa.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KiRaYa.Controllers
{
    public class RentalController : Controller
    {
        // GET: Rental
        public ActionResult Index()
        {
            List<Rental> listrental = new Rental().GetAll();
            if (Session["UserID"].ToString().Contains("2"))
            {
                int ID = int.Parse(Session["ID"].ToString());
                listrental = listrental.FindAll(x => x.RantalID == ID);
            }else
            {
                  listrental = new Rental().GetAll();
            }
            return View(listrental);
        }
        // Create and edit Rental
        public ActionResult CreateEdit(int RentalID)
        {
            bool Status = Convert.ToBoolean(Session["ID"]);
            int ID = Convert.ToInt32(Session["ID"]);
            Rental objpad = new Rental();
            if (RentalID > 0)
            {
                objpad = objpad.GetOne(RentalID);
            }
            if ( Request.QueryString["RID"] != null)
           {
                  ViewData["RID"]=(Request.QueryString["RID"]);
            }
            if(Request.QueryString["DDID"]!=null)
            {
                ViewData["DDID"]=(Request.QueryString["DDID"]);
                objpad.ID = ID;
                objpad.Status = Status;
            }
            if (Request.QueryString["Status"] != null)
            {
                    ViewData["Status"] = (Request.QueryString["Status"]);
            }
               
                ViewData["msg"] = "";
                
            return View(objpad);
        }
        [HttpPost]
        public ActionResult CreateEdit(Rental objpad, HttpPostedFileBase PhotoPath,HttpPostedFileBase AdharcardPhoto ,HttpPostedFileBase PanPhoto)
         {
            string location = "";
            string loca = "";
           string tion = "";
            if (PhotoPath != null)
            {
                string pic = Path.GetFileName(PhotoPath.FileName);
                location = @"\Image\" + pic;
                string path = Path.Combine(Server.MapPath("~/Image"), pic);
                PhotoPath.SaveAs(path);
                
            }
            if (AdharcardPhoto != null)
            {
                string pic = Path.GetFileName(AdharcardPhoto.FileName);
                loca= @"\Image\" + pic;
                string path = Path.Combine(Server.MapPath("~/Image"), pic);
                AdharcardPhoto.SaveAs(path);

            }
            if (PanPhoto != null)
            {
                string pic = Path.GetFileName(PanPhoto.FileName);
                tion = @"\Image\" + pic;
                string path = Path.Combine(Server.MapPath("~/Image"), pic);
                PanPhoto.SaveAs(path);

            }
            if (objpad.RantalID>0 && PhotoPath==null)
            {
                Rental OldObj = new Rental().GetOne(objpad.RantalID);
                location = OldObj.PhotoPath;
            }
            if (objpad.RantalID > 0 && AdharcardPhoto == null)
            {
                Rental OldObj = new Rental().GetOne(objpad.RantalID);
                loca= OldObj.AdharcardPhoto;
            }
            if (objpad.RantalID > 0 && PanPhoto == null)
            {
                Rental OldObj = new Rental().GetOne(objpad.RantalID);
                tion = OldObj.PanPhoto;
            }
            objpad.PhotoPath = location;
            objpad.AdharcardPhoto = loca;
            objpad.PanPhoto = tion;
            int i = objpad.Save();
           if(objpad.RID>0)
            {
                RoomTable objroom = new RoomTable().GetOne(objpad.RID);
                objroom.Status = objpad.Status;
                objroom.Save();
            }
            if (i > 0)
            

            return RedirectToAction("Index");
            return RedirectToAction("Error");
        }

        public ActionResult Delete(int ID)
        {
            Rental ObjCon = new Rental();
            int d = ObjCon.Dell(ID);
            if (d > 0)
                  
            return RedirectToAction("Index");
            return RedirectToAction("Error");

        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult termpolcy(int RID , int DDID)
        {
            ViewData["RID"] = RID;
            ViewData["DDID"] = DDID;
           
            
            return View();
        }
        public ActionResult HIDEFUNCTION(Rental objpad)
        {
            return View();
        }
    }
}