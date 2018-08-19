using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieMonster.Data;
using MovieMonster.Features;
using MovieMonster.Models;
using Newtonsoft.Json;

namespace MovieMonster.Controllers
{
    
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movie.ToListAsync());
        }

        [HttpPost]
        public async Task<PartialViewResult> ShortIndex()
        {
            var movies = await _context.Movie.ToListAsync();
            return PartialView("Index", movies);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.MovieID == id);
            if (movie == null)
            {
                return NotFound();
            }
            ImdbEntity imdbMovie = OmdbFetcher(movie.Title);
            if (imdbMovie == null || imdbMovie.Poster == "N/A")
            {
                ViewData["Poster"] = "/assets/img/MovieMonsterLogo.jpg";
            }
            else
            {
                ViewData["Poster"] = imdbMovie.Poster;
            }
            ViewData["Title"] = new SelectList(_context.Movie, "Title", "Title");
            return View(movie);
        }
        [Authorize(Roles = "Admin")]
        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieID,Title,Genere,UnitsInStock,YearRelease,Actors,Rated,Language,Wholesale,Retail")] Movie movie)
        {
            // need change YearReleased and Reated
            if (ModelState.IsValid)
            {
                ImdbEntity imdbMovie = OmdbFetcher(movie.Title);
                if (imdbMovie != null)
                {
                    movie.Genere = imdbMovie.Genre;
                    if (!imdbMovie.Year.Equals("N/A"))
                    {
                        movie.YearRelease = int.Parse(imdbMovie.Year);
                    }
                    movie.Actors = imdbMovie.Actors;
                    movie.Rated = imdbMovie.Rated;
                    movie.Language = imdbMovie.Language;
                }
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }
        [Authorize(Roles = "Admin")]
        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }
        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MovieID,Title,Genere,UnitsInStock,YearRelease,Actors,Rated,Language,Wholesale,Retail")] Movie movie)
        {
            if (id != movie.MovieID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieID))
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
            return View(movie);
        }
        // GET: Movies/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.MovieID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(string id)
        {
            return _context.Movie.Any(e => e.MovieID == id);
        }

        /*-----------------My Function----------------*/
        //fetch data from IMDB
        // disconnect from gave the jeson and convert him to object
        public ImdbEntity OmdbFetcher(string title)
        {
            ImdbEntity obj = new ImdbEntity();
            var json = DownloadJesonMovie(title);
            obj = JsonConvert.DeserializeObject<ImdbEntity>(DownloadJesonMovie(title));
            if (obj.Response.Equals("True"))
                return obj;
            else return null;

        }
        public string DownloadJesonMovie(string title)
        {
            using (var webClient = new WebClient())
            {
                return webClient.DownloadString("http://www.omdbapi.com/?t=" + title + "&plot=full&apikey=94919479");
            }
        }
        /* 
          search function by title with Regex from begining and end connect to search box in the view
          (HTML-layout, just if you on MoviesController page)
        */

        [HttpPost]
        public async Task<string> Search(string searchTxt)
        {
            var movies = await _context.Movie.Where(movie => movie.Title.Contains(searchTxt)).ToListAsync();
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(movies);
            return jsonString;
        }

        [HttpPost]
        public async Task<string> AdvancedSearch([Bind("MovieID,Title,Genere,UnitsInStock,YearRelease,Actors,Rated,Language,Wholesale,Retail")] Movie searchMovie)
        {
            var result = _context.Movie.AsQueryable();
            if (searchMovie != null)
            {
                if (searchMovie.MovieID != null)
                    result = result.Where(movie => movie.MovieID == searchMovie.MovieID);
                if (searchMovie.Title != null)
                    result = result.Where(movie => movie.Title.Contains(searchMovie.Title));
                if (searchMovie.Genere != null)
                    result = result.Where(movie => movie.Genere == searchMovie.Genere);
                if (searchMovie.UnitsInStock != 0)
                    result = result.Where(movie => movie.UnitsInStock == searchMovie.UnitsInStock);
                if (searchMovie.Actors != null)
                {
                    string[] actorsSplit = searchMovie.Actors.Split(',');
                    foreach (string actor in actorsSplit)
                    {
                        result = result.Where(movie => movie.Actors.Contains(actor));
                    }
                }
                if (searchMovie.YearRelease != 0)
                    result = result.Where(movie => movie.YearRelease == searchMovie.YearRelease);
                if (searchMovie.Rated != null)
                    result = result.Where(movie => movie.Rated == searchMovie.Rated);
                if (searchMovie.Language != null)
                    result = result.Where(movie => movie.Language == searchMovie.Language);
            }
            var list = await result.ToListAsync();
            var listJason = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            return listJason;
        }
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddMovieToCart(string CustomerID, string MovieID)
        {
            var customer = await _context.Customer.Include(u => u.Sales).FirstOrDefaultAsync(u => u.CustomerID == CustomerID);
            var cart = new Sale();
            foreach (var sale in customer.Sales)
            {
                if (sale.Purchased == false)
                    cart = sale;
            }
            if (cart.SaleID != null)
            {

                return RedirectToAction("Create", "MovieSales", new
                {
                    SaleID=cart.SaleID, MovieID=MovieID
                });
            }
            var sales = await _context.Sale.ToListAsync();
            var SaleID = sales.Count + 1;
            return RedirectToAction("Create", "Sales", new
            {
                SaleID=SaleID,
                MovieID = MovieID,
                CustomerID = customer.CustomerID,
                Purchased=false

            });
        }
    }
}
