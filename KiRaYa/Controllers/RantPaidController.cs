using KiRaYa.Models;
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
             MailMessage mailmsg = new MailMessage();
            SmtpClient smtpclient = new SmtpClient();
           // mailmsg.To.Add(rant.EmailID);
            mailmsg.CC.Add("957501deepak@gmail.com");
            mailmsg.Subject = "forget";
            mailmsg.From = new MailAddress("957501deepak@gmail.com");
            string html = "<div class='card'>";
            html+=" < div class='card-header'>";
            html += "< h4 style = 'text-align:center;color:#000000' >< b > RantPaid </ b ></ h4 >";
            html += "< hr >";
            html += " </ div >";
            html += " < div class='card-body'>";
            html += " <form action = '/RantPaid/CreateEdit?RPID=0' method='post'>";
            html += "<input name = '__RequestVerificationToken' type='hidden' value='hBQBLD02eaDW_0FLcTJ11X8ZyFpcBZUmmN8VsXvggFcJQgY58_dPvaSVd7lzWfZyrHZRhsaMpNP_6icyt-sp7GXYw65Ajs49gHgJ68afw1I1'><input data-val='true' data-val-number='The field Rental ID must be a number.' data-val-required='The Rental ID field is required.' id='RPID' name='RPID' type='hidden' value='0'> ";
             html +="<div class='row'>";
             html += "<span class='field-validation-valid text-danger' data-valmsg-for='RPID' data-valmsg-replace='true'></span>";
            html += "<div class='col-md-4'>";
            html += "<label for='Retal_Name'>Retal Name</label>";
            html += "<select class='form-control' data-val='true' data-val-number='The field Rantal Name must be a number.' data-val-required='The Rantal Name field is required.' id='RantalID' name='RantalID' required='required'><option value = '' > Select Rental</option>";
            html += " <option value = '1'> DEEPAK VERMA</option>";
             html += "  </select>";
            html += "<span class='field-validation-valid text-danger' data-valmsg-for='RantalID' data-valmsg-replace='true'></span>";
                       </div>
                       <div class="col-md-4">
                       <label for="Room_Number">Room Number</label>
                       <select class="form-control" data-val="true" data-val-number="The field Room Number must be a number." data-val-required="The Room Number field is required." id="RID" name="RID" required="required"><option value = "6" > R006 </ option >
                       < option value="5">R005</option>
                       <option value = "4" > R004 </ option >
                       < option value="3">R003</option>
                       <option value = "2" > R002 </ option >
                       < option value="1">R001</option>
                       </select>
                       <span class="field-validation-valid text-danger" data-valmsg-for="RID" data-valmsg-replace="true"></span>
                       </div>
                       <div class="col-md-4">
                       <label for="Last_Reading">Last Reading</label>
                       <input class="form-control text-box single-line" data-val="true" data-val-number="The field Last Reading must be a number." data-val-required="The Last Reading field is required." id="LastReading" name="LastReading" required="required" type="number" value="0">
                       <span class="field-validation-valid text-danger" data-valmsg-for="LastReading" data-valmsg-replace="true"></span>
                       </div>
                       <div class="col-md-4">
                       <label for="Current_Reading">Current Reading</label>
                       <input class="form-control text-box single-line" data-val="true" data-val-number="The field Current Reading must be a number." data-val-required="The Current Reading field is required." id="CurrentReading" name="CurrentReading" required="required" type="number" value="0">
                       <span class="field-validation-valid text-danger" data-valmsg-for="CurrentReading" data-valmsg-replace="true"></span>
                       </div>
                       <div class="col-md-4">
                       <label for="Electricity_Used">Electricity Used</label>
                       <input class="form-control text-box single-line" data-val="true" data-val-number="The field Electricity Used must be a number." data-val-required="The Electricity Used field is required." id="UsedElectricity" name="UsedElectricity" onchange="my();" type="number" value="0">
                       <span class="field-validation-valid text-danger" data-valmsg-for="UsedElectricity" data-valmsg-replace="true"></span>
                       </div>
                       <div class="col-md-4">
                       <label for="Total_Rant">Total Rant</label>
                       <input class="form-control text-box single-line" data-val="true" data-val-number="The field Total Rant must be a number." data-val-required="The Total Rant field is required." id="TotalRant" name="TotalRant" onchange="my();" type="number" value="0">
                       <span class="field-validation-valid text-danger" data-valmsg-for="TotalRant" data-valmsg-replace="true"></span>
                       </div>
                       <div class="col-md-4">
                       <label for="Total_Pay">Total Pay</label>
                       <input class="form-control text-box single-line" data-val="true" data-val-number="The field Total Pay must be a number." data-val-required="The Total Pay field is required." id="TotalPay" name="TotalPay" onchange="change();" type="number" value="0">
                       <span class="field-validation-valid text-danger" data-valmsg-for="TotalPay" data-valmsg-replace="true"></span>
                       </div>
                       <div class="col-md-4">
                       <label for="Amount">Amount</label>
                       <input class="form-control text-box single-line" data-val="true" data-val-number="The field Pay Amount must be a number." data-val-required="The Pay Amount field is required." id="Amt" name="Amt" onchange="change();" type="number" value="0">
                       <span class="field-validation-valid text-danger" data-valmsg-for="Amt" data-valmsg-replace="true"></span>
                      </div>
                      <div class=" col-sm-1">
                      <label class="text-white" for="SAVE">SAVE</label>
                      <input type = "submit" value="SaVe" class="btn btn-success">
                      </div>
                      </div>
                     </form>           
                    </div>
                    </div>
            mailmsg.Body = "";
            mailmsg.Priority = MailPriority.High;

            smtpclient.Port = 587;
            smtpclient.Host = "smtp.gmail.com";
            smtpclient.EnableSsl = true;
            smtpclient.UseDefaultCredentials = false;
            smtpclient.Credentials = new NetworkCredential("957501deepak@gmail.com", "deepak1998@");
            smtpclient.Send(mailmsg);

            return View();
        }

    }
}