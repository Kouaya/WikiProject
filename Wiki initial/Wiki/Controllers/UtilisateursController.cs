using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Wiki.Models.Biz;
using Wiki.Models.DAL;
using Wiki.Models.Views;

namespace Wiki.Controllers
{
    public class UtilisateursController : Controller
    {
        Articles unArticle = new Articles();

        // GET: Utilisateur
        [HttpGet]
        public ActionResult Connexion(string returnUrl, string Lang)  
        {
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière  
            ChangeCulture(Lang);
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        /*
         *Affiche la page  d'authentification de l'utilisateur
         *Si l'utilisateur est authenfié, il est    
         *est redirigé vers l'article qu'il consultait
         *sinon, la page est réaffichée
         */
        [HttpPost]
        [ValidateAntiForgeryToken]       
        public ActionResult Connexion(string courriel, string MDP, string str) {          
                if (unArticle.IsAuthentified(courriel, MDP)) {
                    FormsAuthentication.SetAuthCookie(courriel, false);
                }
                else {
                    ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière      
                    ViewBag.error = Wiki.Ressources.Shared.Error.ErrorLogIn;
                    return View();
                }
                return RedirectToAction("Index", "Home");            
        }

        /*
         *View permettant à l'utilisateur de s'inscrire 
         *Affiche un fornulaire pour inscription 
         */
        [HttpGet]        
        public ActionResult Inscription(string Lang) {
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière
            ChangeCulture(Lang); 
            return View();
        }

        /*
         * Affiche la page d'inscription de l'utilisateur
         * Une fois le formulaire d'inscription bien rempli,
         * l'utilisateur est ajouté à la base de données.
         * 
         */
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Inscription(Utilisateur user) {
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière   
            user.Langue = Request.Form["Language"].ToString();
            unArticle.AddUser(user);            
            return View("Connexion");
        }


        /*
         *Affiche la page de profil 
         *l'utilisateur peut changer  
         *changer:
         *Son nom, prenom et salangue
         */
        [HttpGet]
        public ActionResult Profil(string Lang) {
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière 
            ChangeCulture(Lang);
            if (Lang != null)
                ViewBag.Cookie = Lang;
            return View(unArticle.FindUser(User.Identity.Name));
        }


        /*
         *Mise à jour du profil de l'utilisateur dans la base de donnée 
         *et retour à la page d'accueil 
         * 
         */
        [HttpPost]
        public ActionResult Profil(Utilisateur user) {
            user.Langue = Request.Form["Language"];            
            int nbEnreg = unArticle.UpDateUserProfile(user);
            //return RedirectToAction("Index", "Home");
            return RedirectToAction("Profil", new { Lang = user.Langue });
        }

         
        /*
         *Affiche la page de modification du mot de passe 
         *de l'utilisateur 
         * 
         */
        [HttpGet]
        public ActionResult ModifierMdp(string Lang) {
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière  
            UtilisateurModificationMdp userPw = new UtilisateurModificationMdp();
            ChangeCulture(Lang);
            return View(userPw);
        }


        [HttpPost]
        public ActionResult ModifierMdp(UtilisateurModificationMdp u) {
            Utilisateur user = unArticle.FindUser(User.Identity.Name);            
            if(!PasswordHash.ValidatePassword(u.AMDP, user.MDP)){
               ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière
               ViewBag.PwError = Wiki.Ressources.Utilisateur.Utilisateur.PassWordError;
               return View();
            }else{                
                unArticle.UpDateUserPassWord(user.Id, u.NMDP);
            }            

            return RedirectToAction("Index", "Home");
        }

        /*
         *Déconnexion de l'utilisateur 
         *et retour à la page d'accueil 
         * 
         */
        [HttpGet]
        public ActionResult Deconnexion() {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        /*
         *Gestion de la langue d'affichage 
         * 
         * 
         */
        public void ChangeCulture(string Lang) {
            if (Lang != null) {
                HttpCookie cookie = new HttpCookie("_culture");
                cookie.Value = Lang;
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(cookie);
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Lang);
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            }
        }
        
    }
}