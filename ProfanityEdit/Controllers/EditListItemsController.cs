using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ProfanityEdit.Models;

namespace ProfanityEdit.Controllers
{
    public class EditListItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EditListItems
        public ActionResult Index()
        {
            var editListItems = db.EditListItems.Include(e => e.EditList).Include(e => e.ObjectionableScene).Include(e => e.Profanity);
            return View(editListItems.ToList());
        }

        // GET: EditListItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditListItem editListItem = db.EditListItems.Find(id);
            if (editListItem == null)
            {
                return HttpNotFound();
            }
            return View(editListItem);
        }

        // GET: EditListItems/Create
        public ActionResult Create()
        {
            ViewBag.EditListId = new SelectList(db.EditLists, "Id", "Name");
            ViewBag.ObjectionableSceneId = new SelectList(db.ObjectionableScenes, "Id", "Description");
            ViewBag.ProfanityId = new SelectList(db.Profanities, "Id", "Word");
            return View();
        }

        // POST: EditListItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EditListId,Description,StartTime,EndTime,Audio,Video,ProfanityId,ObjectionableSceneId")] EditListItem editListItem)
        {
            if (ModelState.IsValid)
            {
                db.EditListItems.Add(editListItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EditListId = new SelectList(db.EditLists, "Id", "Name", editListItem.EditListId);
            ViewBag.ObjectionableSceneId = new SelectList(db.ObjectionableScenes, "Id", "Description", editListItem.ObjectionableSceneId);
            ViewBag.ProfanityId = new SelectList(db.Profanities, "Id", "Word", editListItem.ProfanityId);
            return View(editListItem);
        }

        // GET: EditListItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditListItem editListItem = db.EditListItems.Find(id);
            if (editListItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.EditListId = new SelectList(db.EditLists, "Id", "Name", editListItem.EditListId);
            ViewBag.ObjectionableSceneId = new SelectList(db.ObjectionableScenes, "Id", "Description", editListItem.ObjectionableSceneId);
            ViewBag.ProfanityId = new SelectList(db.Profanities, "Id", "Word", editListItem.ProfanityId);
            return View(editListItem);
        }

        // POST: EditListItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EditListId,Description,StartTime,EndTime,Audio,Video,ProfanityId,ObjectionableSceneId")] EditListItem editListItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(editListItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EditListId = new SelectList(db.EditLists, "Id", "Name", editListItem.EditListId);
            ViewBag.ObjectionableSceneId = new SelectList(db.ObjectionableScenes, "Id", "Description", editListItem.ObjectionableSceneId);
            ViewBag.ProfanityId = new SelectList(db.Profanities, "Id", "Word", editListItem.ProfanityId);
            return View(editListItem);
        }

        // GET: EditListItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditListItem editListItem = db.EditListItems.Find(id);
            if (editListItem == null)
            {
                return HttpNotFound();
            }
            return View(editListItem);
        }

        // POST: EditListItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EditListItem editListItem = db.EditListItems.Find(id);
            db.EditListItems.Remove(editListItem);
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
