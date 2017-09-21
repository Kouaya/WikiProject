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
        // GET: Article
        public ActionResult Index(string id)
        {
            Article a = new Article();
            ViewBag.Status = "null";

            if (ModelState.IsValid)
            {
                if (id != null)
                {
                    if (id == "")
                    {
                        return View();
                    }

                    a = Articles.Find(id);

                    if (id == a.Titre)
                    {
                        ViewBag.Status = "found";
                        return View(a);
                    }

                    else
                    {
                        ViewBag.Status = "else";
                        return View(a);
                    }
                }
            }
            return View();
        }


        // GET: Article/Create
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            Article newArticle = new Article();
            return View(newArticle);
        }

        // POST: Home/Create
        [HttpPost]
        [Authorize]
        [ValidateInput(false)]  //POUR INSCRIRE DES BALISES HTML
        public ActionResult Create(Article newArticle)
        {

            if (ModelState.IsValid)
            {
                newArticle.IdContributeur = 1;
                Articles.Add(newArticle);

                return this.RedirectToAction("Index");
            }
            else
            {
                return View(newArticle);
            }
        }

        // GET: Article/Edit
        [HttpGet]
        [Authorize]
        public ActionResult Edit(string id)
        {
            Article a = new Article();
            Article toEdit = Articles.Find(id);

            return View(toEdit);
        }

        // POST: Article/Edit
        [HttpPost]
        [Authorize]
        [ValidateInput(false)]  //POUR INSCRIRE DES BALISES HTML
        public ActionResult Edit(string id, Article toEdit)
        {
            if (ModelState.IsValid)
            {
                toEdit.IdContributeur = 1;
                Articles.Update(toEdit);

                return RedirectToAction("Index");
            }
            else
            {
                return View("Edit");
            }
        }

        // GET: Article/Delete
        [HttpGet]
        [Authorize]
        public ActionResult Delete(string id)
        {
            Article toDelete = Articles.Find(id);

            return View(toDelete);
        }

        // POST: Article/Delete
        [HttpPost]
        [Authorize]
        public ActionResult Delete(string id, FormCollection collection)
        {
            Articles.Delete(id);

            return RedirectToAction("Index");
        }

    }
}
