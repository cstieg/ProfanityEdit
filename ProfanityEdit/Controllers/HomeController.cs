using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ProfanityEdit.Models;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using ObjectHelpers;
using System.IO;
using System.Net.Mime;

namespace ProfanityEdit.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

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

        public async Task<ActionResult> MakeXspf(int id)
        {
            UserPreferenceSet preferenceSet = null;
            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (user != null)
            {
                preferenceSet = user.UserPreferenceSet;
            }

            if (preferenceSet == null)
            {
                preferenceSet = db.UserPreferenceSets.Where(u => u.Preset == true).First();
            }

            List<UserPreferenceSet> preferenceSetPresets = db.UserPreferenceSets.Where(u => u.Preset == true).ToList();

            MakeXspfViewModel model = new MakeXspfViewModel()
            {
                EditListId = id,
            };
            ObjectHelper.CopyProperties(preferenceSet, model, new List<string>() { "Id" });
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult DownloadXspf(MakeXspfViewModel model)
        {
            EditList editList = db.EditLists.Find(model.EditListId);
            if (editList == null)
            {
                return HttpNotFound();
            }

            UserPreferenceSet preferenceSet = new UserPreferenceSet();
            ObjectHelper.CopyProperties(model, preferenceSet, new List<string>() { "EditListId" });


            Stream stream = new Xspf().EditListToXspf(editList, preferenceSet);

            var cd = new ContentDisposition
            {
                FileName = "example.xspf",
                Inline = false
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());
            return new FileStreamResult(stream, "text/xml");
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

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}