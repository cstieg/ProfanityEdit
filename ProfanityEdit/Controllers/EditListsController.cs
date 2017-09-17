using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ProfanityEdit.Models;

namespace ProfanityEdit.Controllers
{
    public class EditListsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EditLists
        public ActionResult Index()
        {
            var editLists = db.EditLists.Include(e => e.Editor).Include(e => e.GenerateMethod).Include(e => e.Movie);
            return View(editLists.ToList());
        }

        // GET: EditLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditList editList = db.EditLists.Find(id);
            if (editList == null)
            {
                return HttpNotFound();
            }
            return View(editList);
        }

        // GET: EditLists/Create
        public ActionResult Create()
        {
            ViewBag.EditorId = new SelectList(db.Users, "Id", "Email");
            ViewBag.GenerateMethodId = new SelectList(db.GenerateMethods, "Id", "Name");
            ViewBag.MovieId = new SelectList(db.Movies, "Id", "Name");
            return View();
        }

        // POST: EditLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,MovieId,EditorId,EditDate,GenerateMethodId")] EditList editList)
        {
            if (ModelState.IsValid)
            {
                db.EditLists.Add(editList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EditorId = new SelectList(db.Users, "Id", "Email", editList.EditorId);
            ViewBag.GenerateMethodId = new SelectList(db.GenerateMethods, "Id", "Name", editList.GenerateMethodId);
            ViewBag.MovieId = new SelectList(db.Movies, "Id", "Name", editList.MovieId);
            return View(editList);
        }

        // GET: EditLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditList editList = db.EditLists.Find(id);
            if (editList == null)
            {
                return HttpNotFound();
            }
            ViewBag.EditorId = new SelectList(db.Users, "Id", "Email", editList.EditorId);
            ViewBag.GenerateMethodId = new SelectList(db.GenerateMethods, "Id", "Name", editList.GenerateMethodId);
            ViewBag.MovieId = new SelectList(db.Movies, "Id", "Name", editList.MovieId);
            return View(editList);
        }

        // POST: EditLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,MovieId,EditorId,EditDate,GenerateMethodId")] EditList editList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(editList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EditorId = new SelectList(db.Users, "Id", "Email", editList.EditorId);
            ViewBag.GenerateMethodId = new SelectList(db.GenerateMethods, "Id", "Name", editList.GenerateMethodId);
            ViewBag.MovieId = new SelectList(db.Movies, "Id", "Name", editList.MovieId);
            return View(editList);
        }

        // GET: EditLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditList editList = db.EditLists.Find(id);
            if (editList == null)
            {
                return HttpNotFound();
            }
            return View(editList);
        }

        // POST: EditLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EditList editList = db.EditLists.Find(id);
            db.EditLists.Remove(editList);
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
