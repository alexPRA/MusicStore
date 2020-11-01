namespace MusicStore.Controllers {
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Web.Mvc;
    using System.Web.Security;
    using MusicStore.Models;
    using MusicStore.Models.DAL;
    using MusicStore.Models.DataModels;
    using MusicStore.Models.ViewModels.Utilisateur;

    //Les meilleures pratique indiquent qu'il est préférable d'indiquer
    //[Authorize] sur le contrôleur et ensuit [AllowAnonymous] sur 
    //les méthodes qu'il est possible d'appeller sans être authentifié

    public class UtilisateurController : Controller {
        private readonly Depot depot = new Depot();

        [HttpGet, AllowAnonymous]
        public ActionResult Create() {
            return this.View(new Inscription());
        }

        [HttpPost, AllowAnonymous]
        public ActionResult Create(Inscription i) {
            if (this.ModelState.IsValid) {
                try {
                    Utilisateur u = new Utilisateur();
                    u.Courriel = i.Courriel;
                    u.MotDePasse = i.MotDePasse;
                    u.NomUtilisateur = i.NomUtilisateur;
                    this.depot.Utilisateurs.Add(u);
                    return this.RedirectToAction("Index", "Home");
                }
                catch (SqlException e) {
                    this.ModelState.AddModelError("", e.Message);
                }
            }
            return this.View(i);
        }

        [HttpGet, AllowAnonymous]
        public ActionResult Login() {
            return this.View(new Login());
        }

        [HttpPost, AllowAnonymous]
        public ActionResult Login(Login login) {
            if (this.ModelState.IsValid) {
                //Envoyer le cookie d'authentification lorsque Login valide
                //Dans cet exemple on envoi le nom d'utilisateur, qui est unique par contrainte BD
                FormsAuthentication.SetAuthCookie(login.NomUtilisateur, login.ResterConnecté);
                return this.RedirectToAction("Index", "Home");
            }
            else {
                return this.View(login);
            }
        }

        [HttpGet, Authorize]
        public ActionResult Logout() {
            FormsAuthentication.SignOut();
            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet, Authorize]
        public ActionResult Edit() {
            //Obtenir les données de l'utilisateur courant
            Utilisateur u = this.depot.Utilisateurs.FindByUsername(this.User.Identity.Name);
            //L'utilisateur possède un cookie d'authentification mais l'utilisateur n'existe plus, faire un logout.
            if (u == null) { return this.RedirectToAction("Logout", "Utilisateur"); }

            //Placer les données de l'utilisateur courant dans les données de la vue
            ModifierProfil mp = new ModifierProfil();
            mp.Courriel = u.Courriel;

            //Envoyer la vue à l'utilisateur
            return this.View(mp);
        }

        [HttpPost, Authorize]
        public ActionResult Edit(ModifierProfil mp) {
            //Si la vue contient des données valide, procéder aux modifications demandées par l'utilisateur
            if (this.ModelState.IsValid) {
                try {
                    //Obtenir les données de l'utilisateur courant
                    Utilisateur u = this.depot.Utilisateurs.FindByUsername(this.User.Identity.Name);

                    //Le courriel a peut-être été modifié, et est obligatoire
                    u.Courriel = mp.Courriel;

                    //Si l'utilisateur a spécifié un nouveau mot de passe, remplacer l'ancien
                    if (!string.IsNullOrEmpty(mp.NouveauMotDePasse)) {
                        u.MotDePasse = mp.NouveauMotDePasse;
                    }

                    //Lancer UPDATE ds la BD 
                    this.depot.Utilisateurs.Update(u);
                    return this.RedirectToAction("Index", "Home");
                }
                catch (SqlException e) {
                    this.ModelState.AddModelError("", e.Message);
                }
            }
            return this.View(mp);
        }

        [HttpGet, Authorize]
        public ActionResult Delete() {
            Utilisateur u = this.depot.Utilisateurs.FindByUsername(this.User.Identity.Name);
            //L'utilisateur possède un cookie d'authentification mais l'utilisateur n'existe plus, faire un logout.
            if (u == null) { return this.RedirectToAction("Logout", "Utilisateur"); }
            return this.View(u);
        }

        [HttpPost, Authorize]
        public ActionResult Delete(Utilisateur ignore) {
            Utilisateur u = this.depot.Utilisateurs.FindByUsername(this.User.Identity.Name);
            try {
                this.depot.Utilisateurs.Remove(u);
                FormsAuthentication.SignOut();
                return this.RedirectToAction("Index", "Home");
            }
            catch (SqlException e) {
                this.ModelState.AddModelError("", e.Message);
            }
            return this.View(u);
        }
    }
}
