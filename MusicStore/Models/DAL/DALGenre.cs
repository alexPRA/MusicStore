namespace MusicStore.Models.DAL {
    using MusicStore.Models.DataModels;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;

    public class DALGenre {
        protected string ChaineConnexion = ConfigurationManager.ConnectionStrings["MusicStore"].ConnectionString;
        protected const string GENRE_INSERT = @"INSERT INTO Genre(Nom) OUTPUT INSERTED.GenreId VALUES(@Nom);";
        protected const string GENRE_DELETE = @"DELETE Genre WHERE GenreId=@GenreId";
        protected const string GENRE_UPDATE = @"UPDATE Genre SET Nom = @Nom WHERE GenreId=@GenreId";
        protected const string GENRE_SELECTALL = @"SELECT GenreId,Nom FROM Genre";
        protected const string GENRE_FINDBYID = @"SELECT GenreId,Nom FROM Genre WHERE GenreId=@GenreId";
        protected const string GENRE_FINDBYNAME = @"SELECT GenreId,Nom FROM Genre WHERE Nom=@NomGenre";

        public void Add(Genre u)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                SqlCommand command = new SqlCommand(GENRE_INSERT, connection);
                command.Parameters.AddWithValue("Nom", u.NomGenre);
                connection.Open();
                u.GenreId = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Remove(Genre u) { this.Remove(u.GenreId); }
        public void Remove(int GenreId)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                SqlCommand command = new SqlCommand(GENRE_DELETE, connection);
                command.Parameters.AddWithValue("GenreId", GenreId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Genre entity)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                SqlCommand command = new SqlCommand(GENRE_UPDATE, connection);
                command.Parameters.AddWithValue("GenreId", entity.GenreId);
                command.Parameters.AddWithValue("Nom", entity.NomGenre);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public Genre Find(int GenreId)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(GENRE_FINDBYID, connection);
                command.Parameters.AddWithValue("GenreId", GenreId);
                SqlDataReader dr = command.ExecuteReader();
                Genre u = null;
                if (dr.Read())
                {
                    u = new Genre
                    {
                        GenreId = (int)dr["GenreId"],
                        NomGenre = dr["Nom"].ToString(),
                    };
                }
                dr.Close();
                return u;
            }
        }

        public Genre FindByName(string nomGenre)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(GENRE_FINDBYNAME, connection);
                command.Parameters.AddWithValue("Nom", nomGenre);
                SqlDataReader dr = command.ExecuteReader();
                Genre u = null;
                if (dr.Read())
                {
                    u = new Genre
                    {
                        GenreId = (int)dr["GenreId"],
                        NomGenre = dr["Nom"].ToString(),
                    };
                }
                dr.Close();
                return u;
            }
        }

        public List<Genre> List()
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(GENRE_SELECTALL, connection);
                SqlDataReader dr = command.ExecuteReader();
                List<Genre> lu = new List<Genre>();
                while (dr.Read())
                {
                    lu.Add(new Genre
                    {
                        GenreId = (int)dr["GenreId"],
                        NomGenre = dr["Nom"].ToString()
                    });
                }
                dr.Close();
                return lu;
            }
        }
    }
}