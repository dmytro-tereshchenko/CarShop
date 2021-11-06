using CarShop.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly CarShopContext context;
        public ManufacturerController(CarShopContext context)
        {
            this.context = context;
        }
        // GET: api/<ManufacturerController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manufacturer>>> Get()
        {
            return await context.Manufactures.ToListAsync();
        }

        // GET api/<ManufacturerController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Manufacturer>> Get(int id)
        {
            try
            {
                Manufacturer manufacturer = await context.Manufactures.FirstAsync(m => m.Id == id);
                if (manufacturer == null)
                    return NotFound();
                return Ok(manufacturer);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }

        // POST api/<ManufacturerController>
        [HttpPost]
        public async Task<ActionResult<Manufacturer>> Post([FromBody] Manufacturer manufacturer)
        {
            if (manufacturer == null)
            {
                return BadRequest();
            }
            context.Manufactures.Add(manufacturer);
            await context.SaveChangesAsync();
            return Ok(manufacturer);
        }

        // PUT api/<ManufacturerController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Manufacturer>> Put([FromBody] Manufacturer manufacturer)
        {
            if (manufacturer == null)
            {
                return BadRequest();
            }
            if (!context.Manufactures.Any(m => m.Id == manufacturer.Id))
                return NotFound();
            context.Manufactures.Update(manufacturer);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManufacturerExists(manufacturer.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(manufacturer);
        }

        // DELETE api/<ManufacturerController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Manufacturer manufacturer = await context.Manufactures.FirstAsync(m => m.Id == id);
                if (manufacturer == null)
                    return NotFound();
                context.Manufactures.Remove(manufacturer);
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }
        private bool ManufacturerExists(int id)
        {
            return context.Manufactures.Any(c => c.Id == id);
        }
    }
}
