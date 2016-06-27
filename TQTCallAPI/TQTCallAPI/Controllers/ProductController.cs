using SSC.Core.Api.Models.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TQTCallAPI.Services.Product;

namespace TQTCallAPI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController()
        {
            _productService = _productService.IsNull() ? new ProductService() : _productService;
        }
        public ActionResult Index()
        {
            var model = _productService.GetListProducts();
            return View(model);
        }
    }
}