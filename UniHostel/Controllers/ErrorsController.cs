using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniHostel.Controllers
{
    public class ErrorsController : Controller
    {

        public ActionResult Default()
        {
            return PartialView();
        }

        public ActionResult BadRequest()
        {
            return PartialView("400");
        }

        public ActionResult Forbidden()
        {
            return PartialView("403");
        }
        

        public ActionResult NotFound()
        {
            return PartialView("404");
        }

        public ActionResult InternalServerError()
        {
            return PartialView("500");
        }
        

        public ActionResult ServiceUnavailable()
        {
            return PartialView("503");
        }

        public ActionResult Test()
        {
            throw new Exception();
        }
        
    }
}