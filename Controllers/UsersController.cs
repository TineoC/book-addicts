using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteca;

namespace Biblioteca.Controllers
{
    public class UsersController : Controller
    {
        private BibliotecaEntities db = new BibliotecaEntities();

        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,username,password,createdDate,userType")] User user)
        {
            if (ModelState.IsValid)
            {
                user.id = Guid.NewGuid();
                user.createdDate = DateTime.Now;
                user.userType = "usuario";
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login", "Books");
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
