namespace MusicStore.Models.DataModels {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Utilisateur {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UtilisateurId { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 4)]
        [RegularExpression(@"^[A-Za-z]{1,1}[A-Za-z0-9]{3,14}$", ErrorMessage = "Le nom d'utilisateur doit ocmmencer par une lettre, comprendre entre 4 et 15 caractères et être composés de lettres ou de chiffres uniquement.")]
        [Display(Name = "Nom d'utilisateur", Description = "Votre nom d'utilisateur.")]
        public string NomUtilisateur { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de Passe", Description = "Votre mot de passe.")]
        public string MotDePasse { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Courriel", Description = "Votre adresse de courriel.")]
        public string Courriel { get; set; }
    }
}