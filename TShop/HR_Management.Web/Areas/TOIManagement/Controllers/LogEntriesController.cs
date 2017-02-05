using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HR_Management.Context;
using HR_Management.Models;

namespace HR_Management.Web.Areas.TOIManagement.Controllers
{
    public class LogEntriesController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public ActionResult LogEntries(string entityFormalNamePlural)
        {

            IEnumerable<LogEntry> logEntries = _dbContext.LogEntries.Where(le => le.EntityFormalNamePlural == entityFormalNamePlural)
                                                    .OrderByDescending(le => le.LogDate);

            return PartialView("_LogEntries", logEntries);
        }
        public ActionResult Index()
        {
            return View(_dbContext.LogEntries.ToList());
        }

        // GET: LogEntries/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogEntry logEntry = _dbContext.LogEntries.Find(id);
            if (logEntry == null)
            {
                return HttpNotFound();
            }
            return View(logEntry);
        }

        // GET: LogEntries/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LogEntryID,LogDate,Logger,LogLevel,Thread,EntityFormalNamePlural,EntityKeyValue,UserName,Message,Exception")] LogEntry logEntry)
        {
            if (ModelState.IsValid)
            {
                _dbContext.LogEntries.Add(logEntry);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(logEntry);
        }


        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogEntry logEntry = _dbContext.LogEntries.Find(id);
            if (logEntry == null)
            {
                return HttpNotFound();
            }
            return View(logEntry);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LogEntryID,LogDate,Logger,LogLevel,Thread,EntityFormalNamePlural,EntityKeyValue,UserName,Message,Exception")] LogEntry logEntry)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(logEntry).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(logEntry);
        }


        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogEntry logEntry = _dbContext.LogEntries.Find(id);
            if (logEntry == null)
            {
                return HttpNotFound();
            }
            return View(logEntry);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            LogEntry logEntry = _dbContext.LogEntries.Find(id);
            _dbContext.LogEntries.Remove(logEntry);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
