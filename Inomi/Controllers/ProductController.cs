using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using DataLayer;
using Models;
using Newtonsoft.Json;
using System.Xml;
using System.Data;

namespace Inomi.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [SessionExpireAttribute]
        public ActionResult Product()
        {
            DataTable dtProductDetails = new DataTable();
            dtProductDetails = StudentCon.ProductDetails();
            return View(dtProductDetails);
        }

        public ActionResult ProductInfo(string Id)
        {
            DataTable dtProductDetails = new DataTable();
            dtProductDetails = StudentCon.ProductDetailsInfo(Id);
            return View(dtProductDetails);
        }
    }
}