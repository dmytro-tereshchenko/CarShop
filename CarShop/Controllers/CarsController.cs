using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarShop.Models;
using CarShop.Domain;

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarShopContext context;

        public CarsController(CarShopContext context)
        {
            this.context = context;
        }

        // GET: Cars
        //public async Task<IActionResult> Index()
        public async Task<IActionResult> Index(string manufacturer, int page = 1)
        {
            int pageSize = 2;
            IEnumerable<Car> carShopContext = await context.Cars.Include(c => c.Manufacturer).ToListAsync();
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = manufacturer == null ? carShopContext.Count() :
                carShopContext.Where(g => g.Manufacturer.Name == manufacturer).Count()

            };
            IEnumerable<Car> carsResult = manufacturer == null ? carShopContext.
                OrderBy(g => g.Id).
                Skip((page - 1) * pageSize).Take(pageSize)
                : carShopContext.
                Where(g => g.Manufacturer.Name == manufacturer).
                OrderBy(g => g.Id).
                Skip((page - 1) * pageSize).Take(pageSize);
            IndexCarViewModel viewModel = new IndexCarViewModel
            {
                Cars = carsResult,
                PageInfo = pageInfo,
                SelectedManufacturer = manufacturer
            };
            return View(viewModel);
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await context.Cars
                .Include(c => c.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            ViewData["ManufacturerId"] = new SelectList(context.Manufactures, "Id", "Name");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Model,Year,Description,Price,ManufacturerId")] Car car)
        {
            if (ModelState.IsValid)
            {
                context.Add(car);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(context.Manufactures, "Id", "Name", car.ManufacturerId);
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(context.Manufactures, "Id", "Name", car.ManufacturerId);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model,Year,Description,Price,ManufacturerId")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(car);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(context.Manufactures, "Id", "Name", car.ManufacturerId);
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await context.Cars
                .Include(c => c.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await context.Cars.FindAsync(id);
            context.Cars.Remove(car);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return context.Cars.Any(e => e.Id == id);
        }
    }
}
