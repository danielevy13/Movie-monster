using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieMonster.Data;
using MovieMonster.Models;

namespace MovieMonster.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;


        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            // include Movies from MovieSale(Movies)
            var movieMonsteContext = _context.Sale.Include(s => s.Customer).Include(ms => ms.Movies).Include("Movies.Movie");
            return View(await movieMonsteContext.ToListAsync());
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sale.Include(s => s.Customer).Include("Movies.Movie")
                .FirstOrDefaultAsync(m => m.SaleID == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        //// GET: Sales/Create
        //public IActionResult Create()
        //{
        //    ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "CustomerID");
        //    return View();
        //}

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string MovieID, [Bind("SaleID,CustomerID,Purchased,TotalPrice")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sale);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "MovieSale", new
                {
                    SaleID = sale.SaleID,
                    MovieID = MovieID
                });
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "CustomerID");
            return View(sale);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sale.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "CustomerID");
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SaleID,CustomerID,Purchased,TotalPrice")] Sale sale)
        {
            if (id != sale.SaleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleExists(sale.SaleID))
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
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "CustomerID");
            return View(sale);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sale.Include(s => s.Customer)
                .FirstOrDefaultAsync(m => m.SaleID == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sale = await _context.Sale.FindAsync(id);
            _context.Sale.Remove(sale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleExists(string id)
        {
            return _context.Sale.Any(e => e.SaleID == id);
        }


        /******************MyFunctions*******************/

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<string> AdvancedSearch([Bind("SaleID,CustomerID,Purchased,TotalPrice")] Sale searchSale)
        {
            var result = _context.Sale.AsQueryable();
            if (searchSale != null)
            {
                if (searchSale.SaleID != null)
                    result = result.Where(sale => sale.SaleID == searchSale.SaleID);
                if (searchSale.CustomerID != null)
                    result = result.Where(sale => sale.CustomerID == searchSale.CustomerID);
              //  if (searchSale.Purchased != null)
              //      result = result.Where(sale => sale.Purchased == searchSale.Purchased);
                if (searchSale.TotalPrice != 0)
                    result = result.Where(sale => sale.TotalPrice == searchSale.TotalPrice);
            }
            var list = await result.ToListAsync();
            var listJason = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            return listJason;
        }

        public async Task<IActionResult> MatchCartToCustomer(string CustomerID)
        {
            var customer =await _context.Customer.Include(u => u.Sales).Include("Sales.Movies.Movie").FirstOrDefaultAsync(u => u.CustomerID == CustomerID);
            var cart= new List<Sale>();
            foreach (var sale in customer.Sales)
            {
                if (sale.Purchased == false)
                {
                    cart.Add(await _context.Sale.Include(ms => ms.Movies).Include("Movies.Movie").FirstOrDefaultAsync(s => s.SaleID == sale.SaleID));
                }
            }
            return View("Index", cart);
        }

        public async Task<IActionResult> Purchase(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sale.FirstOrDefaultAsync(m => m.SaleID == id);
            if (sale == null)
            {
                return NotFound();
            }
            sale.Purchased = true;
            try
            {
                _context.Update(sale);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(sale.SaleID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "Home");
        }
        //Direction to the graph view =  /Sales/PieChart
        public IActionResult PieChart()
        {
            return View("~/Views/Graphs/PieChart.cshtml");
        }
        // Join And groupBy query result, sent as array to the view by Ajax
        public JsonResult QuantityJson()
        {
            var titleQuantityTable = (from m in _context.Movie
                                      join ms in _context.MovieSale on m.MovieID equals ms.MovieID
                                      join s in _context.Sale on ms.SaleID equals s.SaleID
                                      where ms.Quantity > 0 && s.Purchased == true
                                      select new { m.Title, ms.Quantity } into newTable
                                      group newTable by new { newTable.Title } into finalTable
                                      select new
                                      {
                                          Title = finalTable.Key.Title,
                                          Quantity = finalTable.Sum(q => q.Quantity)
                                      }
                                      );

            var resultTableAsArray = titleQuantityTable.ToList();

            //var json = JsonConvert.SerializeObject(resultTableAsArray);

            return Json(resultTableAsArray);
            
        }
    }
}
