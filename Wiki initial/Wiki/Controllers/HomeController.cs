using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Wiki.Models.Biz;
using Wiki.Models.DAL;

namespace Wiki.Controllers
{   //Auteur: Hilaire Tchakote
    public class HomeController : Controller
    {
        Articles unArticle = new Articles();

        /*Affiche la page d'accueil au démarrage. Un clic sur un article de la table de matière 
         * ou une saisie du titre dans la zone de texte affiche l'article.
         * Auteur: Hilaire Tchakote
         */
        // GET: Home
        public ActionResult Index(string Lang)
        {   
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière 
            ChangeCulture(Lang);            
            return View();
        }

        /*Affiche le formulaire permettant d'ajouter un article
         *Auteur: Hilaire Tchakote 
         */
        [HttpGet]
        public ActionResult Ajouter(string titre, string Lang) {
            ViewBag.Ajout = titre;
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière
            ChangeCulture(Lang);
            return View();
        }


        /*Ajoute un nouvel article.
         *Un clic sur aperçu permet de prévisualiser le contenu de l'article à ajouter 
         *Après ajout l'article est affiché
         *Auteur: Hilaire Tchakote
         */
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Ajouter(string apercu, Article a) {
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière
            if (!String.IsNullOrEmpty(apercu)) {                
                ViewBag.Contenu = a.Contenu;
                ViewBag.Ajout = a.Titre;
                return View();
            }
            else if (ModelState.IsValid) {
                a.IdContributeur = unArticle.FindUser(User.Identity.Name).Id;
                if (unArticle.Add(a) != -1) //Vérifie si le titre existe d`éjà dans la Bd. si oui, il n'est pas inserré.
                    return RedirectToAction("Display", "Home", new { titre = a.Titre }); 
                else
                    ViewBag.Exist = Wiki.Ressources.Home.Views.TitleExists; 
            }
            ViewBag.Ajout = a.Titre;
            return View();
        }


        /*Affiche l'article à modifier
         *Auteur: Hilaire Tchakote
         */
        [HttpGet]
        public ActionResult Modifier(string titre, string Lang) {
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière
            ViewBag.article = titre;
            ChangeCulture(Lang);
           return View(unArticle.Find(titre));
                          
        }

        /*Enregistre la modification de l'article 
         *Un clic sur le bouton Aperçu permet de
         *prévisualiser la modification. 
         *Un clic sur supprimer pour confirmation avant la suppression définitive.
         *Auteur: Hilaire Tchakote
         */
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Modifier(string apercu, string supprimer, Article a) {
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière
            if (!String.IsNullOrEmpty(apercu)) {
                //Affiche un aperçu du contenu modifié
                ViewBag.Contenu = a.Contenu;  
                return View(a);
            }
            else if (!String.IsNullOrEmpty(supprimer)) {
                //Affiche l'article à supprimer et demande confirmation
                return RedirectToAction("Supprimer","Home", new{titre=a.Titre});
            }
            else {
                //Mis à jour de la modification et affichage dudit article
                a.IdContributeur = unArticle.FindUser(User.Identity.Name).Id;
                unArticle.Update(a);
                var article = unArticle.Find(a.Titre);
                return RedirectToAction("Display", "Home", new { titre = article.Titre });        
            }               
        }

        /*Affiche l'article dont le titre est passé en paramètre
         *Auteur: Hilaire Tchakote
         */
        [HttpGet]
        public ActionResult Display(string titre, string Lang) { 
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière
            ChangeCulture(Lang);
            ViewBag.article = titre;
            return View(unArticle.Find(titre));
        }



        [HttpGet]
        public ActionResult Supprimer(string titre, string Lang) {
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière
            ViewBag.article = titre;
            ChangeCulture(Lang);
            return View(unArticle.Find(titre));
        }

        /*Supprime définitivement dont le titre est passé en paramètre
         *l'article et redirection à la page d'accueil
         *Auteur: Hilaire Tchakote 
         */
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Supprimer(string titre) { 
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière             
            unArticle.Delete(titre);
            return RedirectToAction("Index");
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
                Response.Cookies.Add(cookie);
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Lang);
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            }
        }
    }
}
