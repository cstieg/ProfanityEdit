using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ProfanityEdit.Models;

namespace ProfanityEdit.Controllers
{
    public class GenerateMethodsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GenerateMethods
        public ActionResult Index()
        {
            return View(db.GenerateMethods.ToList());
        }

        // GET: GenerateMethods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenerateMethod generateMethod = db.GenerateMethods.Find(id);
            if (generateMethod == null)
            {
                return HttpNotFound();
            }
            return View(generateMethod);
        }

        // GET: GenerateMethods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GenerateMethods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] GenerateMethod generateMethod)
        {
            if (ModelState.IsValid)
            {
                db.GenerateMethods.Add(generateMethod);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(generateMethod);
        }

        // GET: GenerateMethods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenerateMethod generateMethod = db.GenerateMethods.Find(id);
            if (generateMethod == null)
            {
                return HttpNotFound();
            }
            return View(generateMethod);
        }

        // POST: GenerateMethods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] GenerateMethod generateMethod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(generateMethod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(generateMethod);
        }

        // GET: GenerateMethods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenerateMethod generateMethod = db.GenerateMethods.Find(id);
            if (generateMethod == null)
            {
                return HttpNotFound();
            }
            return View(generateMethod);
        }

        // POST: GenerateMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GenerateMethod generateMethod = db.GenerateMethods.Find(id);
            db.GenerateMethods.Remove(generateMethod);
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
