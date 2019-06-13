using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
	public class MoviesController : Controller
	{

		private VidlyContext _context;

		public MoviesController()
		{
			_context = new VidlyContext();
		}
		// ctor

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}
		//
		// GET: /Movies/

		//[Route("movies/released/{year}/{month:regex(\\d{4}):range(1, 12)}")]

		// customers
		public ActionResult Index()
		{
			return View();
		}
		// mvcaction4

		public ActionResult Details(int id)
		{
			var movie = _context.Movies.Include(g => g.Genre).SingleOrDefault(m => m.Id == id);

			if (movie == null)
			{
				HttpNotFound();
			}

			return View(movie);
		}

		public ActionResult New()
		{
			var genres = _context.Genres.ToList();
			var viewModel = new MovieFormViewModel
			{
				Genres = genres
			};

			return View("MovieForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Save(Movie movie)
		{
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };

                return View("MovieForm", viewModel);
            }
			// Type of movie: update or add
			if (movie.Id == 0)
			{
				movie.DateAdded = DateTime.Now;
				_context.Movies.Add(movie);
			}
			else
			{
				// Get movie by ID
				var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

				// Set value for movie
				movieInDb.Name = movie.Name;
				movieInDb.GenreId = movie.GenreId;
				movieInDb.ReleaseDate = movie.ReleaseDate;
				movieInDb.DateAdded = DateTime.Now;
				movieInDb.NumberInStock = movie.NumberInStock;
			}

			_context.SaveChanges();

			return RedirectToAction("Index", "Movies");
		}

		public ActionResult Edit(int id)
		{
			var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

			if (movie == null)
				return HttpNotFound();

			var viewModel = new MovieFormViewModel(movie)
			{
				Genres = _context.Genres.ToList()
			};

			return View("MovieForm", viewModel);
		}
		//public ActionResult Edit(int id)
		//{
		//    return Content("id=" + id);
		//}

		// movies
		//public ActionResult Index(int? pageIndex, string sortBy)
		//{
		//    if (!pageIndex.HasValue)
		//    {
		//        pageIndex = 1;
		//    }

		//    if (String.IsNullOrWhiteSpace(sortBy))
		//        sortBy = "Name";
		//    return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
		//}

		//public ActionResult ByReleaseDate(int year, int month)
		//{
		//    return Content(year + "/" + month);
		//}
		public ActionResult Random()
		{
			var movie = new Movie() { Name = "Shrek!" };

			var customers = new List<Customer> 
			{
				new Customer { Name = "Customer 1"},
				new Customer { Name = "Customer 2"}
			};

			var viewModel = new RandomMovieViewModel
			{
				Movie = movie,
				Customers = customers
			};

			return View(viewModel);
			//return Content("Hello World!");
			//return HttpNotFound(); ***
			//return new EmptyResult();
			//return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" }); ***
		}
	}
}