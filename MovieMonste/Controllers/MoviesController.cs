using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieMonste.Features;
using MovieMonste.Models;
using Newtonsoft.Json;

namespace MovieMonste.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieMonsteContext _context;

        public MoviesController(MovieMonsteContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movie.ToListAsync());
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
            ViewData["Title"] = new SelectList(_context.Movie, "Title", "Title");
            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieID,Title,Genere,UnitsInStock,ReleaseDate,Actors,MinAge,Language,UnitPrice")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                ImdbEntity imdbMovie = OmdbFetcher(movie.Title);
                if (imdbMovie != null)
                {
                    movie.Genere = imdbMovie.Genre;
                    if (!imdbMovie.Released.Equals("N/A"))
                    {
                        movie.ReleaseDate = DateTime.Parse(imdbMovie.Released, new CultureInfo("de-DE"));
                    }
                    else
                    {
                        movie.ReleaseDate = DateTime.Now;
                    }
                    movie.Actors = imdbMovie.Actors;
                    if (!imdbMovie.Released.Equals("N/A"))
                    {
                        if (imdbMovie.Rated.Equals("R") || imdbMovie.Rated.Equals("PG") || imdbMovie.Rated.Equals("PG-13"))
                        {
                            movie.MinAge = 16;
                        }
                        else
                        {
                            movie.MinAge = 18;
                        }

                    }
                    movie.Language = imdbMovie.Language;
                }
                else
                {
                    movie.ReleaseDate = DateTime.Now;
                }
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MovieID,Title,Genere,UnitsInStock,ReleaseDate,Actors,MinAge,Language")] Movie movie)
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
        //fetch data from IMDB
        // disconnect from gave the jeson and convert him to object
        public ImdbEntity OmdbFetcher(string title)
        {
                ImdbEntity obj = new ImdbEntity();
                obj = JsonConvert.DeserializeObject<ImdbEntity>(DownloadJesonMovie(title));
                if (obj.Response.Equals("True"))
                    return obj;
                else return null;

        }
        public string DownloadJesonMovie(string title)
        {
            using (var webClient = new WebClient())
            {
                //string represntation of the JSON
                //string rawJson = webClient.DownloadString("http://www.omdbapi.com/?t=" + title + "&plot=full&apikey=94919479");
                return webClient.DownloadString("http://www.omdbapi.com/?t=" + title + "&plot=full&apikey=94919479");
            }
        }
        [HttpPost, ActionName("Search")]
        [ValidateAntiForgeryToken]
        public void Search (string title)
        {
           /* func for serch just from movie page */
        }
    }
}
