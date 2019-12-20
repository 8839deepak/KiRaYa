﻿using KiRaYa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        public ActionResult SentMail(RantPaid rant)
        {
           
            List<Rental> listrental = new Rental().GetAll();
            Rental Objrental = listrental.Find(x => x.RantalID ==  rant.RantalID);
            List<RoomTable> listroom = new RoomTable().GetAll();
            RoomTable Objroom = listroom.Find(x => x.RID == Objrental.RID);
             MailMessage mailmsg = new MailMessage();
            SmtpClient smtpclient = new SmtpClient();
           // mailmsg.To.Add(rant.EmailID);
            mailmsg.CC.Add("957501deepak@gmail.com");
            mailmsg.Subject =  " Room Rent";
            mailmsg.From = new MailAddress("957501deepak@gmail.com");
            string html = "<div class='card'>";
            html += "<div class='card-header'>";
            html += "<h4 style = 'text-align:center;color:#000000' ><b>Room Rent</b></h4>";
            html += "<hr>";
            html += "</div>";
            html += "<input name = '__RequestVerificationToken' type='hidden' value='hBQBLD02eaDW_0FLcTJ11X8ZyFpcBZUmmN8VsXvggFcJQgY58_dPvaSVd7lzWfZyrHZRhsaMpNP_6icyt-sp7GXYw65Ajs49gHgJ68afw1I1'><input data-val='true' data-val-number='The field Rental ID must be a number.' data-val-required='The Rental ID field is required.' id='RPID' name='RPID' type='hidden' value='0'>";
            html += "<div>";
            html += "<label for='Retal_Name'>Retal Name</label>";
            html += "<select class='form-control' data-val='true' data-val-number='The field Rantal Name must be a number.' data-val-required='The Rantal Name field is required.' id='RantalID' name='RantalID' required='required'><option value = '' >" + Objrental.Name + "</option>";
            html += "</div>";
            html += "<div>";
            html += "<label for='Room_Number'>Room Number</label>";
            html += "<select class='form-control' data-val='true' data-val-number='The field Room Number must be a number.' data-val-required='The Room Number field is required.' id='RID' name='RID' required='required'><option value = '6' > "+ Objroom .RoomNumber+ "</ option >";
            html += "</div>";
            html += "<div>";
            html += "<label for='Last_Reading'>Last Reading</label>----" + rant.LastReading + "";
            html += "</div>";
            html += "<div>";
            html += " <label for='Current_Reading'>Current Reading</label>----" + rant.CurrentReading + "";
            html += "</div>";
            html += "<div>";
            html += " <label for='Electricity_Used'>Electricity Used</label>-----" + rant.UsedElectricity + "";
           html += "</div>";
            html += "<div>";
            html += " <label for='Total_Rant'>Total Rant</label>------" + rant.TotalRant + "";
            html += "</div>";
            html += "<div>";
            html += " <label for='Total_Pay'>Total Pay</label>----" + rant.TotalPay + "";
            html += "</div>";
            html += "<div>";
            html += " <label for='Amount'>Amount</label>----" + rant.Amt + "";
            html += "</div>";
            mailmsg.Body = html;
            mailmsg.Priority = MailPriority.High;
            mailmsg.IsBodyHtml = true;
            smtpclient.Port = 587;
            smtpclient.Host = "smtp.gmail.com";
            smtpclient.EnableSsl = true;
            smtpclient.UseDefaultCredentials = false;
            smtpclient.Credentials = new NetworkCredential("957501deepak@gmail.com", "deepak1998@");
            smtpclient.Send(mailmsg);



            return RedirectToAction("Index");
        }

    }
}