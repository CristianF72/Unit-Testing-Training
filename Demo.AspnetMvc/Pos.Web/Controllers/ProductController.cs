using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pos.Web.Models;

namespace Pos.Web.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Details(string barcode)
        {
            var product = new ProductViewModel { Id = "test"};

            return View(product);
        }

        //public JsonResult ApiDetails(string barcode)
        //{
        //    return Json(new{ Id =barcode });
        //}
    }
}