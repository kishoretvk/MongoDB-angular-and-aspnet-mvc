using MongoDB.Driver;
using RealEstate.App_Start.DataContext;
using RealEstate.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Controllers
{
    public class HomeController : Controller
    {
        //create a context for mongodb from datacontext class
         RealEstateContext context = new RealEstateContext();
        public ActionResult Index()   
        {
            //using class object to make db connection
            context.DataBase.GetStats();
            return Json(context.DataBase.Server.BuildInfo, JsonRequestBehavior.AllowGet);
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
    }
}