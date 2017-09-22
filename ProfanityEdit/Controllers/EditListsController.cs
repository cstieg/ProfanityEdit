using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ProfanityEdit.Models;
using System.Xml;
using System;
using System.IO;
using System.Xml.Linq;
using System.Net.Mime;

namespace ProfanityEdit.Controllers
{
    public class EditListsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EditLists
        public ActionResult Index()
        {
            var editLists = db.EditLists.Include(e => e.Movie);
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

            // Order by start time
            editList.EditListItems = editList.EditListItems.OrderBy(e => e.StartTime).ToList();

            return View(editList);
        }

        // GET: EditLists/Create
        public ActionResult Create()
        {
            ViewBag.MovieId = new SelectList(db.Movies, "Id", "Name");
            return View();
        }

        // POST: EditLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,MovieId,EditDate,GenerateMethod")] EditList editList)
        {
            if (ModelState.IsValid)
            {
                db.EditLists.Add(editList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            ViewBag.MovieId = new SelectList(db.Movies, "Id", "Name", editList.MovieId);
            return View(editList);
        }

        // POST: EditLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,MovieId,EditDate,GenerateMethod")] EditList editList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(editList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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




        public ActionResult DownloadXspf(int id)
        {
            EditList editList = db.EditLists.Find(id);
            if (editList == null)
            {
                return HttpNotFound();
            }

            // Todo: get preference set id from user
            int userPreferenceSetId = 0;
            UserPreferenceSet preferenceSet = db.UserPreferenceSets.Find(userPreferenceSetId);

            Stream stream = new Xspf().EditListToXspf(editList, preferenceSet);

            var cd = new ContentDisposition
            {
                FileName = "example.xspf",
                Inline = false
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());
            return new FileStreamResult(stream, "text/xml");
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
