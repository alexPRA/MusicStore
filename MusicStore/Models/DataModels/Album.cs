namespace MusicStore.Models.DataModels {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [CustomValidation(typeof(Album), "ValiderAlbum")]
    public class Album {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AlbumId { get; set; }
        
        [Required,MaxLength(100)]
        public string Titre { get; set; }

        [Required]
        public int AnneeParution { get; set; }

        [Required,DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public int GenreId { get; set; }

        [Required, MaxLength(50)]
        public string Artiste { get; set; }

        public double Prix { get; set; }

        [NotMapped]
        public string Cover { get => $"/Content/Images/Albums/{AlbumId}.jpg"; }

        public static ValidationResult ValiderAlbum(Album album)
        {
            if (album.AnneeParution <= 1930 && album.AnneeParution >= 2100)
                return new ValidationResult("La annee de parution doit etre comprise entre 1930 et 2100");
            if (album.Prix < 10 && album.Prix > 100)
                return new ValidationResult("Le prix doit etre compris entre 10 et 100 dollars");
            return ValidationResult.Success;
        }
    }
}