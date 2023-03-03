using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entities;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProvinceController : ControllerBase
    {
        private readonly DataContext _context;

        public ProvinceController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.Provinces
                .Include(p => p.Cities)
                .ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var country = await _context.Provinces
                .Include(p => p.Cities)
                .FirstOrDefaultAsync(c => c.ProvinceId == id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Province province)
        {
            try
            {
                _context.Add(province);
                await _context.SaveChangesAsync();
                return Ok(province);

            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una provincia con el mismo nombre.");
                }
                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Province province)
        {
            try
            {
                _context.Update(province);
                await _context.SaveChangesAsync();
                return Ok(province);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una provincia con el mismo nombre.");
                }
                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var province = await _context.Provinces.FirstOrDefaultAsync(c => c.ProvinceId == id);
            if (province == null)
            {
                return NotFound();
            }
            _context.Remove(province);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
