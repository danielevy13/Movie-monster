using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieMonste.Models;

namespace MovieMonste.Controllers
{
    public class MovieSalesController : Controller
    {
        private readonly MovieMonsteContext _context;

        public MovieSalesController(MovieMonsteContext context)
        {
            _context = context;
        }

        // GET: MovieSales
        public async Task<IActionResult> Index()
        {
            var movieMonsteContext = _context.MovieSale.Include(m => m.Movie).Include(m => m.Sale);
            return View(await movieMonsteContext.ToListAsync());
        }

        // GET: MovieSales/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieSale = await _context.MovieSale
                .Include(m => m.Movie)
                .Include(m => m.Sale)
                .FirstOrDefaultAsync(m => m.MovieID == id);
            if (movieSale == null)
            {
                return NotFound();
            }

            return View(movieSale);
        }

        // GET: MovieSales/Create
        public IActionResult Create()
        {
            ViewData["MovieID"] = new SelectList(_context.Movie, "MovieID", "MovieID");
            ViewData["SaleID"] = new SelectList(_context.Sale, "SaleID", "SaleID");
            return View();
        }

        // POST: MovieSales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaleID,MovieID")] MovieSale movieSale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieSale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movieSale);
        }

        // GET: MovieSales/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieSale = await _context.MovieSale.FindAsync(id);
            if (movieSale == null)
            {
                return NotFound();
            }
            return View(movieSale);
        }

        // POST: MovieSales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SaleID,MovieID")] MovieSale movieSale)
        {
            if (id != movieSale.MovieID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieSale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieSaleExists(movieSale.MovieID))
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
            return View(movieSale);
        }

        // GET: MovieSales/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieSale = await _context.MovieSale
                .Include(m => m.Movie)
                .Include(m => m.Sale)
                .FirstOrDefaultAsync(m => m.MovieID == id);
            if (movieSale == null)
            {
                return NotFound();
            }

            return View(movieSale);
        }

        // POST: MovieSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var movieSale = await _context.MovieSale.FindAsync(id);
            _context.MovieSale.Remove(movieSale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieSaleExists(string id)
        {
            return _context.MovieSale.Any(e => e.MovieID == id);
        }
    }
}
