using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pos.DataAccess.Model;
using Pos.DataAccess.Repositories;
using Pos.Web.Models;

namespace Pos.Web.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Details(string barcode)
        {
            if (barcode == "")
            {
                return NotFound();
            }
            return View(ProductsRepository.getProductsList());
        }

        //public JsonResult ApiDetails(string barcode)
        //{
        //    return Json(new{ Id =barcode });
        //}
    }
}