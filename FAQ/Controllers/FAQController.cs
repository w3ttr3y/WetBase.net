using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FAQ.Models;
using FAQ.DAL;

namespace FAQ.Controllers
{ 
    public class FAQController : Controller
    {
        private FAQContext db = new FAQContext();

        //
        // GET: /FAQ/

        public ViewResult Index()
        {
            return View(db.FAQs.ToList());
        }

        //
        // GET: /FAQ/Details/5

        public ViewResult Details(int id)
        {
            FAQEntry faqentry = db.FAQs.Find(id);
            return View(faqentry);
        }

        //
        // GET: /FAQ/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /FAQ/Create

        [HttpPost]
        public ActionResult Create(FAQEntry faqentry)
        {
            if (ModelState.IsValid)
            {
                db.FAQs.Add(faqentry);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(faqentry);
        }
        
        //
        // GET: /FAQ/Edit/5
 
        public ActionResult Edit(int id)
        {
            FAQEntry faqentry = db.FAQs.Find(id);
            return View(faqentry);
        }

        //
        // POST: /FAQ/Edit/5

        [HttpPost]
        public ActionResult Edit(FAQEntry faqentry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faqentry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(faqentry);
        }

        //
        // GET: /FAQ/Delete/5
 
        public ActionResult Delete(int id)
        {
            FAQEntry faqentry = db.FAQs.Find(id);
            return View(faqentry);
        }

        //
        // POST: /FAQ/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            FAQEntry faqentry = db.FAQs.Find(id);
            db.FAQs.Remove(faqentry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}