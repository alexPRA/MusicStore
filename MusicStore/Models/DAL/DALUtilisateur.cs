namespace MusicStore.Models.DAL {
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using MusicStore.Models.DataModels;

    public class DALUtilisateur {
        protected string ChaineConnexion = ConfigurationManager.ConnectionStrings["MusicStore"].ConnectionString;
        protected const string UTILISATEUR_INSERT = @"INSERT INTO Utilisateur(NomUtilisateur,MotDePasse,Courriel) OUTPUT INSERTED.UtilisateurId VALUES(@NomUtilisateur,@MotDePasse,@Courriel);";
        protected const string UTILISATEUR_DELETE = @"DELETE Utilisateur WHERE UtilisateurId=@UtilisateurId";
        protected const string UTILISATEUR_UPDATE = @"UPDATE Utilisateur SET NomUtilisateur=@NomUtilisateur,MotDePasse=@MotDePasse,Courriel=@Courriel WHERE UtilisateurId=@UtilisateurId";
        protected const string UTILISATEUR_SELECTALL = @"SELECT UtilisateurId,NomUtilisateur,MotDePasse,Courriel FROM Utilisateur";
        protected const string UTILISATEUR_FINDBYID = @"SELECT UtilisateurId,NomUtilisateur,MotDePasse,Courriel FROM Utilisateur WHERE UtilisateurId=@UtilisateurId";
        protected const string UTILISATEUR_FINDBYUSERNAME = @"SELECT UtilisateurId,NomUtilisateur,MotDePasse,Courriel FROM Utilisateur WHERE NomUtilisateur=@NomUtilisateur";
        protected const string UTILISATEUR_FINDBYEMAIL = @"SELECT UtilisateurId,NomUtilisateur,MotDePasse,Courriel FROM Utilisateur WHERE Courriel=@Courriel";

        public void Add(Utilisateur u) {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion)) {
                SqlCommand command = new SqlCommand(UTILISATEUR_INSERT, connection);
                command.Parameters.AddWithValue("NomUtilisateur", u.NomUtilisateur);
                command.Parameters.AddWithValue("MotDePasse", u.MotDePasse);
                command.Parameters.AddWithValue("Courriel", u.Courriel);
                connection.Open();
                u.UtilisateurId = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Remove(Utilisateur u) { this.Remove(u.UtilisateurId); }
        public void Remove(int UtilisateurId) {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion)) {
                SqlCommand command = new SqlCommand(UTILISATEUR_DELETE, connection);
                command.Parameters.AddWithValue("UtilisateurId", UtilisateurId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Utilisateur entity) {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion)) {
                SqlCommand command = new SqlCommand(UTILISATEUR_UPDATE, connection);
                command.Parameters.AddWithValue("UtilisateurId", entity.UtilisateurId);
                command.Parameters.AddWithValue("NomUtilisateur", entity.NomUtilisateur);
                command.Parameters.AddWithValue("MotDePasse", entity.MotDePasse);
                command.Parameters.AddWithValue("Courriel", entity.Courriel);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public Utilisateur Find(int UtilisateurId) {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion)) {
                connection.Open();
                SqlCommand command = new SqlCommand(UTILISATEUR_FINDBYID, connection);
                command.Parameters.AddWithValue("UtilisateurId", UtilisateurId);
                SqlDataReader dr = command.ExecuteReader();
                Utilisateur u = null;
                if (dr.Read()) {
                    u = new Utilisateur {
                        UtilisateurId = (int)dr["UtilisateurId"],
                        NomUtilisateur = dr["NomUtilisateur"].ToString(),
                        MotDePasse = dr["MotDePasse"].ToString(),
                        Courriel = dr["Courriel"].ToString()
                    };
                }
                dr.Close();
                return u;
            }
        }

        public Utilisateur FindByEmail(string courriel) {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion)) {
                connection.Open();
                SqlCommand command = new SqlCommand(UTILISATEUR_FINDBYEMAIL, connection);
                command.Parameters.AddWithValue("Courriel", courriel);
                SqlDataReader dr = command.ExecuteReader();
                Utilisateur u = null;
                if (dr.Read()) {
                    u = new Utilisateur {
                        UtilisateurId = (int)dr["UtilisateurId"],
                        NomUtilisateur = dr["NomUtilisateur"].ToString(),
                        MotDePasse = dr["MotDePasse"].ToString(),
                        Courriel = dr["Courriel"].ToString()
                    };
                }
                dr.Close();
                return u;
            }
        }

        public Utilisateur FindByUsername(string nomUtilisateur) {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion)) {
                connection.Open();
                SqlCommand command = new SqlCommand(UTILISATEUR_FINDBYUSERNAME, connection);
                command.Parameters.AddWithValue("NomUtilisateur", nomUtilisateur);
                SqlDataReader dr = command.ExecuteReader();
                Utilisateur u = null;
                if (dr.Read()) {
                    u = new Utilisateur {
                        UtilisateurId = (int)dr["UtilisateurId"],
                        NomUtilisateur = dr["NomUtilisateur"].ToString(),
                        MotDePasse = dr["MotDePasse"].ToString(),
                        Courriel = dr["Courriel"].ToString()
                    };
                }
                dr.Close();
                return u;
            }
        }

        public List<Utilisateur> List() {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion)) {
                connection.Open();
                SqlCommand command = new SqlCommand(UTILISATEUR_SELECTALL, connection);
                SqlDataReader dr = command.ExecuteReader();
                List<Utilisateur> lu = new List<Utilisateur>();
                while (dr.Read()) {
                    lu.Add(new Utilisateur {
                        UtilisateurId = (int)dr["UtilisateurId"],
                        NomUtilisateur = dr["NomUtilisateur"].ToString(),
                        MotDePasse = dr["MotDePasse"].ToString(),
                        Courriel = dr["Courriel"].ToString()
                    });
                }
                dr.Close();
                return lu;
            }
        }
    }
}