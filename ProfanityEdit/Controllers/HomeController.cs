using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ProfanityEdit.Models;


namespace ProfanityEdit.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchResults()
        {
            var searchTerm = Request.Params.Get("movieName");
            var searchResults = db.Movies.Include(m => m.EditLists)
                .Where(m => m.Name.ToLower().Contains(searchTerm.ToLower())).ToList();

            return View(searchResults);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}