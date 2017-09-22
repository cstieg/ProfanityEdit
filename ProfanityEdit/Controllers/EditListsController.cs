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

            // Create XSPF document foundation
            XNamespace ns = @"http://xspf.org/ns/o";
            XNamespace vlcNs = @"http://www.videolan.org/vlc/playlist/ns/o";
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", null), 
                new XElement(ns+"playlist",
                    new XAttribute(XNamespace.Xmlns+"vlc", vlcNs.NamespaceName),
                    new XElement(ns+"title", "Playlist"),
                    new XElement(ns+"trackList")));

            // Process
            editList.EditListItems.Sort(new Comparison<EditListItem>(CompStartTime));
            
            float startTime = 0.000F;
            float endTime = 0.000F;
            int trackId = 0;
            for (int i = 0; i < editList.EditListItems.Count; i++)
            {
                var editListItem = editList.EditListItems[i];
                endTime = editListItem.StartTime;

                // add clear video
                AddVideoSegmentToXspf(doc, ns, vlcNs, trackId, startTime, endTime, false);
                trackId++;

                // add muted video
                AddVideoSegmentToXspf(doc, ns, vlcNs, trackId, editListItem.StartTime, editListItem.EndTime, true);
                trackId++;

                startTime = editListItem.EndTime;
            }

            // final segment
            AddVideoSegmentToXspf(doc, ns, vlcNs, trackId, startTime, 0, false);


            var fileStream = new MemoryStream();
            doc.Save(fileStream);
            fileStream.Position = 0;
            var cd = new ContentDisposition
            {
                FileName = "example.xspf",
                Inline = false
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());
            return new FileStreamResult(fileStream, "text/xml");
        }
        
        private void AddVideoSegmentToXspf(XDocument doc, XNamespace ns, XNamespace vlcNs, int id, float startTime, float stopTime, bool muted = false)
        {
            XElement trackList = doc.Descendants(ns+"trackList").First();
            XElement extension = new XElement(ns + "extension",
                        new XAttribute("application", @"http://www.videolan.org/vlc/playlist/0"),
                        new XElement(vlcNs + "id", id.ToString()),
                        new XElement(vlcNs + "option", @"start-time=" + startTime.ToString()),
                        new XElement(vlcNs + "option", @"stop-time=" + stopTime.ToString()));
            trackList.Add(
                new XElement(ns+"track",
                    new XElement(ns+"location", @"dvd:///f:/#1"),
                    extension));

            if (muted)
            {
                extension.Add(new XElement(vlcNs+"option", "no-audio"));
            }
        }

        private int CompStartTime(EditListItem a, EditListItem b)
        {
            return a.StartTime.CompareTo(b.StartTime);
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
