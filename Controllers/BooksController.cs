using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteca;

namespace Biblioteca.Controllers
{
    public class BooksController : Controller
    {
        private BibliotecaEntities db = new BibliotecaEntities();

        // GET: Books
        public ActionResult Index()
        {
            return View(db.Book.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,year,publisher,createdDate,description,cover,author,genre,valoration")] Book book)
        {
            book.id = Guid.NewGuid();
            book.createdDate = DateTime.Now;
            if (Request.Files.Count > 0)
            {
                var cover = Request.Files[0];

                if (cover != null && cover.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(cover.FileName);
                    var path = Path.Combine(Server.MapPath("/Public/Covers/"), fileName);
                    cover.SaveAs(path);

                    book.cover = fileName;
                }
            }
            db.Book.Add(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Books/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,year,publisher,createdDate,description,cover,author,genre,valoration")] Book book)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var cover = Request.Files[0];

                    if (cover != null && cover.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(cover.FileName);
                        var path = Path.Combine(Server.MapPath("/Public/Covers/"), fileName);
                        cover.SaveAs(path);

                        book.cover = fileName;
                    }
                }
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Book book = db.Book.Find(id);
            db.Book.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Books/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Books/Login
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "username,password")] User user, string login, string register)
        {
            if (!string.IsNullOrEmpty(login))
            {
                string username = Request["username"];
                string password = Request["password"];
                try
                {
                    if (db.login_books(username, password).Count() > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch
                {
                    return View(user);
                }
            }
            else if (!string.IsNullOrEmpty(register))
            {
                return RedirectToAction("Create", "Users");
            }
            return View(user);
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