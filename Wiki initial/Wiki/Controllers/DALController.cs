using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Wiki.Models.DAL;
using Wiki.Models.Biz;

namespace Wiki.Controllers
{
    public class DALController : Controller
    {
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
                    a = Articles.Find(a.Titre);
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
                        Articles.Update(a);
                    }
                    break;
                case "Create":
                    if (ModelState.IsValid)
                    {
                        a.IdContributeur = 1;
                        Articles.Add(a);
                    }
                    break;
                case "Delete":
                    Articles.Delete(a.Titre);
                    break;
            }

            return View(Articles.GetArticles());
        }


    }
}