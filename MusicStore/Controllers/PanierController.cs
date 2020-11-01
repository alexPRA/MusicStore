using MusicStore.Models;
using MusicStore.Models.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicStore.Controllers
{
    public class PanierController : Controller
    {
        private readonly Depot depot = new Depot();
         
        [HttpGet]
        public ActionResult Index()
        {
            Utilisateur u = depot.Utilisateurs.FindByUsername(User.Identity.Name);
            var panier = depot.Paniers.TousLesArticles(u.UtilisateurId);
            
            return View(panier);
        }
        public ActionResult Ajouter(int AlbumId)
        {
            Utilisateur u = depot.Utilisateurs.FindByUsername(User.Identity.Name);
            depot.Paniers.AjouterUnArticle(AlbumId, u.UtilisateurId);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(Album a)
        {
            Utilisateur u = depot.Utilisateurs.FindByUsername(User.Identity.Name);
            this.depot.Paniers.SupprimerUnArticle(a.AlbumId,u.UtilisateurId);

            return this.RedirectToAction("Index", "Panier");
        }
        public ActionResult DeleteAll()
        {
            Utilisateur u = depot.Utilisateurs.FindByUsername(User.Identity.Name);
            depot.Paniers.ViderLePanier(u.UtilisateurId);

            return RedirectToAction("Index","Panier");
        }
    }
}