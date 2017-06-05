using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vera.Domain.Entity.Identity;

namespace Vera.Controllers
{
    public class EditorController : Controller
    {
        // GET: Editor
        [Authorize(Roles = "manager, admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}