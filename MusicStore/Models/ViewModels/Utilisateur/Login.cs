namespace MusicStore.Models.ViewModels.Utilisateur {
    using System.ComponentModel.DataAnnotations;
    using MusicStore.Models.DAL;
    using MusicStore.Models.DataModels;

    [CustomValidation(typeof(Login), "ValiderLogin")]
    public class Login {
        public static ValidationResult ValiderLogin(Login login) {
            Depot depot = new Depot();
            Utilisateur u = depot.Utilisateurs.FindByUsername(login.NomUtilisateur);
            if (u == null) { return new ValidationResult("Login inexistant"); }
            if (u.MotDePasse != login.MotDePasse) { return new ValidationResult("Login invalide"); }
            return ValidationResult.Success;
        }

        [Required]
        [Display(Name = "Nom d'utilisateur", Description = "Votre nom d'utilisateur.")]
        public string NomUtilisateur { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de Passe", Description = "Votre mot de passe.")]
        public string MotDePasse { get; set; }

        [Required]
        [Display(Name = "Resté connecté?")]
        public bool ResterConnecté { get; set; }
    }
}