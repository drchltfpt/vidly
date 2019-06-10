using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;


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
		public ActionResult Index( )
		{
			var movies = _context.Movies.Include(g => g.Genre).ToList();
			return View(movies);
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