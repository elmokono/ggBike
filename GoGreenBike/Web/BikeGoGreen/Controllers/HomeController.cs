using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BikeGoGreen.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new Model.DatabaseModelDataContext())
            {                
                return View(new Models.HomeIndexModel());
            }
        }
    }
}