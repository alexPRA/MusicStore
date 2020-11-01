using MusicStore.Models;
using MusicStore.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicStore.Controllers
{
    [Authorize(Users ="admin")]
    public class GenreController : Controller
    {
        private readonly Depot depot = new Depot();
        [HttpGet]
        public ActionResult Index()
        {
            List<Genre> lc = this.depot.Genres.List();
            return View(lc);
        }
        [HttpGet]
        public ActionResult Create()
        {
            Genre g = new Genre();
            return this.View(g);
        }
        [HttpPost]
        public ActionResult Create(Genre g)
        {
            if (this.ModelState.IsValid)
            {
                this.depot.Genres.Add(g);
                return this.RedirectToAction("Index", "Genre");
            }
            return this.View(g);
        }
        [HttpGet]
        public ActionResult Edit(int GenreId)
        {
            Genre c = depot.Genres.Find(GenreId);

            if (c == null)
                return RedirectToAction("Index", "Genre");

            return this.View(c);
        }
        [HttpPost]
        public ActionResult Edit(Genre g)
        {
            if (this.ModelState.IsValid)
            {
                this.depot.Genres.Update(g);
                return this.RedirectToAction("Index", "Genre");
            }
            return this.View(g);
        }
        [HttpGet]
        public ActionResult Delete(int GenreId)
        {
            Genre c = depot.Genres.Find(GenreId);
            return this.View(c);
        }
        [HttpPost]
        public ActionResult Delete(Genre g)
        {
            this.depot.Genres.Remove(g.GenreId);
            return this.RedirectToAction("Index", "Genre");
        }

    }
}