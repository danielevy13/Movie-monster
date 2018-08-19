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
using Newtonsoft.Json;

namespace MovieMonster.Controllers
{
    
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customers
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            // Include all virtuals (one or ICollections /// one or many) 
            var customers = _context.Customer.Include(u=>u.User).Include(u => u.Sales);
            return View(await customers.ToListAsync());
        }

        // GET: Customers/Details/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(string id
            )
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }
        /*
                // GET: Customers/Create
                public IActionResult Create(string UserID)
                {

                    return View();
                }
                */
        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //  [HttpPost]
        //  [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create([Bind("CustomerID,UserID,FirstName,LastName,BirthDate,PhoneNumber,Gender,State,City,StreetName,ApartmentNumber,ZipCode")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return View("Details",customer);
            }
            return View(customer);
        }
        // GET: Customers/Edit/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(string id, [Bind("CustomerID,UserID,FirstName,LastName,BirthDate,PhoneNumber,Gender,State,City,StreetName,ApartmentNumber,ZipCode")] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerID))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(string id)
        {
            return _context.Customer.Any(e => e.CustomerID == id);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<string> Search(string searchTxt)
        {
            var customers = await _context.Customer.Where(customer => customer.FirstName.Contains(searchTxt)).ToListAsync();
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(customers);
            return jsonString;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<string> AdvancedSearch(int Year, [Bind("CustomerID,UserID,FirstName,LastName,BirthDate,PhoneNumber,Gender,State,City,StreetName,ApartmentNumber,ZipCode")] Customer searchCustomer)
        {
            var result = _context.Customer.AsQueryable();
            if (searchCustomer != null)
            {
                if (searchCustomer.CustomerID != null)
                    result = result.Where(customer => customer.CustomerID == searchCustomer.CustomerID);
                if (searchCustomer.FirstName != null)
                    result = result.Where(customer => customer.FirstName == searchCustomer.FirstName);
                if (searchCustomer.LastName != null)
                    result = result.Where(customer => customer.LastName == searchCustomer.LastName);
                if (Year != 0)
                    result = result.Where(customer => customer.BirthDate.Year == Year);
                if (searchCustomer.PhoneNumber != null)
                    result = result.Where(customer => customer.PhoneNumber == searchCustomer.PhoneNumber);
                if (searchCustomer.Gender != null)
                    result = result.Where(customer => customer.Gender == searchCustomer.Gender);
                if (searchCustomer.State != null)
                    result = result.Where(customer => customer.State == searchCustomer.State);
                if (searchCustomer.City != null)
                    result = result.Where(customer => customer.City == searchCustomer.City);
                if (searchCustomer.StreetName != null)
                    result = result.Where(customer => customer.StreetName == searchCustomer.StreetName);
                if (searchCustomer.ApartmentNumber != 0)
                    result = result.Where(customer => customer.ApartmentNumber == searchCustomer.ApartmentNumber);
                if (searchCustomer.ZipCode != 0)
                    result = result.Where(customer => customer.ZipCode == searchCustomer.ZipCode);
            }
            var list = await result.ToListAsync();
            var listJason = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            return listJason;
        }

        //Query the DB to get the top customers
        [Authorize(Roles = "Admin")]
        public JsonResult TopCustomers()
        {
            var top5 = (from c in _context.Customer
                        join s in _context.Sale on c.CustomerID equals s.CustomerID
                        where s.Purchased == true
                        select new { c.FirstName, s.Purchased } into newTable
                        group newTable by new { newTable.FirstName } into finalTable
                        select new
                        {
                            Title = finalTable.Key.FirstName,
                            Quantity = finalTable.Count(q => q.Purchased)
                        }
                        );
            var resultTableAsList = top5.ToList();
            return Json(resultTableAsList);
        }
    }
}
