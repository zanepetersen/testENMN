using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ENMN.Models;

namespace ENMN.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Person u)
        {
            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                using (zpeterseEntities dc = new zpeterseEntities())
                {
                    var v = dc.People.Where(a => a.Email.Equals(u.Email) && a.Password.Equals(u.Password)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LoggedUserID"] = v.PersonID.ToString();
                        Session["LoggedUserFirstName"] = v.FirstName.ToString();
                        Session["LoggedUserLastName"] = v.LastName.ToString();
                        Session["loggedUserType"] = v.Type.ToString();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(u);
        }

        public ActionResult AfterLogin()
        {
            if (Session["LoggedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Logoff()
        {
            System.Web.HttpContext.Current.Response.Cookies.Clear();
            Session["LoggedUserID"] = null;
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return Redirect("Login");
        }

    }
}