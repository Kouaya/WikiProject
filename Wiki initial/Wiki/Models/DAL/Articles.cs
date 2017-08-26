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

namespace Wiki.Models.DAL {
    public class Articles {

        // Auteurs:Kouaya Carles Romuald
        public int Add(Article a) {
            string cStr = ConfigurationManager.ConnectionStrings["WikiCon"].ConnectionString;
            using(SqlConnection cnx = new SqlConnection(cStr)) {
                string requete = "AddArticle";                   // Stored procedures
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                try {
                    cnx.Open();
                    cmd.Parameters.Add("Titre", SqlDbType.NVarChar).Value = a.Titre;
                    cmd.Parameters.Add("Contenu", SqlDbType.NVarChar).Value = a.Contenu;
                    //cmd.Parameters.Add("IdContributeur", SqlDbType.Int).Value = a.IdContributeur;
                    cmd.Parameters.Add("IdContributeur", SqlDbType.Int).Value = 1;

                    return cmd.ExecuteNonQuery(); ;
                }

                catch(Exception e) {
                    throw new Exception(e.Message);
                }

                finally {
                    cnx.Close();
                }
            }

        }


        // Auteurs:Kouaya Carles Romuald
        public Article Find(string titre) {
            string cStr = ConfigurationManager.ConnectionStrings["WikiCon"].ConnectionString;
            Article article = new Article();
            using(SqlConnection cnx = new SqlConnection(cStr)) {
                string requete = "FindArticle";                   // Stored procedures
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                try {
                    cnx.Open();
                    cmd.Parameters.Add("Titre", SqlDbType.NVarChar).Value = titre;
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    dataReader.Read();
                    article.Titre = (string)dataReader["Titre"];
                    article.Contenu = (string)dataReader["Contenu"];
                    article.Revision = (int)dataReader["Revision"];
                    article.IdContributeur = (int)dataReader["IdContributeur"];
                    article.DateModification = (DateTime)dataReader["DateModification"];

                    dataReader.Close();

                    return article;
                }

                    catch(Exception e) {
                    string message=e.Message;
                    return null;
                }

                finally {
                    cnx.Close();
                }
            }
        }


        // Auteurs: Vincent Simard, Phan Ngoc Long Denis, Floyd Ducharme, Pierre-Olivier Morin
        public IList<string> GetTitres() {
            string cStr = ConfigurationManager.ConnectionStrings["WikiCon"].ConnectionString;
            using(SqlConnection cnx = new SqlConnection(cStr)) {
                string requete = "GetTitresArticles";                   // Stored procedures
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                try {
                    cnx.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    IList<string> ListeTitre = new List<string>();
                    while(dataReader.Read()) {
                        string t = (string)dataReader["Titre"];
                        ListeTitre.Add(t);
                    }
                    dataReader.Close();

                    return ListeTitre;
                }

                finally {
                    cnx.Close();
                }
            }
        }

        // Auteurs: Alexandre, Vincent, William et Nicolas
        public IList<Article> GetArticles() {
            using(var conn = new SqlConnection(ConnectionString)) {
                conn.Open();

                var cmd = new SqlCommand("GetArticles", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                try {
                    var dataReader = cmd.ExecuteReader();
                    var articles = new List<Article>();

                    while(dataReader.Read()) {
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
                catch {
                    return null;
                }
            }
        }


        // Auteurs:Kouaya Carles Romuald
        public int Update(Article a) {

            string cStr = ConfigurationManager.ConnectionStrings["WikiCon"].ConnectionString;
            using(SqlConnection cnx = new SqlConnection(cStr)) {
                string requete = "UpdateArticle";                   // Stored procedures
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                try {
                    cnx.Open();
                    cmd.Parameters.Add("Titre", SqlDbType.NVarChar).Value = a.Titre;
                    cmd.Parameters.Add("Contenu", SqlDbType.NVarChar).Value = a.Contenu;
                    cmd.Parameters.Add("IdContributeur", SqlDbType.Int).Value = a.IdContributeur;

                    return cmd.ExecuteNonQuery(); ;
                }

                catch(Exception e) {
                    throw new Exception(e.Message);
                }

                finally {
                    cnx.Close();
                }
            }

        }



        // Auteurs:Kouaya Carles Romuald
        public int Delete(string titre) {
            string cStr = ConfigurationManager.ConnectionStrings["WikiCon"].ConnectionString;
            using(SqlConnection cnx = new SqlConnection(cStr)) {
                string requete = "DeleteArticle";                   // Stored procedures
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                try {
                    cnx.Open();
                    cmd.Parameters.Add("Titre", SqlDbType.NVarChar).Value = titre;

                    return cmd.ExecuteNonQuery(); ;
                }

                catch(Exception e) {
                    throw new Exception(e.Message);
                }

                finally {
                    cnx.Close();
                }
            }

        }


        private string ConnectionString {
            get { return ConfigurationManager.ConnectionStrings["WikiCon"].ConnectionString; }
        }

    }
}