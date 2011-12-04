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
        private FAQContext _context;

        public FAQController(FAQContext context)
        {
            this._context = context;
        }

        //
        // GET: /FAQ/

        public ViewResult Index()
        {
            return View(_context.FAQs.ToList());
        }

        //
        // GET: /FAQ/Details/5

        public ViewResult Details(int id)
        {
            FAQEntry faqentry = _context.FAQs.Find(id);
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
                _context.FAQs.Add(faqentry);
                _context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(faqentry);
        }
        
        //
        // GET: /FAQ/Edit/5
 
        public ActionResult Edit(int id)
        {
            FAQEntry faqentry = _context.FAQs.Find(id);
            return View(faqentry);
        }

        //
        // POST: /FAQ/Edit/5

        [HttpPost]
        public ActionResult Edit(FAQEntry faqentry)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(faqentry).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(faqentry);
        }

        //
        // GET: /FAQ/Delete/5
 
        public ActionResult Delete(int id)
        {
            FAQEntry faqentry = _context.FAQs.Find(id);
            return View(faqentry);
        }

        //
        // POST: /FAQ/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            FAQEntry faqentry = _context.FAQs.Find(id);
            _context.FAQs.Remove(faqentry);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
    }
}