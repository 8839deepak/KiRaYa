using System;
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
            List<RoomTable> roomTables = new RoomTable().GetAll();
            int TR = roomTables.FindAll(x => x.DDID == 2).Count();
            if (TR!=0)
            {
                ViewData["mgs"] = "some room is left";
            }
            else
            {
                ViewData["mgs"] = "All Room is Full";
            }
            int RT = roomTables.FindAll(x => x.DDID == 3).Count();
            if (RT!= 0)
            {
                
                TempData["mgs"] = "some room is left";
            }
            else
            {
                TempData["mgs"] = "All Room is Full";
            }
            int QT = roomTables.FindAll(x => x.DDID == 4).Count();
            if (QT!=0)
            {
                TempData["mgs"] = "some room is left";

            }
            else
            {
                TempData["mgs"] = "All Room is Full";
            }
            int yt = roomTables.FindAll(x => x.DDID == 1).Count();
            if(yt!=0)
            {
                TempData["mgs"] = "some room is left";
            }
            else
            {
                TempData["mgs"] = "All Room is Full";
            }

            return View(ViewData["mgs"]);
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
            ForgetPassword OBJFORGET = new ForgetPassword();
            OBJFORGET = OBJFORGET.GetOne(ID);
            return View(OBJFORGET);

        }
        [HttpPost]
        public ActionResult forgetPassword(ForgetPassword OBJFORGET)
        {
            //////---------------send E-mail-------------------- 

          
             
            string msg = "<h1> Wel com in iT-Helper </h1> <br> <h2> click here to activate This Email <a href='http://192.168.1.8/Default/Activation?TokenKey=" + OBJFORGET.FID + "'><b>Activate</b></a></h2><br> <br>Your Password is updated successfully<br><br> any problem call <u><mark>8839070602</mark><u>";
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage("957501deepak@gmail.com", OBJFORGET.EmailID, "iT-Helper", msg);
            mail.IsBodyHtml = true;
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new System.Net.NetworkCredential("957501deepak@gmail.com", "deepak1998@");
            //client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Timeout = 20000;
            //("Msg", "Password Change Successful");
            
            List<Login> listlogin = new Login().GetAll();
            Login login = listlogin.Find(x => x.EmailID == OBJFORGET.EmailID);
            if(login!=null)
            {

                login.Password=OBJFORGET.NewPassword;
                login.Save();

            }
            
            client.Send(mail);
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

