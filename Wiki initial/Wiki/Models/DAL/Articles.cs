using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Wiki.Models.Biz;

namespace Wiki.Models.DAL
{
    public static class Articles
    {
        // Auteurs: Ren S. Roy
        public static int Add(Article a)
        {
            String comm = ConnectionString;
            int IdArticle;

            using (SqlConnection cnx = new SqlConnection(comm))
            {
                cnx.Open();

                //Création d'une commande
                SqlCommand commande = new SqlCommand("AddArticle", cnx);
                commande.CommandType = CommandType.StoredProcedure;
                commande.Parameters.Add("Titre", SqlDbType.NVarChar).SqlValue = a.Titre;
                commande.Parameters.Add("Contenu", SqlDbType.NChar).SqlValue = a.Contenu;
                commande.Parameters.Add("Revision", SqlDbType.Int).SqlValue = a.Revision;
                commande.Parameters.Add("IdContributeur", SqlDbType.Int).SqlValue = a.IdContributeur;

                try
                {
                    IdArticle = (int)commande.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    IdArticle = -1;
                }
            }
            return IdArticle;
        }

        // Auteurs: Ren S. Roy
        public static Article Find(string titre)
        {
            using (SqlConnection cnx = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("FindArticle", cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("Titre", SqlDbType.NVarChar).Value = titre;
                try
                {
                    cnx.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    Article a = new Article();


                    while (dataReader.Read())
                    {

                        a.Titre = (string)dataReader["Titre"];
                        a.Contenu = (string)dataReader["Contenu"];
                        a.Revision = (int)dataReader["Revision"];
                        a.IdContributeur = (int)dataReader["IdContributeur"];
                        a.DateModification = (DateTime)dataReader["DateModification"];

                    }
                    dataReader.Close();
                    return a;
                }
                finally
                {
                    cnx.Close();
                }

            }
        }


        // Auteurs: EXISTING
        public static IList<string> GetTitres()
        {
            //string cStr = ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString;
            using (SqlConnection cnx = new SqlConnection(ConnectionString))
            {
                string requete = "GetTitresArticles"; //StoredProc.
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cnx.Open();

                try
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    IList<string> ListeTitre = new List<string>();
                    while (dataReader.Read())
                    {
                        string t = (string)dataReader["Titre"];
                        ListeTitre.Add(t);
                    }
                    dataReader.Close();

                    return ListeTitre;
                }
                finally
                {
                    cnx.Close();
                }
            }
        }

        // Auteurs: EXISTING
        public static IList<Article> GetArticles()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand("GetArticles", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

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

        // Auteurs: Ren S. Roy
        public static int Update(Article a)
        {
            int modifiedLine = 0;

            using (SqlConnection connect = new SqlConnection(ConnectionString))
            {
                connect.Open();

                SqlCommand command = new SqlCommand("UpdateArticle", connect);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("Titre", SqlDbType.NVarChar).SqlValue = a.Titre;
                command.Parameters.Add("Contenu", SqlDbType.NVarChar).SqlValue = a.Contenu;
                command.Parameters.Add("IdContributeur", SqlDbType.Int).SqlValue = a.IdContributeur;

                try
                {
                    modifiedLine = command.ExecuteNonQuery();
                    return modifiedLine;
                }
                catch (Exception e)
                {
                    return modifiedLine;
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        // Auteurs: Ren S. Roy
        public static int Delete(string titre)
        {
            using (SqlConnection conx = new SqlConnection(ConnectionString))
            {


                SqlCommand cmd = new SqlCommand("DeleteArticle", conx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("Titre", SqlDbType.NVarChar).Value = titre;
                cmd.Connection = conx;

                conx.Open();
                int x = cmd.ExecuteNonQuery();
                conx.Close();

                return x;
            }
        }


        private static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString; }
        }

    }
}