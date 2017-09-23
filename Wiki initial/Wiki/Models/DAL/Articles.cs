using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Collections.Generic;
using Wiki.Models.DAL;
using Wiki.Models.Biz;

namespace Wiki.Models.DAL
{
    public class Articles
    {
        // Auteur: Hilaire Tchakote
        public int Add(Article a)
        {   
            
            int nbEnregistrement;            
            using (SqlConnection cnx = new SqlConnection(ConnectionString)) {                
                string requete = "AddArticle";                   // Stored procedures
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Titre", SqlDbType.NVarChar, 100).Value = a.Titre;
                cmd.Parameters.Add("@contenu", SqlDbType.NVarChar, 500).Value = a.Contenu;
                cmd.Parameters.Add("@IdContributeur", SqlDbType.Int).Value = a.IdContributeur;                 
                
                try {
                    cnx.Open();
                    nbEnregistrement = cmd.ExecuteNonQuery();                    
                }
                finally {
                    cnx.Close();
                }
            }
            return nbEnregistrement;
        }

        // Auteur: hilaire Tchakote
        public Article Find(string titre)
        {
            Article unArticle = new Article();            
            using (SqlConnection cnx = new SqlConnection(ConnectionString)) {
                string requete = "FindArticle";                   // Stored procedures                
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Titre", SqlDbType.NVarChar, 100).Value = titre;
                try {
                   
                    cnx.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if(dataReader.Read()){
                        unArticle.Titre = (string)dataReader["Titre"];
                        unArticle.Revision = (int)dataReader["Revision"];
                        unArticle.IdContributeur = (int)dataReader["IdContributeur"];
                        unArticle.Contenu = (string)dataReader["Contenu"];
                        unArticle.DateModification = (DateTime)dataReader["DateModification"];
                    }
                    dataReader.Close();                   
                }
                finally {
                    cnx.Close();
                }
            }
            return unArticle;
        }


        // Auteurs: Vincent Simard, Phan Ngoc Long Denis, Floyd Ducharme, Pierre-Olivier Morin, Hilaire Tchakote
        public IList<string> GetTitres()
        {            
            using (SqlConnection cnx = new SqlConnection(ConnectionString))
            {
                string requete = "GetTitresArticles";                   // Stored procedures
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                try
                {
                    cnx.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    List<string> ListeTitre = new List<string>();
                    while (dataReader.Read())
                    {
                        string t = (string)dataReader["Titre"];
                        ListeTitre.Add(t);
                    }                   
                    dataReader.Close();
                    ListeTitre.Sort();
                    return ListeTitre;
                }
                finally
                {
                    cnx.Close();
                }
            }
        }

        // Auteurs: Alexandre, Vincent, William et Nicolas
        public IList<Article> GetArticles()
        {            
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("GetArticles", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    var dataReader = cmd.ExecuteReader();                    
                    var articles = new List<Article>();

                    while (dataReader.Read())
                    {
                        var article = new Article();

                        article.Titre = (string)dataReader["Titre"];
                        article.Contenu = (string)dataReader["Contenu"];
                        article.Revision = (int)dataReader["Revision"];
                        article.IdContributeur = (int)dataReader["IdContributeur"];
                        article.DateModification = (DateTime)dataReader["DateModification"];

                        articles.Add(article);
                    }

                    return articles;
                }
                catch
                {
                    return null;
                }
            }
        }

        // Auteurs: Hilaire Tchakote
        public int Update(Article a)
        {           
            int nbEnregistrement;            
            using (SqlConnection cnx = new SqlConnection(ConnectionString)) {                
                string requete = "UpdateArticle";                   // Stored procedures
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;                
                cmd.Parameters.Add("@Titre", SqlDbType.NVarChar, 100).Value = a.Titre;
                cmd.Parameters.Add("@contenu", SqlDbType.NVarChar, 500).Value = a.Contenu;
                cmd.Parameters.Add("@IdContributeur", SqlDbType.Int).Value = a.IdContributeur;                 
                try {
                    cnx.Open();
                    nbEnregistrement = cmd.ExecuteNonQuery();
                }
                finally {
                    cnx.Close();
                }
            }
            return nbEnregistrement;
        }

        // Auteurs: hilaire Tchakote
        public int Delete(string titre)
        {
            int nbEnregistrement;            
            using (SqlConnection cnx = new SqlConnection(ConnectionString)) {                
                string requete = "DeleteArticle";                   // Stored procedures
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Titre", SqlDbType.NVarChar, 100).Value = titre;
                try {
                    cnx.Open();
                    nbEnregistrement = cmd.ExecuteNonQuery();
                }
                finally {
                    cnx.Close();
                }
            }
            return nbEnregistrement;            
        }

        // Auteurs: hilaire Tchakote
        public int AddUser(Utilisateur user) {
            int nbEnregistrement;
            using (SqlConnection cnx = new SqlConnection(ConnectionString)) {
                string requete = "AddUtilisateur";                   // Stored procedures
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Courriel", SqlDbType.NVarChar, 50).Value = user.Courriel;
                cmd.Parameters.Add("@MDP", SqlDbType.NVarChar, 70).Value = PasswordHash.CreateHash( user.MDP);
                cmd.Parameters.Add("@Prenom", SqlDbType.NVarChar, 50).Value = user.Prenom;
                cmd.Parameters.Add("@NomFamille", SqlDbType.NVarChar, 50).Value = user.NomFamille;
                cmd.Parameters.Add("@Langue", SqlDbType.NChar, 5).Value = user.Langue;

                try {
                    cnx.Open();
                    nbEnregistrement = cmd.ExecuteNonQuery();
                }
                finally {
                    cnx.Close();
                }
            }
            return nbEnregistrement;           
        }

        // Auteurs: hilaire Tchakote
        public Utilisateur FindUser(string email) {
            Utilisateur user;
            using (SqlConnection cnx = new SqlConnection(ConnectionString)) {
                string requete = "FindUtilisateurByCourriel";                   // Stored procedures
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.Parameters.Add("@Courriel", SqlDbType.NVarChar, 50).Value = email;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                try {
                    cnx.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows) {
                        dataReader.Close();                        
                    }
                    dataReader.Read();
                    user = new Utilisateur {
                        Langue = (string)dataReader["Langue"],
                        NomFamille = (string)dataReader["NomFamille"],
                        Prenom = (string)dataReader["Prenom"],
                        Id=(int)dataReader["Id"],
                        MDP=(string)dataReader["MDP"]
                    };
                } 
                finally {
                    cnx.Close();
                }
            }
            return user;
        }

        //Hilaire Tchakote
        public int UpDateUserProfile(Utilisateur user){ 
            int nbEnregistrement;
            using (SqlConnection cnx = new SqlConnection(ConnectionString)) {
                string requete = "UpdateUtilisateur";                   // Stored procedures
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@NomFamille", SqlDbType.NVarChar, 50).Value = user.NomFamille;
                cmd.Parameters.Add("@Prenom", SqlDbType.NVarChar, 50).Value = user.Prenom;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = user.Id;               
                cmd.Parameters.Add("@Langue", SqlDbType.NChar, 5).Value = user.Langue;

                try {
                    cnx.Open();
                    nbEnregistrement = cmd.ExecuteNonQuery();
                }
                finally {
                    cnx.Close();
                }
            }
            return nbEnregistrement;            
        }

        /*
         *Modifie le mot de passe de l'utilisateur 
         * Auteur: Hilaire Tchakote
         * 
         */
        public int UpDateUserPassWord(int Id, string newPassWord) {
            int nbEnregistrement;
            using (SqlConnection cnx = new SqlConnection(ConnectionString)) {
                string requete = "UpdateMotDePasse";                   // Stored procedures
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                cmd.Parameters.Add("@nouveauMDP", SqlDbType.NVarChar, 70).Value = PasswordHash.CreateHash(newPassWord);                     

                try {
                    cnx.Open();
                    nbEnregistrement = cmd.ExecuteNonQuery();
                }
                finally {
                    cnx.Close();
                }
            }
            return nbEnregistrement;              
        }


        /*
         *Authentification de l'utilisateur 
         *param:courriel, type string
         *param:password, type string 
         *Auteur: hilaire Tchakote
         */
        public bool IsAuthentified(string courriel, string password) {
            bool isauthentified = false;            
            using (SqlConnection cnx = new SqlConnection(ConnectionString)) {
                string requete = "FindUtilisateurByCourriel";                   // Stored procedures
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.Parameters.Add("@Courriel", SqlDbType.NVarChar, 50).Value = courriel;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                try {
                    cnx.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows) {
                        dataReader.Close();
                        return isauthentified;
                    }
                    dataReader.Read();
                    var hashPassword = (string)dataReader["MDP"];
                    isauthentified = PasswordHash.ValidatePassword(password, hashPassword);                    
                }
                finally {
                    cnx.Close();
                }
            }
            return isauthentified;
        }

        private string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString; }
        }

    }
}