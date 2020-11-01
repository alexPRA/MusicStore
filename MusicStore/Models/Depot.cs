using MusicStore.Models.DAL;

namespace MusicStore.Models {

    public class Depot {
        public DALUtilisateur Utilisateurs { get; private set; } = new DALUtilisateur();
        public DALGenre Genres { get; private set; } = new DALGenre();
        public DALAlbum Albums { get; private set; } = new DALAlbum();
        public DALPanier Paniers { get; private set; } = new DALPanier();
    }

}