namespace MusicStore.Models.DAL
{
    using MusicStore.Models.DataModels;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;

    public class DALPanier
    {
        protected string ChaineConnexion = ConfigurationManager.ConnectionStrings["MusicStore"].ConnectionString;
        protected const string Panier_AJOUTER_UN_ARTICLE = @"INSERT INTO Panier(UtilisateurId,AlbumId) VALUES(@UtilisateurId,@AlbumId);";
        protected const string Panier_SUPPRIMER_UN_ARTICLE = @"DELETE Panier WHERE AlbumId=@AlbumId AND UtilisateurId=@UtilisateurId;";
        protected const string Panier_VIDER_LE_PANIER = @"DELETE Panier WHERE UtilisateurId=@UtilisateurId;";
        protected const string Panier_ARTICLES_DE_LUTILISATEUR = @"SELECT Album.AlbumId,Album.Titre,Album.AnneeParution,Album.Description,Album.GenreId,Album.Artiste,Album.Prix FROM Album INNER JOIN Panier ON Album.AlbumId = Panier.AlbumId WHERE UtilisateurId=@UtilisateurId;"
       ;
        public void AjouterUnArticle(int AlbumId, int UtilisateurId)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                SqlCommand command = new SqlCommand(Panier_AJOUTER_UN_ARTICLE, connection);
                command.Parameters.AddWithValue("AlbumId", AlbumId);
                command.Parameters.AddWithValue("UtilisateurId", UtilisateurId);
                connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    //Ignorer l'ajout d'un item en double
                }
            }
        }
        public void SupprimerUnArticle(int AlbumId, int UtilisateurId)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                SqlCommand command = new SqlCommand(Panier_SUPPRIMER_UN_ARTICLE, connection);
                command.Parameters.AddWithValue("AlbumId", AlbumId);
                command.Parameters.AddWithValue("UtilisateurId", UtilisateurId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void ViderLePanier(int UtilisateurId)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                SqlCommand command = new SqlCommand(Panier_VIDER_LE_PANIER, connection);
                command.Parameters.AddWithValue("UtilisateurId", UtilisateurId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public List<Album> TousLesArticles(int UtilisateurId)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(Panier_ARTICLES_DE_LUTILISATEUR, connection);
                command.Parameters.AddWithValue("UtilisateurId", UtilisateurId);
                SqlDataReader dr = command.ExecuteReader();
                List<Album> la = new List<Album>();
                while (dr.Read())
                {
                    var p = new Album();
                    p.AlbumId = (int)dr["AlbumId"];
                    p.Titre = dr["Titre"].ToString();
                    p.AnneeParution = (int)dr["AnneeParution"];
                    p.Description = dr["Description"].ToString();
                    p.Artiste = dr["Artiste"].ToString();
                    p.Prix = Convert.ToDouble(dr["Prix"]);
                    p.GenreId = (int)dr["GenreId"];
                    la.Add(p);
                }
                dr.Close();
                return la;
            }
        }
    }
}