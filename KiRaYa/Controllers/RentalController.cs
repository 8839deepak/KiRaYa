﻿using KiRaYa.Models;
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
            if(Session["UserType"].ToString().Contains("User"))
            {
                int ID = int.Parse(Session["ID"].ToString());
                listrental = listrental.FindAll(x => x.createBy == ID);
            }else
            {
                  listrental = new Rental().GetAll();
            }


            return View(listrental);
        }
        // Create and edit Rental
        public ActionResult CreateEdit(int RentalID)
        {
            if (Request.QueryString["RID"] != null)
            {
                int rID = int.Parse(Request.QueryString["RID"]);
            }
            //ViewData["msg"] = "";
            Rental objpad = new Rental();
            if (RentalID > 0)
            {
                objpad = objpad.GetOne(RentalID);
            }
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
        public ActionResult termpolcy()
        {
            return View();
        }
        
    }
}