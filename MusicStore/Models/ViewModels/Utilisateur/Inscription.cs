namespace MusicStore.Models.ViewModels.Utilisateur {
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    [CustomValidation(typeof(Inscription), "ValiderInscription")]
    public class Inscription {
        public static ValidationResult ValiderInscription(Inscription i) {
            if (!i.IAgree) { return new ValidationResult("YOU MUST AGREE!"); }
            return ValidationResult.Success;
        }

        [Required]
        [StringLength(15, MinimumLength = 4)]
        [RegularExpression(@"^[A-Za-z]{1,1}[A-Za-z0-9]{3,14}$", ErrorMessage = "Le nom d'utilisateur doit ocmmencer par une lettre, comprendre entre 4 et 15 caractères et être composés de lettres ou de chiffres uniquement.")]
        [Display(Name = "Nom d'utilisateur", Description = "Votre nom d'utilisateur.")]
        public string NomUtilisateur { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de Passe", Description = "Votre mot de passe.")]
        [Compare("ConfirmerMotDePasse")]
        public string MotDePasse { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmer mot de Passe", Description = "Votre mot de passe.")]
        [Compare("MotDePasse")]
        public string ConfirmerMotDePasse { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Courriel", Description = "Votre adresse de courriel.")]
        public string Courriel { get; set; }

        [Required]
        [Display(Name = "J'accepte les conditions du site.")]
        public bool IAgree { get; set; }
    }
}