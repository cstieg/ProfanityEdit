using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ProfanityEdit.Models;
using ModelControllerHelpers;
using System.Web;
using CsvHelper;
using System.IO;

namespace ProfanityEdit.Controllers
{
    public class ProfanitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Profanities
        public ActionResult Index()
        {
            var profanities = db.Profanities.Include(p => p.Category);
            return View(profanities.ToList());
        }

        // GET: Profanities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profanity profanity = db.Profanities.Find(id);
            if (profanity == null)
            {
                return HttpNotFound();
            }
            return View(profanity);
        }

        // GET: Profanities/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Profanities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Word,CategoryId,Level,Ask")] Profanity profanity)
        {
            if (ModelState.IsValid)
            {
                db.Profanities.Add(profanity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", profanity.CategoryId);
            return View(profanity);
        }

        // GET: Profanities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profanity profanity = db.Profanities.Find(id);
            if (profanity == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", profanity.CategoryId);
            return View(profanity);
        }

        // POST: Profanities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Word,CategoryId,Level,Ask")] Profanity profanity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profanity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", profanity.CategoryId);
            return View(profanity);
        }

        // GET: Profanities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profanity profanity = db.Profanities.Find(id);
            if (profanity == null)
            {
                return HttpNotFound();
            }
            return View(profanity);
        }

        // POST: Profanities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profanity profanity = db.Profanities.Find(id);
            db.Profanities.Remove(profanity);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UploadProfanityCsv()
        {
            HttpPostedFileBase file = ModelControllerHelper.GetFile(ModelState, Request, "profanityList");

            StreamReader textReader = new StreamReader(file.InputStream);
            var csvParser = new CsvParser(textReader);
            string[] headerRow = csvParser.Read();
            string[] dataRow = csvParser.Read();
            while (dataRow != null)
            {
                string pWord = dataRow[0];
                bool newEntry = false;
                Profanity profanity = db.Profanities.Where(p => p.Word == pWord).SingleOrDefault();

                if (profanity == null)
                {
                    newEntry = true;
                    profanity = new Profanity();
                }

                profanity.Word = dataRow[0];
                profanity.CategoryId = int.Parse(dataRow[1]);
                profanity.Level = int.Parse(dataRow[2]);
                profanity.Ask = (dataRow[3].ToLower() == "yes");

                if (newEntry)
                {
                    db.Profanities.Add(profanity);
                } 
                else
                {
                    db.Entry(profanity).State = EntityState.Modified;
                }

                db.SaveChanges();

                // read next row
                dataRow = csvParser.Read();
            }

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
