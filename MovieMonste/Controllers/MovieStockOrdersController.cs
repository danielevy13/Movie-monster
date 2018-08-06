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
    public class MovieStockOrdersController : Controller
    {
        private readonly MovieMonsteContext _context;

        public MovieStockOrdersController(MovieMonsteContext context)
        {
            _context = context;
        }

        // GET: MovieStockOrders
        public async Task<IActionResult> Index()
        {
            var movieMonsteContext = _context.MovieStockOrder.Include(m => m.Movie).Include(m => m.StockOrder);
            return View(await movieMonsteContext.ToListAsync());
        }

        // GET: MovieStockOrders/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieStockOrder = await _context.MovieStockOrder
                .Include(m => m.Movie)
                .Include(m => m.StockOrder)
                .FirstOrDefaultAsync(m => m.MovieID == id);
            if (movieStockOrder == null)
            {
                return NotFound();
            }

            return View(movieStockOrder);
        }

        // GET: MovieStockOrders/Create
        public IActionResult Create()
        {
            ViewData["MovieID"] = new SelectList(_context.Movie, "MovieID", "MovieID");
            ViewData["StockOrderID"] = new SelectList(_context.StockOrder, "StockOrderID", "StockOrderID");
            return View();
        }

        // POST: MovieStockOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StockOrderID,MovieID")] MovieStockOrder movieStockOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieStockOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieID"] = new SelectList(_context.Movie, "MovieID", "MovieID", movieStockOrder.MovieID);
            ViewData["StockOrderID"] = new SelectList(_context.StockOrder, "StockOrderID", "StockOrderID", movieStockOrder.StockOrderID);
            return View(movieStockOrder);
        }

        // GET: MovieStockOrders/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieStockOrder = await _context.MovieStockOrder.FindAsync(id);
            if (movieStockOrder == null)
            {
                return NotFound();
            }
            ViewData["MovieID"] = new SelectList(_context.Movie, "MovieID", "MovieID", movieStockOrder.MovieID);
            ViewData["StockOrderID"] = new SelectList(_context.StockOrder, "StockOrderID", "StockOrderID", movieStockOrder.StockOrderID);
            return View(movieStockOrder);
        }

        // POST: MovieStockOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StockOrderID,MovieID")] MovieStockOrder movieStockOrder)
        {
            if (id != movieStockOrder.MovieID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieStockOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieStockOrderExists(movieStockOrder.MovieID))
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
            ViewData["MovieID"] = new SelectList(_context.Movie, "MovieID", "MovieID", movieStockOrder.MovieID);
            ViewData["StockOrderID"] = new SelectList(_context.StockOrder, "StockOrderID", "StockOrderID", movieStockOrder.StockOrderID);
            return View(movieStockOrder);
        }

        // GET: MovieStockOrders/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieStockOrder = await _context.MovieStockOrder
                .Include(m => m.Movie)
                .Include(m => m.StockOrder)
                .FirstOrDefaultAsync(m => m.MovieID == id);
            if (movieStockOrder == null)
            {
                return NotFound();
            }

            return View(movieStockOrder);
        }

        // POST: MovieStockOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var movieStockOrder = await _context.MovieStockOrder.FindAsync(id);
            _context.MovieStockOrder.Remove(movieStockOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieStockOrderExists(string id)
        {
            return _context.MovieStockOrder.Any(e => e.MovieID == id);
        }
    }
}
