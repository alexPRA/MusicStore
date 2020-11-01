using MusicStore.Models;
using MusicStore.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicStore.Controllers
{
    [Authorize]
    public class MusiqueController : Controller
    {
        
        // GET: Musique
        public ActionResult Index(int GenreId)
        {
            Depot depot = new Depot();
            var lc  = depot.Albums.List().Where(o => o.GenreId == GenreId).ToList();

            return View(lc);
        }
    }
}