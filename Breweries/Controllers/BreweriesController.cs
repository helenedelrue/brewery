﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Breweries.Models.DB;

namespace Breweries.Controllers
{
    public class BreweriesController : Controller
    {
        private readonly BreweryDatabaseContext _context;

        public BreweriesController(BreweryDatabaseContext context)
        {
            _context = context;
        }

        // GET: Breweries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Brewery.ToListAsync());
        }

        // GET: Breweries/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brewery = await _context.Brewery
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brewery == null)
            {
                return NotFound();
            }

            return View(brewery);
        }

        // GET: Breweries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Breweries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Email,Website,Telephone")] Brewery brewery)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brewery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brewery);
        }

        // GET: Breweries/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brewery = await _context.Brewery.FindAsync(id);
            if (brewery == null)
            {
                return NotFound();
            }
            return View(brewery);
        }

        // POST: Breweries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Email,Website,Telephone")] Brewery brewery)
        {
            if (id != brewery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brewery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BreweryExists(brewery.Id))
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
            return View(brewery);
        }

        // GET: Breweries/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brewery = await _context.Brewery
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brewery == null)
            {
                return NotFound();
            }

            return View(brewery);
        }

        // POST: Breweries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var brewery = await _context.Brewery.FindAsync(id);
            _context.Brewery.Remove(brewery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BreweryExists(Guid id)
        {
            return _context.Brewery.Any(e => e.Id == id);
        }
    }
}
