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
using Accord.MachineLearning.Rules;

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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            // include Movies from MovieSale(Movies)
            var movieMonsteContext = _context.Sale.Include(s => s.Customer).Include(ms => ms.Movies).Include("Movies.Movie");
            return View(await movieMonsteContext.ToListAsync());
        }
        [Authorize(Roles = "User")]
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
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create(string MovieID, [Bind("SaleID,CustomerID,Purchased,TotalPrice")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sale);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "MovieSales", new
                {
                    SaleID = sale.SaleID,
                    MovieID = MovieID
                });
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "CustomerID");
            return View(sale);
        }

        // GET: Sales/Edit/5
        [Authorize(Roles = "User")]
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
        [Authorize(Roles = "User")]
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
        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sale = await _context.Sale.FindAsync(id);
            if (sale.Purchased == false)
            {
                _context.Sale.Remove(sale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "Home");
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
        [Authorize(Roles = "User")]
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

        //Direction to the graph view =  /Sales/Statistics
        public ActionResult Statistics()
        {
            return View();
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
            var resultTableAsList = titleQuantityTable.OrderByDescending(x => x.Quantity).ToList();
            return Json(resultTableAsList);

        }

        public void Predict(string SaleID, string CustomerID)
        {
            Dictionary<string, int> mapActors = new Dictionary<string, int>();
            SortedSet<int>[] dataset = SalesTransactionConverter(mapActors);
            Apriori apriori = new Apriori(threshold: 3, confidence: 0);
            AssociationRuleMatcher<int> classifier = apriori.Learn(dataset);
            var sale = _context.Sale.Include(s => s.Movies).Include("Movies.Movie").FirstOrDefault(s => s.SaleID == SaleID);

            HashSet<string> actorsSet = new HashSet<string>();
            List<int> sample = new List<int>();
            foreach (var movie in sale.Movies)
            {
                string[] actors = movie.Movie.Actors.Split(",");
                foreach (var actor in actors)
                {
                    if (!actorsSet.Contains(actor))
                    {
                        actorsSet.Add(actor);
                        sample.Add(mapActors.GetValueOrDefault(actor));
                    }
                }
            }
            int[][] matches = classifier.Decide(sample.ToArray());
        }

        public SortedSet<int>[] SalesTransactionConverter(Dictionary<string, int> mapActors)
        {
            int counter = 0;
            var sales = _context.Sale.Include(s => s.Movies).Include("Movies.Movie").ToList();

            List<SortedSet<int>> datasetList = new List<SortedSet<int>>();
            foreach (var sale in sales)
            {
                HashSet<string> actorsSet = new HashSet<string>();
                SortedSet<int> actors = new SortedSet<int>();
                foreach (var movie in sale.Movies)
                {
                    string[] temp = movie.Movie.Actors.Split(",");
                    foreach (var actor in temp)
                    {
                        if (!actorsSet.Contains(actor))
                            if (mapActors.ContainsKey(actor))
                            {
                                actors.Add(mapActors.GetValueOrDefault(actor));
                            }
                            else
                            {
                                counter++;
                                mapActors.Add(actor, counter);
                                actors.Add(counter);
                            }
                    }
                }
                datasetList.Add(actors);
            }
            SortedSet<int>[] dataset = datasetList.ToArray();
            return dataset;
        }

    }
}