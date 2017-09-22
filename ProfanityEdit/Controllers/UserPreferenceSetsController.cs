using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ProfanityEdit.Models;

namespace ProfanityEdit.Controllers
{
    public class UserPreferenceSetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserPreferenceSets
        public ActionResult Index()
        {
            return View(db.UserPreferenceSets.ToList());
        }

        // GET: UserPreferenceSets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPreferenceSet userPreferenceSet = db.UserPreferenceSets.Find(id);
            if (userPreferenceSet == null)
            {
                return HttpNotFound();
            }
            return View(userPreferenceSet);
        }

        // GET: UserPreferenceSets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserPreferenceSets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SkipAudio,SkipVideo")] UserPreferenceSet userPreferenceSet)
        {
            if (ModelState.IsValid)
            {
                db.UserPreferenceSets.Add(userPreferenceSet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userPreferenceSet);
        }

        // GET: UserPreferenceSets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPreferenceSet userPreferenceSet = db.UserPreferenceSets.Find(id);
            if (userPreferenceSet == null)
            {
                return HttpNotFound();
            }
            return View(userPreferenceSet);
        }

        // POST: UserPreferenceSets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SkipAudio,SkipVideo")] UserPreferenceSet userPreferenceSet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userPreferenceSet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userPreferenceSet);
        }

        // GET: UserPreferenceSets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPreferenceSet userPreferenceSet = db.UserPreferenceSets.Find(id);
            if (userPreferenceSet == null)
            {
                return HttpNotFound();
            }
            return View(userPreferenceSet);
        }

        // POST: UserPreferenceSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserPreferenceSet userPreferenceSet = db.UserPreferenceSets.Find(id);
            db.UserPreferenceSets.Remove(userPreferenceSet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
