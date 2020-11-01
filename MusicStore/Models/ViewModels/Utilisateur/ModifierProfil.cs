namespace MusicStore.Models.ViewModels.Utilisateur {
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using MusicStore.Models.DAL;
    using MusicStore.Models.DataModels;

    [CustomValidation(typeof(ModifierProfil), "ValiderModifierProfil")]
    public class ModifierProfil {
        public static ValidationResult ValiderModifierProfil(ModifierProfil profil) {
            if (!string.IsNullOrEmpty(profil.NouveauMotDePasse)) {
                Depot depot = new Depot();
                Utilisateur u = depot.Utilisateurs.FindByUsername(HttpContext.Current.User.Identity.Name);
                if (u.MotDePasse != profil.AncienMotDePasse) {
                    return new ValidationResult("L'ancien mot de passe ne correspond pas au mot de passe pour ce compte.  Le mot de passe n'a pas été modifié.", new[] { "" });
                }
            }
            return ValidationResult.Success;
        }

        [StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Ancien mot de Passe", Description = "Votre ancien mot de passe.")]
        public string AncienMotDePasse { get; set; }

        [StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nouveau mot de Passe", Description = "Votre nouveau mot de passe.")]
        [Compare("ConfirmerNouveauMotDePasse")]
        public string NouveauMotDePasse { get; set; }

        [StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmer nouveau mot de Passe", Description = "Confirmer votre nouveau mot de passe.")]
        [Compare("NouveauMotDePasse")]
        public string ConfirmerNouveauMotDePasse { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Courriel", Description = "Votre adresse de courriel.")]
        public string Courriel { get; set; }
    }
}