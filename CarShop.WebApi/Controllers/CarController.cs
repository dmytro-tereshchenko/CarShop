using CarShop.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly CarShopContext context;
        public CarController(CarShopContext context)
        {
            this.context = context;
        }
        //api/car
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> Get()
        {
            return await context.Cars.Include(context => context.Manufacturer).ToListAsync();
        }

        //api/car/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> Get(int id)
        {
            try
            {
                Car car = await context.Cars.Include(context => context.Manufacturer).FirstAsync(c => c.Id == id);
                if (car == null)
                    return NotFound();
                return Ok(car);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }

        //api/car
        [HttpPost]
        public async Task<ActionResult<Car>> Post([FromBody] Car car)
        {
            if (car == null)
            {
                return BadRequest();
            }
            if (car.Manufacturer != null)
            {
                car.ManufacturerId = car.Manufacturer.Id;
                car.Manufacturer = null;
            }
            context.Cars.Add(car);
            await context.SaveChangesAsync();
            return Ok(car);
        }

        //api/car
        [HttpPut]
        public async Task<ActionResult<Car>> Put([FromBody] Car car)
        {
            if (car == null)
            {
                return BadRequest();
            }
            if (!context.Cars.Any(c => c.Id == car.Id))
                return NotFound();
            if (car.Manufacturer != null)
            {
                car.ManufacturerId = car.Manufacturer.Id;
                car.Manufacturer = null;
            }
            context.Cars.Update(car);
            try
            {
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
            return Ok(car);
        }
        //api/car/2
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Car car = await context.Cars.FirstAsync(t => t.Id == id);
                if (car == null)
                    return NotFound();
                context.Cars.Remove(car);
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }
        private bool CarExists(int id)
        {
            return context.Cars.Any(c => c.Id == id);
        }
    }
}
