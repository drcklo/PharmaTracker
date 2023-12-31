﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaTracker.Server.DAL;
using PharmaTracker.Shared;
namespace PharmaTracker.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminsController : ControllerBase
	{
		private readonly PharmaTracketContext _context;
		public AdminsController(PharmaTracketContext context)
		{
			_context = context;
		}
		// GET: api/Admins
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Admin>>> GetAdmin()
		{
			if (_context.Admin == null)
			{
				return NotFound();
			}
			return await _context.Admin.ToListAsync();
		}
		// GET: api/Admins/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Admin>> GetAdmin(int id)
		{
			if (_context.Admin == null)
			{
				return NotFound();
			}
			var admin = await _context.Admin.Include(e => e.AdminDetalle)
				.Where(e => e.AdminId == id)
				.FirstOrDefaultAsync();


			if (admin == null)
			{
				return NotFound();
			}
			return admin;
		}


		// PUT: api/Admins/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutAdmin(int id, Admin admin)
		{
			if (id != admin.AdminId)
			{
				return BadRequest();
			}
			_context.Entry(admin).State = EntityState.Modified;
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!AdminExists(id))
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
		// POST: api/Admins
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
		{
			if (_context.Admin == null)
			{
				return Problem("Entity set 'PharmaTracketContext.Admin'  is null.");
			}

			if (admin.AdminId <= 0 || !AdminExists(admin.AdminId))
			{
				_context.Admin.Add(admin);
			}
			else
			{
				_context.Admin.Update(admin);
			}

			await _context.SaveChangesAsync();

			return Ok(admin);
		}

		// DELETE: api/Admins/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAdmin(int id)
		{
			if (_context.Admin == null)
			{
				return NotFound();
			}
			var admin = await _context.Admin.FindAsync(id);
			if (admin == null)
			{
				return NotFound();
			}
			_context.Admin.Remove(admin);
			await _context.SaveChangesAsync();
			return NoContent();
		}
		private bool AdminExists(int id)
		{
			return (_context.Admin?.Any(e => e.AdminId == id)).GetValueOrDefault();
		}

		[HttpDelete("DeleteAdminDetalle/{id}")]
		public async Task<IActionResult> DeleteDetalle(int id)
		{
			if(id <= 0)
			{
				return BadRequest();
			}
			var Detalle = await _context.Admin.FirstOrDefaultAsync(ad => ad.AdminId == id);
			if(Detalle is null)
			{
				return NotFound();
			}
			_context.Admin.Remove(Detalle);
			await _context.SaveChangesAsync();

			return Ok();
		}
	}
}

/*
 * 		//Delete AdminDetalle
		[HttpDelete("DeleteAdminDetalle/{id}")]
		public async Task<IActionResult> DeleteAdminDetalle(int id)
		{
			if (id <= 0)
			{
				return BadRequest();
			}
			var Detalle = await _context.
				
				AdminDetalle.FirstOrDefaultAsync(ad => ad.AdminDetalleId == id);
			if (Detalle is null)
			{
				return NotFound();
			}
			_context.AdminDetalle.Remove(Detalle);
			await _context.SaveChangesAsync();

			return Ok();
		}
 * 
 * 
 * 
         [HttpDelete("DeleteAguaDetalle/{id}")]
        public async Task<IActionResult> DeleteAguaDetalle(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var Detalle = await _context.ControlCalidadAguaDetalle.FirstOrDefaultAsync(ad => ad.ControlCalidadAguaDetalleId == id);
            if (Detalle is null)
            {
                return NotFound();
            }
            _context.ControlCalidadAguaDetalle.Remove(Detalle);
            await _context.SaveChangesAsync();

            return Ok();
        }
 */