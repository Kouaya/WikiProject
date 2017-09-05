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
                cmd.Parameters.Add("@IdContributeur", SqlDbType.Int).Value = 1;                 
                
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
                cmd.Parameters.Add("@IdContributeur", SqlDbType.Int).Value = 1;                 
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

        private string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString; }
        }

    }
}