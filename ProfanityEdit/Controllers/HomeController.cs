using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ProfanityEdit.Models;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
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

        /// <summary>
        /// Renders the form to make the Xspf playlist, allowing user to choose preferences
        /// </summary>
        /// <param name="id">The id of the editList from which to make the playlist</param>
        public ActionResult MakeXspf(int id)
        {
            UserPreferenceSet preferenceSet = null;
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());

            // If user is logged in, get the details of the last used custom preference set
            if (user != null)
            {
                preferenceSet = user.UserPreferenceSet;
            }

            // If there is no custom preference set registered for the user yet, use the first preset
            var defaultPreferenceSet = db.UserPreferenceSets.Where(u => u.Preset == true).First();
            if (preferenceSet == null)
            {
                preferenceSet = new UserPreferenceSet(db.Categories.ToList());
                defaultPreferenceSet.CopyUserPreferenceSet(preferenceSet);
            }

            var viewModel = new MakeXspfViewModel()
            {
                UserPreferenceSetId = preferenceSet.Id,
                SkipAudio = preferenceSet.SkipAudio,
                SkipVideo = preferenceSet.SkipVideo,
                UserPreferenceItems = preferenceSet.UserPreferenceItems,
                EditListId = id
            };

            // Pass current preference selection and presets to view
            if (user != null && user.SelectedPreferenceSetId != null)
            {
                ViewBag.SelectedPreferenceSetId = user.SelectedPreferenceSetId;
            }
            else
            {
                ViewBag.SelectedPreferenceSetId = defaultPreferenceSet.Id;
            }
            ViewBag.PreferenceSetPresets = db.UserPreferenceSets.Where(u => u.Preset == true).ToList();
            return View(viewModel);
        }

        /// <summary>
        /// Update preference set and return xspf
        /// </summary>
        /// <param name="model">The preference set info</param>
        /// <returns>The xspf file</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DownloadXspf(MakeXspfViewModel viewModel)
        {
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());

            // editList containing potentially objectionable material
            EditList editList = db.EditLists.Find(viewModel.EditListId);
            if (editList == null)
            {
                return HttpNotFound();
            }

            UserPreferenceSet preferenceSet;
            string preferenceSetChoice = Request.Params.Get("preferenceSet");

            // get preference set model details if custom
            if (preferenceSetChoice == "custom")
            {
                // copy user preferences from viewModel to db model
                preferenceSet = db.UserPreferenceSets.Find(viewModel.UserPreferenceSetId) ?? new UserPreferenceSet(db.Categories.ToList());
                preferenceSet.SkipAudio = viewModel.SkipAudio;
                preferenceSet.SkipVideo = viewModel.SkipVideo;
                // copy each user preference item
                for (int i = 0; i < preferenceSet.UserPreferenceItems.Count; i++)
                {
                    var preferenceItem = preferenceSet.UserPreferenceItems[i];
                    preferenceItem.AllowLevel = viewModel.UserPreferenceItems.Where(p => p.CategoryId == preferenceItem.CategoryId).Single().AllowLevel;
                }

                // save preference set if user is logged in
                if (user != null)
                {
                    var saveAction = user.UserPreferenceSet == null ? EntityState.Added : EntityState.Modified;
                    for (int i = 0; i < preferenceSet.UserPreferenceItems.Count(); i++)
                    {
                        db.Entry(preferenceSet.UserPreferenceItems[i]).State = saveAction;
                    }
                    db.Entry(preferenceSet).State = saveAction;
                    db.SaveChanges();
                    user.UserPreferenceSet = preferenceSet;
                }
            }
            // for presets, simply lookup the preset
            else
            {
                int preferenceSetId = int.Parse(Request.Params.Get("preferenceSet"));
                preferenceSet = db.UserPreferenceSets.Find(preferenceSetId);
            }

            // save preference set reference in user table
            if (user != null)
            {
                user.SelectedPreferenceSet = preferenceSet;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
            

            // create and return xspf
            Stream stream = new Xspf().EditListToXspf(editList, preferenceSet);

            var cd = new ContentDisposition
            {
                FileName = editList.Movie.Name + ".xspf",
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