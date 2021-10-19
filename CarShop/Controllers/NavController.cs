using CarShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Controllers
{
    public class NavController : Controller
    {
        private readonly CarShopContext context;
        public NavController(CarShopContext context) => this.context = context;
        public PartialViewResult Menu(string control = "Cars", string category = null)
        {
            IEnumerable<Manufacturer> manufacturers = context.Manufactures.ToList();
            ViewBag.Control = control;
            ViewBag.Header = "Manufacturers";
            return PartialView(manufacturers.Select(c => c.Name));
        }
    }
}
