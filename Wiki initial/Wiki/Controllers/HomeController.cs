using System;
using System.Collections.Generic;
using System.Linq;
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
         */
        // GET: Home
        public ActionResult Index(string title)
        {
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière            
            if (title != null) {
                ViewBag.Ajout = title;
                var article = unArticle.Find(title);                
                return View("Display", article);
            }           
            
            return View();
        }

        //Affiche le formulaire permettant d'ajouter un article
        [HttpGet]
        public ActionResult Ajouter(string titre) {
            ViewBag.Ajout = titre;
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière
            return View();
        }


        /*Ajoute un nouvel article.
         *Un clic sur aperçu permet de prévisualiser le contenu de l'article à ajouter 
         * 
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
                unArticle.Add(a);
                return RedirectToAction("Display", "Home", new { titre = a.Titre });
            }
            ViewBag.Ajout = a.Titre;
            return View();
        }


        //Affiche l'article à modifier
        [HttpGet]
        public ActionResult Modifier(string titre) {
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière
            return View(unArticle.Find(titre));
        }

        /*Enregistre la modification de l'article 
         *Un clic sur le bouton Aperçu permet de
         *prévisualiser la modification. 
         *Un clic sur supprimer pour confirmation avant la suppression définitive.
         */
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Modifier(string apercu, string supprimer, Article a) {
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière
            if (!String.IsNullOrEmpty(apercu)) {
                //Affiche un aperçu de la modification
                ViewBag.Contenu = a.Contenu;  
                return View(a);
            }
            else if (!String.IsNullOrEmpty(supprimer)) {
                //Affiche l'article à supprimer et demande confirmation
                return View("Supprimer", unArticle.Find(a.Titre));
                //return RedirectToAction("Supprimer", new { titre = a.Titre });

            }
            else {
                unArticle.Update(a);
                var article = unArticle.Find(a.Titre);
                return RedirectToAction("Display", "Home", new { titre = article.Titre });       
            }               
        }

        //Affiche un article
        [HttpGet]
        public ActionResult Display(string title) { 
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière
            return View(unArticle.Find(title));
        }

        //Affiche l'article à supprimer et demande la confirmation
        [HttpGet]
        public ActionResult Supprimer(string title) {  
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière
            return View(unArticle.Find(title));
        }        

        //Supprime définitivement l'article
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Supprimer( Article a) {
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière
            string str = a.Titre;            
            unArticle.Delete(a.Titre);
            return RedirectToAction("Index");
        }
    }
}
