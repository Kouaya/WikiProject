using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wiki.Models.DAL;
using Wiki.Models.Biz;

namespace Wiki.Controllers
{
    public class DALController : Controller
    {
        Articles repo = new Articles();

        [ValidateInput(false)]
        public ActionResult Index(String operation, Article a)
        {
            switch (operation)
            {
                case "Find":
                    if (a.Titre == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    a = repo.Find(a.Titre);
                    if (a == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.Article = a;
                    break;
                case "Update":
                    if (ModelState.IsValid)
                    {
                        a.IdContributeur = 1;
                        repo.Update(a);
                    }
                    break;
                case "Add":
                    if (ModelState.IsValid)
                    {
                        a.IdContributeur = 1;
                        repo.Add(a);
                    }
                    break;
                case "Delete":
                    repo.Delete(a.Titre);
                    break;
            }

            return View(repo.GetArticles());
        }



        [HttpGet]
        public ActionResult index(string titre) {
            Article article = new Article();
            article = repo.Find(titre);
            if(article == null) {
                ViewBag.TitreArticle = titre;
                return View("invite");
            }
            return View("afficher",article);
        }



        [HttpGet]
        public ActionResult ajouter(string titre) {
            Article article = new Article();
            article.Titre = titre;
            return View(article);
        }

         [HttpPost]
        public ActionResult ajouter(Article article) {
            Articles repo = new Articles();
            int num = repo.Add(article);
            return View(article);
        }


         [HttpGet]
         public ActionResult modifier(string titre) {
             Article article = repo.Find(titre);
             return View(article);
         }


         [HttpPost]
         public ActionResult modifier(Article article) {
             int num = repo.Update(article);
             return View(article);
         }





    }
}
