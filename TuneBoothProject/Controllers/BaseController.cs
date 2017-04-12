using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuneBoothProject.App_Start;

namespace TuneBoothProject.Controllers
{

    [CustomError]
    [CustomActionFilter]
    public class BaseController : Controller
    {
        // GET: Base
        public ActionResult Error()
        {
            return View("Error");
        }
    }
}