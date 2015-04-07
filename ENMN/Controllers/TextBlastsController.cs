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
    public class TextBlastsController : Controller
    {
        private zpeterseEntities db = new zpeterseEntities();

        // GET: TextBlasts
        public ActionResult Index()
        {
            var textBlasts = db.TextBlasts.Include(t => t.Group).Include(t => t.Person);
            return View(textBlasts.ToList());
        }

        // GET: TextBlasts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TextBlast textBlast = db.TextBlasts.Find(id);
            if (textBlast == null)
            {
                return HttpNotFound();
            }
            return View(textBlast);
        }

        // GET: TextBlasts/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name");
            ViewBag.Creator = new SelectList(db.People, "PersonID", "FirstName");
            return View();
        }

        // POST: TextBlasts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TextBlastID,Title,Creator,GroupID,DateSent")] TextBlast textBlast)
        {
            if (ModelState.IsValid)
            {
                db.TextBlasts.Add(textBlast);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name", textBlast.GroupID);
            ViewBag.Creator = new SelectList(db.People, "PersonID", "FirstName", textBlast.Creator);
            return View(textBlast);
        }

        // GET: TextBlasts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TextBlast textBlast = db.TextBlasts.Find(id);
            if (textBlast == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name", textBlast.GroupID);
            ViewBag.Creator = new SelectList(db.People, "PersonID", "FirstName", textBlast.Creator);
            return View(textBlast);
        }

        // POST: TextBlasts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TextBlastID,Title,Creator,GroupID,DateSent")] TextBlast textBlast)
        {
            if (ModelState.IsValid)
            {
                db.Entry(textBlast).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name", textBlast.GroupID);
            ViewBag.Creator = new SelectList(db.People, "PersonID", "FirstName", textBlast.Creator);
            return View(textBlast);
        }

        // GET: TextBlasts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TextBlast textBlast = db.TextBlasts.Find(id);
            if (textBlast == null)
            {
                return HttpNotFound();
            }
            return View(textBlast);
        }

        // POST: TextBlasts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TextBlast textBlast = db.TextBlasts.Find(id);
            db.TextBlasts.Remove(textBlast);
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
