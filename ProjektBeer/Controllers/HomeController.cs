
using ProjektBeer.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjektBeer.Controllers
{
    public class HomeController : Controller
    {

        s13898Entities1 db = new s13898Entities1();

        public ActionResult Index()
        {
            return View();
        }


        // Dokladne inforacje o piwie
        [HttpGet]
        public ActionResult Details(int id)
        {
            Beer beer = new Beer();
            using (s13898Entities1 db = new s13898Entities1())
            {
                beer = db.Beers.Where(x => x.IdBeer == id).FirstOrDefault();
            }
            return View(beer);
        }


        // Nowe piwo
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Beer imageModel, Beer beer)
        {
                string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
                string extension = Path.GetExtension(imageModel.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                imageModel.ImagePath = "~/Content/img/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Content/img"), fileName);
                imageModel.ImageFile.SaveAs(fileName);

                if (ModelState.IsValid) {

                    db.Beers.Add(beer);
                    db.Beers.Add(imageModel);
                    db.SaveChanges();

            }

            return View(beer);

        }

        public ActionResult Beers()
        {
            return View(db.Beers.ToList());
        }




        // GET: PersonalDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beer beer = db.Beers.Find(id);
            if (beer == null)
            {
                return HttpNotFound();
            }
            return View(beer);
        }

        // POST: PersonalDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Beer beer = db.Beers.Find(id);
            db.Beers.Remove(beer);
            db.SaveChanges();
            return RedirectToAction("Beers");
        }


        // GET: PersonalDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beer beer = db.Beers.Find(id);
            if (beer == null)
            {
                return HttpNotFound();
            }
            return View(beer);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Beer beer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Beers");
            }
            return View(beer);
        }

        public ActionResult News()
        {
            return View();
        }

        public ActionResult Brand()
        {
            return View();
        }

    }
}