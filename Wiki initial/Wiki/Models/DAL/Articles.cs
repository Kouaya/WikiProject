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
        // Auteurs: Hilaire Tchakote
        public int Add(Article a)
        {
            int nbEnregistrement;            
            using (SqlConnection cnx = new SqlConnection(ConnectionString)) {
                string requete = "INSERT INTO Articles (Titre, Contenu, Revision, IdContributeur, DateModification) VALUES('" + a.Titre + "','" + a.Contenu + "', " + a.Revision + ", " + a.IdContributeur + ",'" + a.DateModification + "')";
                //string requete = "GetTitresArticles";                   // Stored procedures
                SqlCommand cmd = new SqlCommand(requete, cnx);
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandType = System.Data.CommandType.Text;
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

        // Auteurs:
        public Article Find(string titre)
        {
            Article unArticle = new Article();            
            using (SqlConnection cnx = new SqlConnection(ConnectionString)) {
                //string requete = "GetTitresArticles";                   // Stored procedures
                string requete = "SELECT * FROM Articles WHERE Titre='"+titre+"'";
                SqlCommand cmd = new SqlCommand(requete, cnx);
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandType = System.Data.CommandType.Text;
                try {
                   
                    cnx.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    dataReader.Read();
                    unArticle.Titre = (string)dataReader["Titre"];
                    unArticle.Revision = (int)dataReader["Revision"];
                    unArticle.IdContributeur = (int)dataReader["IdContributeur"];
                    unArticle.Contenu = (string)dataReader["Contenu"];
                    unArticle.DateModification = (DateTime)dataReader["DateModification"];
                    dataReader.Close();                   
                }
                finally {
                    cnx.Close();
                }
            }
            return unArticle;
        }


        // Auteurs: Vincent Simard, Phan Ngoc Long Denis, Floyd Ducharme, Pierre-Olivier Morin
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
            int revision = Find(a.Titre).Revision;//recupération du no de révision de l'article
            int nbRevision = revision + 1;//incrémentation du nombre de révision
            int nbEnregistrement;            
            using (SqlConnection cnx = new SqlConnection(ConnectionString)) {
                string requete = "UPDATE Article SET Contenu='" + a.Contenu + "', Revision=" + nbRevision + " WHERE Titre="+a.Titre;
                //string requete = "GetTitresArticles";                   // Stored procedures
                SqlCommand cmd = new SqlCommand(requete, cnx);
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandType = System.Data.CommandType.Text;
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
                string requete = "DELETE FROM Articles WHERE Titre='"+titre+"'";
                //string requete = "GetTitresArticles";                   // Stored procedures
                SqlCommand cmd = new SqlCommand(requete, cnx);
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandType = System.Data.CommandType.Text;
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
            get { return ConfigurationManager.ConnectionStrings["WikiCon"].ConnectionString; }
        }

    }
}