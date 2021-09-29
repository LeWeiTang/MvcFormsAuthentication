using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcFormsAuthentication.Controllers
{
    [Authorize(Users = "Kevin@gmail.com,Mary@gmail.com")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}