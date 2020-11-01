namespace MusicStore.Models.DAL
{
    using MusicStore.Models.DataModels;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;

    public class DALAlbum
    {
        protected string ChaineConnexion = ConfigurationManager.ConnectionStrings["MusicStore"].ConnectionString;
        protected const string ALBUM_INSERT = @"INSERT INTO Album(Titre,AnneeParution,Description,Artiste,Prix,GenreId) OUTPUT INSERTED.AlbumId VALUES(@Titre,@AnneeParution,@Description,@Artiste,@Prix,@GenreId);";
        protected const string ALBUM_DELETE = @"DELETE Album WHERE AlbumId=@AlbumId";
        protected const string ALBUM_UPDATE = @"UPDATE Album SET Titre = @TITRE,AnneeParution = @AnneeParution,Description = @Description, Artiste = @Artiste, Prix = @Prix, GenreId = @GenreId WHERE AlbumId=@AlbumId";
        protected const string ALBUM_SELECTALL = @"SELECT * FROM Album";
        protected const string ALBUM_FINDBYID = @"SELECT * FROM Album WHERE AlbumId=@AlbumId";
        protected const string ALBUM_FINDBYTITRE = @"SELECT * FROM Album WHERE Titre=@Titre";

        public void Add(Album u)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                SqlCommand command = new SqlCommand(ALBUM_INSERT, connection);
                command.Parameters.AddWithValue("Titre", u.Titre);
                command.Parameters.AddWithValue("AnneeParution", u.AnneeParution);
                command.Parameters.AddWithValue("Description", u.Description);
                command.Parameters.AddWithValue("Artiste", u.Artiste);
                command.Parameters.AddWithValue("Prix", u.Prix);
                command.Parameters.AddWithValue("GenreId", u.GenreId);
                connection.Open();
                u.AlbumId = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Remove(Album u) { this.Remove(u.AlbumId); }
        public void Remove(int AlbumId)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                SqlCommand command = new SqlCommand(ALBUM_DELETE, connection);
                command.Parameters.AddWithValue("AlbumId", AlbumId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Album entity)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                SqlCommand command = new SqlCommand(ALBUM_UPDATE, connection);
                command.Parameters.AddWithValue("AlbumId", entity.AlbumId);
                command.Parameters.AddWithValue("Titre", entity.Titre);
                command.Parameters.AddWithValue("AnneeParution", entity.AnneeParution);
                command.Parameters.AddWithValue("Description", entity.Description);
                command.Parameters.AddWithValue("Artiste", entity.Artiste);
                command.Parameters.AddWithValue("Prix", entity.Prix);
                command.Parameters.AddWithValue("GenreId", entity.GenreId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public Album Find(int AlbumId)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(ALBUM_FINDBYID, connection);
                command.Parameters.AddWithValue("AlbumId", AlbumId);
                SqlDataReader dr = command.ExecuteReader();
                Album u = null;
                if (dr.Read())
                {
                    u = new Album();

                    u.AlbumId = (int)dr["AlbumId"];
                    u.Titre = dr["Titre"].ToString();
                    u.AnneeParution = (int)dr["AnneeParution"];
                    u.Description = dr["Description"].ToString();
                    u.Artiste = dr["Artiste"].ToString();
                    u.Prix = Convert.ToDouble(dr["Prix"]);
                    u.GenreId = (int)dr["GenreId"];

                }
                dr.Close();
                return u;
            }
        }

        public Album FindByName(string nomAlbum)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(ALBUM_FINDBYTITRE, connection);
                command.Parameters.AddWithValue("Nom", nomAlbum);
                SqlDataReader dr = command.ExecuteReader();
                Album u = null;
                if (dr.Read())
                {
                    u = new Album();

                    u.AlbumId = (int)dr["AlbumId"];
                    u.Titre = dr["Titre"].ToString();
                    u.AnneeParution = (int)dr["AnneeParution"];
                    u.Description = dr["Description"].ToString();
                    u.Artiste = dr["Artiste"].ToString();
                    u.Prix = Convert.ToDouble(dr["Prix"]);
                    u.GenreId = (int)dr["GenreId"];

                }
                dr.Close();
                return u;
            }
        }

        public List<Album> List()
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(ALBUM_SELECTALL, connection);
                SqlDataReader dr = command.ExecuteReader();
                List<Album> album = new List<Album>();
                Album u = null;
                while (dr.Read())
                {
                    u = new Album();

                    u.AlbumId = (int)dr["AlbumId"];
                    u.Titre = dr["Titre"].ToString();
                    u.AnneeParution = (int)dr["AnneeParution"];
                    u.Description = dr["Description"].ToString();
                    u.GenreId = (int)dr["GenreId"];
                    u.Artiste = dr["Artiste"].ToString();
                    u.Prix = Convert.ToDouble(dr["Prix"]);
                    album.Add(u);
                }
                dr.Close();
                return album;
            }
        }
    }
}