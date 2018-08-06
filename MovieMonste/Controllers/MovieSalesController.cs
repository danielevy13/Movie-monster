/*
  MovieSales work with 2 primary key and need get 2 keys for questions(del,edit...)
 */
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

        // all function thats need the key getting 2 primary Keys
        // GET: MovieSales/Details/5
        public async Task<IActionResult> Details(string SaleID , string MovieID)
        {
            if (SaleID == null || MovieID==null)
            {
                return NotFound();
            }

            var movieSale = await _context.MovieSale
                .Include(m => m.Movie)
                .Include(m => m.Sale)
                .FirstOrDefaultAsync(m => m.MovieID == MovieID && m.SaleID==SaleID);
            if (movieSale == null)
            {
                return NotFound();
            }

            return View(movieSale);
        }

        // GET: MovieSales/Create
        public IActionResult Create()
        {
            ViewData["SaleID"] = new SelectList(_context.Sale, "SaleID", "SaleID");
            ViewData["MovieID"] = new SelectList(_context.Movie, "MovieID", "MovieID");
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
        public async Task<IActionResult> Edit(string SaleID,string MovieID)
        {
            if (SaleID == null|| MovieID == null)
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
            if (MovieID != movieSale.MovieID|| SaleID != movieSale.SaleID)
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
            if (SaleID == null || MovieID==null)
            {
                return NotFound();
            }

            var movieSale = await _context.MovieSale
                .Include(m => m.Movie)
                .Include(m => m.Sale)
                .FirstOrDefaultAsync(m => m.MovieID == MovieID&&m.SaleID==SaleID);
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

        private bool MovieSaleExists(string SaleID,string MovieID)
        {
            return _context.MovieSale.Any(e => e.MovieID == MovieID && e.SaleID==SaleID);
        }
    }
}
