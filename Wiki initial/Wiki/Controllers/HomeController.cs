using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wiki.Models.Biz;
using Wiki.Models.DAL;

namespace Wiki.Controllers
{
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
                var article = unArticle.Find(title);                
                return View("Display", article);
            }           
            
            return View();
        }

        //Affiche le formulaire permettant d'ajouter un article
        [HttpGet]
        public ActionResult Ajouter() {
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Ajouter(string apercu, Article a) {
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière
            if (!String.IsNullOrEmpty(apercu)) {                
                ViewBag.Contenu = a.Contenu;
                return View();
            }
            else {
                unArticle.Add(a);
                return RedirectToAction("Display", "Home", new { titre = a.Titre });
            }
        }

        [HttpGet]
        public ActionResult Modifier(string titre) {
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière
            return View(unArticle.Find(titre));
        }

        /*Enregistre la modification de l'article 
         *Un clic sur le bouton Aperçu permet de
         *prévisualiser la modification. 
         * 
         */
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Modifier(string apercu, Article a) {
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière
            if (!String.IsNullOrEmpty(apercu)) {
                //Affiche un aperçu de la modification
                ViewBag.Contenu = a.Contenu;  
                return View(a); 
            }
            else {
                unArticle.Update(a);
                var article = unArticle.Find(a.Titre);
                return RedirectToAction("Display", "Home", new { titre = article.Titre });       
            }               
        }

        //Affiche un article
        [HttpGet]
        public ActionResult Display(string titre) {
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière
            return View(unArticle.Find(titre));
        }

        //Affiche l'article à supprimer et demande la confirmation
        [HttpGet]
        public ActionResult Supprimer(string titre) { 
            ViewBag.TitleList = unArticle.GetTitres();//Affichage des titres dans la table de matière
            return View(unArticle.Find(titre));
        }

        /*Un clic sur le bouton supprimer du formulaire,
         *l'action est redigé vers la méthode HttpGet Supprimer  
         *pour confirmation avant la suppression définitive.
         */
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult RedirectToSuprimer(Article a) { 
            string str = a.Titre;
            return RedirectToAction("Supprimer", "Home", new { titre = a.Titre });
        }

        //Supprime définitivement l'article
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Supprimer(Article a) {   
            unArticle.Delete(a.Titre);
            return RedirectToAction("Index");
        }
    }
}
