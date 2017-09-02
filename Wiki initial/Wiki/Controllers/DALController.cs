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

        // Get: DAL
        public ActionResult Index(String titre) {
            if (titre != null) {                
                ViewBag.Article = repo.Find(titre);
            }
            return View(repo.GetArticles());
        }

       

        /*[ValidateInput(false)]
        public ActionResult Index(String operation, String titre) 
        {
            switch (operation)
            {
                case "Find":
                    if (titre == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    var a = repo.Find(titre);
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
            ViewBag.Text = "test du ViewBag";
            return View(repo.GetArticles());
        }*/
     
    }
}
