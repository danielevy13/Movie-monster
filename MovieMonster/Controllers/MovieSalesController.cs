using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieMonster.Data;
using MovieMonster.Models;

namespace MovieMonster.Controllers
{
    public class MovieSalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieSalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MovieSales
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MovieSale.Include(m => m.Movie).Include(m => m.Sale);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MovieSales/Details/5
        public async Task<IActionResult> Details(string SaleID, string MovieID)
        {
            if (SaleID == null || MovieID == null)
            {
                return NotFound();
            }

            var movieSale = await _context.MovieSale
                .Include(m => m.Movie)
                .Include(m => m.Sale)
                .FirstOrDefaultAsync(m => m.MovieID == MovieID && m.SaleID == SaleID);
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
            ViewData["SaleID"] = new SelectList(_context.Set<Sale>(), "SaleID", "SaleID");
            return View();
        }

        // POST: MovieSales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string SaleID, string MovieID, [Bind("SaleID,MovieID,Quantity")] MovieSale movieSale)
        {
            if (SaleID == null || MovieID == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (!MovieSaleExists(SaleID, MovieID))
                {
                    _context.Add(movieSale);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    try
                    {
                        _context.Update(movieSale);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!MovieSaleExists(movieSale.SaleID, movieSale.MovieID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movieSale);
        }


        // GET: MovieSales/Edit/5
        public async Task<IActionResult> Edit(string SaleID, string MovieID)
        {
            if (SaleID == null || MovieID == null)
            {
                return NotFound();
            }

            var movieSale = await _context.MovieSale.FindAsync(MovieID, SaleID);
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
        public async Task<IActionResult> Edit(string SaleID, string MovieID, [Bind("SaleID,MovieID,Quantity")] MovieSale movieSale)
        {
            if (MovieID != movieSale.MovieID || SaleID != movieSale.SaleID)
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
                    if (!MovieSaleExists(movieSale.SaleID, movieSale.MovieID))
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
        public async Task<IActionResult> Delete(string SaleID, string MovieID)
        {
            if (SaleID == null || MovieID == null)
            {
                return NotFound();
            }

            var movieSale = await _context.MovieSale
                .Include(m => m.Movie)
                .Include(m => m.Sale)
                .FirstOrDefaultAsync(m => m.MovieID == MovieID && m.SaleID == SaleID);
            if (movieSale == null)
            {
                return NotFound();
            }

            return View(movieSale);
        }

        // POST: MovieSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string SaleID, string MovieID)
        {
            var movieSale = await _context.MovieSale.FindAsync(MovieID, SaleID);
            _context.MovieSale.Remove(movieSale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieSaleExists(string SaleID, string MovieID)
        {
            return _context.MovieSale.Any(e => e.MovieID == MovieID && e.SaleID == SaleID);
        }
    }
}
