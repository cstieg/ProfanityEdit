﻿using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ProfanityEdit.Models;
using ProfanityEdit.Modules.Srt;
using System.Collections.Generic;

namespace ProfanityEdit.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Movies
        public ActionResult Index()
        {
            var movies = db.Movies.Include(m => m.Rating);
            return View(movies.ToList());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            ViewBag.RatingId = new SelectList(db.Ratings, "Id", "Name");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,ReleaseYear,RunTime,RatingId,SubtitleText")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                // Convert subtitle text to edit list
                ProcessSubtitleText(movie);

                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RatingId = new SelectList(db.Ratings, "Id", "Name", movie.RatingId);
            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            movie.EditLists = db.EditLists.Where(e => e.MovieId == movie.Id).ToList();

            ViewBag.RatingId = new SelectList(db.Ratings, "Id", "Name", movie.RatingId);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ReleaseYear,RunTime,RatingId,SubtitleText")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                // Convert subtitle text to edit list
                ProcessSubtitleText(movie);

                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RatingId = new SelectList(db.Ratings, "Id", "Name", movie.RatingId);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
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



        private void ProcessSubtitleText(Movie movie)
        {
            var srt = new Srt(movie.SubtitleText);


            movie.EditLists = db.EditLists.Where(e => e.MovieId == movie.Id).ToList();
            var editList = new EditList(movie, srt, db.Profanities.ToList());

            // Don't add if identical editList already exists
            if (movie.EditLists == null)
            {
                movie.EditLists = new List<EditList>();
            }
            else
            {
                for (int i = 0; i < movie.EditLists.Count; i++)
                {
                    if (editList.Equals(movie.EditLists[i]))
                    {
                        return;
                    }
                }
            }

            editList.EditListItems = editList.EditListItems.OrderBy(e => e.StartTime).ToList();

            db.EditLists.Add(editList);
            movie.EditLists.Add(editList);
            db.SaveChanges();
        }
    }
}
