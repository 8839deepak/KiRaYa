﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KiRaYa.Models;
namespace KiRaYa.Controllers
{
    public class HOMEController : Controller
    {
        // GET: HOME

        public ActionResult Login()
        {
            ViewData["msg"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login Model)
        {

            List<Login> ListUsers = new Login().GetAll();
            Login ObjUSers = ListUsers.Find(x => x.EmailID == Model.EmailID && x.Password == Model.Password );
            if (ObjUSers != null)
            {
                if (Session["EmailID"] != null && Session["resulttype"] != null)
                {
                    return View();
                }

                else if (Session["EmailID"] != null && Session["resulttype"] != null)
                {
                    return Redirect(Request.UrlReferrer.ToString());
                }

                Session["ID"] = ObjUSers.ID;
                Session["UserName"] = ObjUSers.UserName;
                Session["UserID"] = ObjUSers.UserID;
                Session["EmailID"] = ObjUSers.EmailID;
                return RedirectToAction("Admin");
            }

            ViewData["msg"] = "Invalid User Name or Password";

            return View();
        }
        public ActionResult Admin( )
        {
            int TR = new KiRaYa.Models.RoomTable().GetAll().Count();
            if(TR>5)
            {
                
                List<RoomTable> yr = new RoomTable().GetAll();
                yr = yr.FindAll(x => x.DDID == 1);
                ViewData["mgs"] = "some room is left";

                return (yr);

            }
            int QT = new KiRaYa.Models.Rental().GetAll().Count();
            if (QT < 2)
            {
                List<RoomTable> yr = new RoomTable().GetAll();
                yr = yr.FindAll(x => x.DDID == 1);
                ViewData["mgs"] = "some room is left";
            }
            else
            {
                ViewData["mgs"] = "All Room is Full";
            }

            return View(TR);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Login", "HOME");
        }
        public ActionResult Userlist()
        {
            Login userlist = new Login();
            List<Login> listuser = new Login().GetAll();
            return View(listuser);
        }
        public ActionResult CreateEdit(int ID)
        {
            Login loginuser = new Login();
            loginuser = loginuser.GetOne(ID);
            return View(loginuser);
        }
        [HttpPost]
        public ActionResult CreateEdit(Login loginuser)
        {
            int i = loginuser.Save();
            if (i > 0)
                return RedirectToAction("Userlist");
            return RedirectToAction("Error");

        }
        public ActionResult forgetPassword(int ID)
        {
            ViewData["msg"] = "";
            Login OBJFORGET = new Login();
            OBJFORGET = OBJFORGET.GetOne(ID);
            return View(OBJFORGET);

        }
        [HttpPost]
        public ActionResult forgetPassword(Login OBJFORGET)
        {
            //////---------------send E-mail-------------------- 

            int i = 1;
            string msg = "<h1> Wel com in iT-Helper </h1> <br> <h2> click here to activate This Email <a href='http://192.168.1.8/Default/Activation?TokenKey=" + OBJFORGET.ID + "'><b>Activate</b></a></h2><br> <br> any problem call <u><mark>8839070602</mark><u>";
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage("957501deepak@gmail.com", OBJFORGET.EmailID, "iT-Helper", msg);
            mail.IsBodyHtml = true;
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new System.Net.NetworkCredential("957501deepak@gmail.com", "deepak1998@");
            //client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Timeout = 20000;
            client.Send(mail);
            if (i > 0)
            {
                OBJFORGET.Save();
                ViewData["mgs"] = "Your Password is updated successfully";
            }
            else
            {
                ViewData["mgs"] = "try again";
            }
            return RedirectToAction("Login");
            //--------------End sent E-mail ----------------
            //MailMessage mailmsg = new MailMessage();
            //SmtpClient smtpclient = new SmtpClient();
            //mailmsg.To.Add(Email.EmailID);
            //mailmsg.CC.Add("957501deepak@gmail.com");
            //mailmsg.Subject ="forget";
            //mailmsg.From = new MailAddress("957501deepak@gmail.com");
            //mailmsg.Body = "your password";
            //mailmsg.Priority = MailPriority.High;

            //smtpclient.Port = 587;
            //smtpclient.Host = "smtp.gmail.com";
            //smtpclient.EnableSsl = true;
            //smtpclient.UseDefaultCredentials = false;
            //smtpclient.Credentials = new NetworkCredential("957501deepak@gmail.com", "deepak1998@");
            //smtpclient.Send(mailmsg);


            //    Login objlogin = new Login();
            //     JObject Result = new JObject();
            //    JObject ParaMeters = JObject.Parse(Obj);
            //    string MobileNo = ParaMeters["MobileNo"].ToString();
            //    string NewPassword = ParaMeters["Pass"].ToString();
            //    Login ObjUser = new Login().GetOne(MobileNo);
            //    if (ObjUser.UserCode > 0)
            //    {
            //        ObjUser.Password = NewPassword;
            //        if (ObjUser.save() > 0)
            //        {
            //            Result.Add("Status", 200);
            //            Result.Add("MSG", "Change Successfully");
            //        }
            //        else
            //        {
            //            Result.Add("Status", 400);
            //            Result.Add("MSG", "Unable To Change  Password Try after some time ");
            //        }
            //    }
            //    else
            //    {
            //        Result.Add("Status", 400);
            //        Result.Add("MSG", "Invalid Mobile Number ");

            //    }
            //    return Result;
            //}


        }
        public ActionResult Delete(int ID)
        {
            Login ObjCon = new Login();
            int d = ObjCon.Dell(ID);
            if (d > 0)

                return RedirectToAction("UserList");
            return RedirectToAction("Error");

        }
        public ActionResult SingUP(int ID)
        {
            Login OBJLogin = new Login();
            OBJLogin = OBJLogin.GetOne(ID);
            return View(OBJLogin);

        }
        [HttpPost]
        public ActionResult SingUp(Login OBJLogin)
        {
            int i = OBJLogin.Save();
            if (i > 0)
                return RedirectToAction("Login");
            return RedirectToAction("Error");
        }
    }

}

