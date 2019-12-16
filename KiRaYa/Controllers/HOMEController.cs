using System;
using System.Collections.Generic;
using System.Linq;
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
            Login ObjUSers = ListUsers.Find(x => x.UserName == Model.UserName && x.Password == Model.Password);
            if (ObjUSers != null)
            {
                Session["ID"] = ObjUSers.ID;
                Session["Display_Name"] = ObjUSers.UserName;
                Session["UserType"] = ObjUSers.UserType;
                return RedirectToAction("Admin");
            }

            ViewData["msg"] = "Invalid User Name or Password";

            return View();
        }
        public ActionResult Admin()
        {

            return View();


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
            loginuser = loginuser.addloginuser(ID);
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
        public ActionResult forgetPassword()
        {
            ViewData["msg"] = "";
             

            return View();

        }
        [HttpPost]
        public ActionResult forgetPassword(Login model)
        {
            //---------------send E-mail-------------------- 
            string msg = "<h1> Wel com in iT-Helper </h1> <br> <h2> click here to activate This Email <a href='http://192.168.1.8/Default/Activation?TokenKey=" + model.ID + "'><b>Activate</b></a></h2><br> <br> any problem call <u><mark>8839070602</mark><u>";
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage("957501deepak@gmail.com", model.EmailID, "iT-Helper", msg);
            mail.IsBodyHtml = true;
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new System.Net.NetworkCredential("957501deepak@gmail.com", "8839verma13@");
            //client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Timeout = 20000;
            client.Send(mail);
            //--------------End sent E-mail ----------------

            ViewData["mgs"] = "Your Password is updated successfully";
            return RedirectToAction("Login");
        }
        public ActionResult Delete(int ID)
        {
            Login ObjCon = new Login();
            int d = ObjCon.Dell(ID);
            if (d > 0)

                return RedirectToAction("UserList");
            return RedirectToAction("Error");

        }
    }

}

