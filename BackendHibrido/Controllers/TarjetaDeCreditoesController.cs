using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendHibrido;
using BackendHibrido.Models;

namespace BackendHibrido.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaDeCreditoesController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public TarjetaDeCreditoesController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TarjetaDeCreditoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TarjetaDeCredito>>> GetTarjetaCreditos()
        {
            return await _context.TarjetaCreditos.ToListAsync();
        }

        // GET: api/TarjetaDeCreditoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TarjetaDeCredito>> GetTarjetaDeCredito(int id)
        {
            var tarjetaDeCredito = await _context.TarjetaCreditos.FindAsync(id);

            if (tarjetaDeCredito == null)
            {
                return NotFound();
            }

            return tarjetaDeCredito;
        }

        // PUT: api/TarjetaDeCreditoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarjetaDeCredito(int id, TarjetaDeCredito tarjetaDeCredito)
        {
            if (id != tarjetaDeCredito.Id)
            {
                return BadRequest();
            }

            _context.Entry(tarjetaDeCredito).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarjetaDeCreditoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TarjetaDeCreditoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TarjetaDeCredito>> PostTarjetaDeCredito(TarjetaDeCredito tarjetaDeCredito)
        {
            _context.TarjetaCreditos.Add(tarjetaDeCredito);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTarjetaDeCredito", new { id = tarjetaDeCredito.Id }, tarjetaDeCredito);
        }

        // DELETE: api/TarjetaDeCreditoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TarjetaDeCredito>> DeleteTarjetaDeCredito(int id)
        {
            var tarjetaDeCredito = await _context.TarjetaCreditos.FindAsync(id);
            if (tarjetaDeCredito == null)
            {
                return NotFound();
            }

            _context.TarjetaCreditos.Remove(tarjetaDeCredito);
            await _context.SaveChangesAsync();

            return tarjetaDeCredito;
        }

        private bool TarjetaDeCreditoExists(int id)
        {
            return _context.TarjetaCreditos.Any(e => e.Id == id);
        }
    }
}
