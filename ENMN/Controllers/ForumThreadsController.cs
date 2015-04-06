using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ENMN.Models;

namespace ENMN.Controllers
{
    public class ForumThreadsController : Controller
    {
        private zpeterseEntities db = new zpeterseEntities();

        // GET: ForumThreads
        public ActionResult Index()
        {
            var forumThreads = db.ForumThreads.Include(f => f.Person);
            return View(forumThreads.ToList());
        }

        // GET: ForumThreads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumThread forumThread = db.ForumThreads.Find(id);
            if (forumThread == null)
            {
                return HttpNotFound();
            }
            return View(forumThread);
        }

        // GET: ForumThreads/Create
        public ActionResult Create()
        {
            ViewBag.Creator = new SelectList(db.People, "PersonID", "FirstName");
            return View();
        }

        // POST: ForumThreads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ForumThreadID,Creator,Title")] ForumThread forumThread)
        {
            if (ModelState.IsValid)
            {
                db.ForumThreads.Add(forumThread);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Creator = new SelectList(db.People, "PersonID", "FirstName", forumThread.Creator);
            return View(forumThread);
        }

        // GET: ForumThreads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumThread forumThread = db.ForumThreads.Find(id);
            if (forumThread == null)
            {
                return HttpNotFound();
            }
            ViewBag.Creator = new SelectList(db.People, "PersonID", "FirstName", forumThread.Creator);
            return View(forumThread);
        }

        // POST: ForumThreads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ForumThreadID,Creator,Title")] ForumThread forumThread)
        {
            if (ModelState.IsValid)
            {
                db.Entry(forumThread).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Creator = new SelectList(db.People, "PersonID", "FirstName", forumThread.Creator);
            return View(forumThread);
        }

        // GET: ForumThreads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumThread forumThread = db.ForumThreads.Find(id);
            if (forumThread == null)
            {
                return HttpNotFound();
            }
            return View(forumThread);
        }

        // POST: ForumThreads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ForumThread forumThread = db.ForumThreads.Find(id);
            db.ForumThreads.Remove(forumThread);
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
