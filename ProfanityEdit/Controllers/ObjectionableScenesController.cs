using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ProfanityEdit.Models;

namespace ProfanityEdit.Controllers
{
    public class ObjectionableScenesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ObjectionableScenes
        public ActionResult Index()
        {
            var objectionableScenes = db.ObjectionableScenes.Include(o => o.Category);
            return View(objectionableScenes.ToList());
        }

        // GET: ObjectionableScenes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObjectionableScene objectionableScene = db.ObjectionableScenes.Find(id);
            if (objectionableScene == null)
            {
                return HttpNotFound();
            }
            return View(objectionableScene);
        }

        // GET: ObjectionableScenes/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: ObjectionableScenes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,CategoryId,Level")] ObjectionableScene objectionableScene)
        {
            if (ModelState.IsValid)
            {
                db.ObjectionableScenes.Add(objectionableScene);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", objectionableScene.CategoryId);
            return View(objectionableScene);
        }

        // GET: ObjectionableScenes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObjectionableScene objectionableScene = db.ObjectionableScenes.Find(id);
            if (objectionableScene == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", objectionableScene.CategoryId);
            return View(objectionableScene);
        }

        // POST: ObjectionableScenes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,CategoryId,Level")] ObjectionableScene objectionableScene)
        {
            if (ModelState.IsValid)
            {
                db.Entry(objectionableScene).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", objectionableScene.CategoryId);
            return View(objectionableScene);
        }

        // GET: ObjectionableScenes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObjectionableScene objectionableScene = db.ObjectionableScenes.Find(id);
            if (objectionableScene == null)
            {
                return HttpNotFound();
            }
            return View(objectionableScene);
        }

        // POST: ObjectionableScenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ObjectionableScene objectionableScene = db.ObjectionableScenes.Find(id);
            db.ObjectionableScenes.Remove(objectionableScene);
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
