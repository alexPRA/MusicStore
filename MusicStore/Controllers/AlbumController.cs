using MusicStore.Models;
using MusicStore.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace MusicStore.Controllers
{   [Authorize]
    public class AlbumController : Controller
    {
        private readonly Depot depot = new Depot();
        [HttpGet]
        public ActionResult Index(int? FiltreGenre, string FiltreTitre="", string FiltreArtiste ="" )
        {
            List<Album> lc = this.depot.Albums.List();
            if (FiltreTitre != "")
            {
                lc = lc.Where(o => o.Titre.Contains(FiltreTitre)).ToList();
            }
            if (FiltreArtiste != "")
            {
                lc = lc.Where(o => o.Artiste.Contains(FiltreArtiste)).ToList();
            }
            if (FiltreGenre != null)
            {
                lc = lc.Where(o => o.GenreId == FiltreGenre).ToList();
            }
            return View(lc);
        }
 

        [HttpGet, Authorize(Users = "admin")]
        public ActionResult Create()
        {
            Album g = new Album();
            return this.View(g);
        }
        [HttpPost, Authorize(Users = "admin")]
        public ActionResult Create(Album g)
        {
            if (this.ModelState.IsValid)
            {
                this.depot.Albums.Add(g);
                return this.RedirectToAction("Index", "Album");
            }
            return this.View(g);
        }
        [HttpGet,Authorize(Users = "admin")]
        public ActionResult Edit(int id)
        {
            Album c = this.depot.Albums.Find(id);

            if (c == null)
                return RedirectToAction("Index", "Album");
            return this.View(c);
        }
        [HttpPost, Authorize(Users = "admin")]
        public ActionResult Edit(Album g)
        {
            if (this.ModelState.IsValid)
            {
                this.depot.Albums.Update(g);
                return this.RedirectToAction("Index", "Album");
            }
            return this.View(g);
        }
        [HttpGet, Authorize(Users = "admin")]
        public ActionResult Delete(int id)
        {
            Album c = this.depot.Albums.Find(id);
            return this.View(c);
        }
        [HttpPost, Authorize(Users = "admin")]
        public ActionResult Delete(Album g)
        {
            try
            {
                this.depot.Albums.Remove(g.AlbumId);
                System.IO.File.Delete(Server.MapPath(g.Cover));
                return RedirectToAction("Index", "Album");
            }
            catch (System.Exception e)
            {
                this.ModelState.AddModelError("","Il est dans panier");
            }

            return View(g);
        }

    }
}
