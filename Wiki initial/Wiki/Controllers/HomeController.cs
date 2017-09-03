using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wiki.Models.Biz;
using Wiki.Models.DAL;

namespace Wiki.Controllers {
    public class HomeController : Controller {
        Articles repo = new Articles();

        [HttpGet]
        public ActionResult Index(string titre) {
            Article article = new Article();
            IList<string> TitlestList = repo.GetTitres();
            ViewBag.TitleList = TitlestList;
            String FistTitle = TitlestList.First();
            article = repo.Find(titre);
            Article FirstArticle = repo.Find(FistTitle);
            ViewBag.FirstArticle = FirstArticle;

            if(titre == null) {
                return View("Index", FirstArticle);
            }
            else if(TitlestList.Contains(titre)) {
                ViewBag.TitreArticle = titre;
                Article item = repo.Find(titre);
                ViewBag.Contenu = item.Contenu;
                return View("afficher", item);
            }else {
                ViewBag.TitreArticle = titre;
                return View("invite");
            }

        }

        [HttpPost]
        public ActionResult consulter(string titre) {
            IList<string> TitlestList = repo.GetTitres();
            ViewBag.TitleList = TitlestList;
            Article item = repo.Find(titre);
            return RedirectToAction("Index", item);
        }



        [HttpGet]
        public ActionResult ajouter(string titre) {
            Articles repo = new Articles();
            IList<string> TitlestList = repo.GetTitres();

            ViewBag.TitleList = TitlestList;

            if(!TitlestList.Contains(titre)) {
                Article article = new Article();
                article.Titre = titre;
                return View(article);
            }
            else {
                Article articleCheck = repo.Find(titre);
                ViewBag.TitreArticle = titre;
                ViewBag.Contenu = articleCheck.Contenu;
                return View("duplicate");
            }

        }

        [HttpPost]
        public ActionResult ajouter(Article article, string Enregistrer, string Apercu) {
            Articles repo = new Articles();
            IList<string> TitlestList = repo.GetTitres();
            ViewBag.TitleList = TitlestList;
            ViewBag.Contenu = article.Contenu;
            if(!string.IsNullOrEmpty(Enregistrer)) {
                int num = repo.Add(article);
            }

            if(!string.IsNullOrEmpty(Apercu)) {

            }
            return View(article);
        }


        [HttpGet]
        public ActionResult modifier(string titre) {
            IList<string> TitlestList = repo.GetTitres();
            ViewBag.TitleList = TitlestList;
            Article article = repo.Find(titre);
            return View(article);
        }


        [HttpPost]
        public ActionResult modifier(Article article, string Enregistrer, string Apercu, string Supprimer) {
            Articles repo = new Articles();
            IList<string> TitlestList = repo.GetTitres();
            ViewBag.Contenu = article.Contenu;
            ViewBag.TitleList = TitlestList;

            if(!string.IsNullOrEmpty(Apercu)) {

            }

            if(!string.IsNullOrEmpty(Enregistrer)) {
                int num = repo.Update(article);
            }

            if(!string.IsNullOrEmpty(Supprimer)) {
                int num = repo.Delete(article.Titre);
            }
            return View(article);
        }


        [HttpGet]
        public ActionResult supprimer(string titre) {
            IList<string> TitlestList = repo.GetTitres();
            ViewBag.TitleList = TitlestList;
            Article article = repo.Find(titre);
            return View(article);
        }

        [HttpPost]
        public ActionResult supprimer(Article article) {
            IList<string> TitlestList = repo.GetTitres();
            ViewBag.TitleList = TitlestList;
            int num = repo.Delete(article.Titre);
            article = null;
            return RedirectToAction("Index", article); 
        }

    }
}