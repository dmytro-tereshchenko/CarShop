using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CarShop.Domain;

namespace CarShop.Controllers
{
    public class NavController : Controller
    {
        private readonly CarShopContext context;
        public NavController(CarShopContext context) => this.context = context;
        public PartialViewResult Menu(string manufacturer = null)
        {
            IEnumerable<Manufacturer> manufacturers = context.Manufactures.ToList();
            ViewBag.Header = "Manufacturers";
            ViewBag.SelectedManufacturer = manufacturer;
            return PartialView(manufacturers.Select(c => c.Name));
        }
    }
}
