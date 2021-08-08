using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedisProject.Models;
using Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RedisProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;


        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult GetById()
        {
            _productService.GetProductById(3);
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
