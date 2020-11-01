namespace MusicStore.Models.DataModels {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Panier {
        [Key]
        public int UtilisateurId { get; set; }
        [Key]
        public int AlbumId { get; set; }
    }
}