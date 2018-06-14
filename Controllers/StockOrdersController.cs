using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieMansterWebApp.Models;

namespace MovieMansterWebApp.Controllers
{
    public class StockOrdersController : Controller
    {
        private readonly MovieMansterWebAppContext _context;

        public StockOrdersController(MovieMansterWebAppContext context)
        {
            _context = context;
        }

        // GET: StockOrders
        public async Task<IActionResult> Index()
        {
            var movieMansterWebAppContext = _context.StockOrder.Include(s => s.Supplier);
            return View(await movieMansterWebAppContext.ToListAsync());
        }

        // GET: StockOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockOrder = await _context.StockOrder
                .Include(s => s.Supplier)
                .FirstOrDefaultAsync(m => m.StockOrderID == id);
            if (stockOrder == null)
            {
                return NotFound();
            }

            return View(stockOrder);
        }

        // GET: StockOrders/Create
        public IActionResult Create()
        {
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "SupplierID");
            return View();
        }

        // POST: StockOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StockOrderID,SupplierID,TotalPrice")] StockOrder stockOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "SupplierID", stockOrder.SupplierID);
            return View(stockOrder);
        }

        // GET: StockOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockOrder = await _context.StockOrder.FindAsync(id);
            if (stockOrder == null)
            {
                return NotFound();
            }
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "SupplierID", stockOrder.SupplierID);
            return View(stockOrder);
        }

        // POST: StockOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StockOrderID,SupplierID,TotalPrice")] StockOrder stockOrder)
        {
            if (id != stockOrder.StockOrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockOrderExists(stockOrder.StockOrderID))
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
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "SupplierID", stockOrder.SupplierID);
            return View(stockOrder);
        }

        // GET: StockOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockOrder = await _context.StockOrder
                .Include(s => s.Supplier)
                .FirstOrDefaultAsync(m => m.StockOrderID == id);
            if (stockOrder == null)
            {
                return NotFound();
            }

            return View(stockOrder);
        }

        // POST: StockOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockOrder = await _context.StockOrder.FindAsync(id);
            _context.StockOrder.Remove(stockOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockOrderExists(int id)
        {
            return _context.StockOrder.Any(e => e.StockOrderID == id);
        }
    }
}
