using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ProfanityEdit.Models;

namespace ProfanityEdit.Controllers
{
    public class UserPreferenceItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserPreferenceItems
        public ActionResult Index()
        {
            var userPreferenceItems = db.UserPreferenceItems.Include(u => u.Category);
            return View(userPreferenceItems.ToList());
        }

        // GET: UserPreferenceItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPreferenceItem userPreferenceItem = db.UserPreferenceItems.Find(id);
            if (userPreferenceItem == null)
            {
                return HttpNotFound();
            }
            return View(userPreferenceItem);
        }

        // GET: UserPreferenceItems/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: UserPreferenceItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryId,AllowLevel")] UserPreferenceItem userPreferenceItem)
        {
            if (ModelState.IsValid)
            {
                db.UserPreferenceItems.Add(userPreferenceItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", userPreferenceItem.CategoryId);
            return View(userPreferenceItem);
        }

        // GET: UserPreferenceItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPreferenceItem userPreferenceItem = db.UserPreferenceItems.Find(id);
            if (userPreferenceItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", userPreferenceItem.CategoryId);
            return View(userPreferenceItem);
        }

        // POST: UserPreferenceItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,AllowLevel")] UserPreferenceItem userPreferenceItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userPreferenceItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", userPreferenceItem.CategoryId);
            return View(userPreferenceItem);
        }

        // GET: UserPreferenceItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPreferenceItem userPreferenceItem = db.UserPreferenceItems.Find(id);
            if (userPreferenceItem == null)
            {
                return HttpNotFound();
            }
            return View(userPreferenceItem);
        }

        // POST: UserPreferenceItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserPreferenceItem userPreferenceItem = db.UserPreferenceItems.Find(id);
            db.UserPreferenceItems.Remove(userPreferenceItem);
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
