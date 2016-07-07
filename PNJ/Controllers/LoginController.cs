using PNJ.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PNJ.Controllers
{
    public class LoginController : Controller
    {
        private Admin_TCBEntities db = new Admin_TCBEntities();

        // GET: Login
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("index", "home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AjaxLogin(string username, string password, string AD)
        {
            var t = db.Users.Where(x => x.idUser == username && x.passWord == password);
            if (t.Any())
            {
                Session["user"] = username;
                return Json(username + " " + password + " " + AD);
            }
            else
            {
                return Json(username + " " + password + " " + AD);
            }
        }

        [HttpGet]
        [ActionName("removeSS")]
        public JsonResult RemoveSS()
        {
            try
            {
                Session["user"] = null;
                Redirect("~/login");
                return Json("clear", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("failed", JsonRequestBehavior.AllowGet);
            }
        }
    }
}