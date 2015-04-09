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
    public class MessagesController : Controller
    {
        private zpeterseEntities db = new zpeterseEntities();

        // GET: Messages
        public ActionResult Index(int ThreadID)
        {
            var messages = db.Messages.Include(m => m.Person).Include(m => m.MessageThread).Where(m => m.MessageThreadID == ThreadID).OrderBy(m=> m.OrderNo);
            return View(messages.ToList());
        }

        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        public ActionResult Create()
        {
            ViewBag.SenderID = new SelectList(db.People, "PersonID", "FirstName");
            ViewBag.MessageThreadID = new SelectList(db.MessageThreads, "MessageThreadID", "MessageThreadID");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MessageID,MessageThreadID,DateTime,OrderNo,Text,SenderID")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                Person temp= db.People.where(m => m.PersonID=message.MessageThread.MotherID);
                sendGCM(temp.GCMConnectionString ,message.MessageID);
                return RedirectToAction("Index");
            }

            ViewBag.SenderID = new SelectList(db.People, "PersonID", "FirstName", message.SenderID);
            ViewBag.MessageThreadID = new SelectList(db.MessageThreads, "MessageThreadID", "MessageThreadID", message.MessageThreadID);
            return View(message);
        }

        // GET: Messages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.SenderID = new SelectList(db.People, "PersonID", "FirstName", message.SenderID);
            ViewBag.MessageThreadID = new SelectList(db.MessageThreads, "MessageThreadID", "MessageThreadID", message.MessageThreadID);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MessageID,MessageThreadID,DateTime,OrderNo,Text,SenderID")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SenderID = new SelectList(db.People, "PersonID", "FirstName", message.SenderID);
            ViewBag.MessageThreadID = new SelectList(db.MessageThreads, "MessageThreadID", "MessageThreadID", message.MessageThreadID);
            return View(message);
        }

        // GET: Messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
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

        void sendGCM(string regId, int messageId)
        {
            var applicationID = "AIzaSyCb7ulAlp7e7lQQ_gjtiZvIB0FpjSm0IU8"; //the API key is this the right thing?


            var SENDER_ID = "763683898982";  //project ID

                WebRequest tRequest;
                tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));

                tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                // string postData = "{ 'registration_id': [ '" + regId + "' ], 'data': {'message': '" + txtMsg.Text + "'}}";
                string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + messageId + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + regId;
                Console.WriteLine(postData);
                Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                tRequest.ContentLength = byteArray.Length;

                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse tResponse = tRequest.GetResponse();

                dataStream = tResponse.GetResponseStream();

                StreamReader tReader = new StreamReader(dataStream);

                String sResponseFromServer = tReader.ReadToEnd();

                lblStat.Text = sResponseFromServer;
                tReader.Close();
                dataStream.Close();
                tResponse.Close();
            
        }
    }
}
