using System;
using System.Data.Common;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;
using Wiki.Models.DAL;
using Wiki.Models.Biz;
using Wiki.Models.Views;
using Wiki.Resources.Views;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections.Generic;

namespace Wiki.Controllers
{
    public class UtilisateursController : Controller
    {
        Utilisateur utilisateur = new Utilisateur();
        Utilisateurs utilisateurs = new Utilisateurs();

        //GET: LOGIN
        [HttpGet]
        public ActionResult Connexion()
        {
            return View();
        }

        //POST: LOGIN
        [HttpPost]
        public ActionResult Connexion(UtilisateursConnexion uc, string returnURL)
        {
            ViewBag.error = "";

            if (ModelState.IsValid)
            {
                try
                {
                    utilisateur = utilisateurs.FindByCourriel(uc.Courriel);

                    if (utilisateur != null)
                    {
                        if (PasswordHash.ValidatePassword(uc.MDP, utilisateur.MDP))
                        {
                            FormsAuthentication.SetAuthCookie(uc.Courriel, uc.Persistence);

                            if (returnURL != null)
                            {
                                return RedirectToAction("ChangerLangue", "Home", new { langue = utilisateur.Langue, returnURL = "../Home/Index" });
                            }
                            else
                            {
                                return RedirectToAction("ChangerLangue", "Home", new { langue = utilisateur.Langue, returnURL = "../" + returnURL });
                            }
                        }
                        else
                        {
                            ViewBag.error = "Error";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.error = "Error";
                        return View();
                    }
                }
                catch
                {
                    ViewBag.error = "Error";
                    return View();
                }
            }
            return this.RedirectToAction("Index", "Home");
        }



        // GET: INDEX
        public ActionResult Index()
        {
            return View();
        }

        //GET: INSCRIPTION
        [HttpGet]
        public ActionResult Inscription()
        {
            return View();
        }

        //POST: INSCRIPTION
        [HttpPost]
        public ActionResult Inscription(UtilisateursInscription ui, string returnURL = "")
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ui.MDP == ui.ConfirmerMDP)
                    {
                        Utilisateur nouvelUtilisateur = new Utilisateur(ui);
                        Utilisateurs ut = new Utilisateurs();

                        PasswordHash.CreateHash(nouvelUtilisateur.MDP);
                        FormsAuthentication.SetAuthCookie(nouvelUtilisateur.Id.ToString(), false);

                        ut.Add(nouvelUtilisateur);
                        if (returnURL == "")
                        {
                            return RedirectToAction("Connexion", "Utilisateurs");
                        }
                        else
                        {
                            ViewBag.Error = StringsUtilisateurs.ErreurCourriel;
                            return View(ui);
                        }
                    }
                    else
                    {
                        ViewBag.Error = StringsUtilisateurs.ErreurUtilisateur;
                        return View(ui);
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", StringsUtilisateurs.ErreurModel);
                }
            }
            return View(ui);
        }



        [HttpGet, Authorize]
        public ActionResult Profil()
        {
            utilisateur = utilisateurs.FindByCourriel(User.Identity.Name);
            UtilisateursProfil userProfile = new UtilisateursProfil(utilisateur);
            return View(userProfile);
        }

        [HttpPost]
        public ActionResult Profil(UtilisateursProfil up, string ReturnUrl = "")
        {
            ViewBag.error = "";

            if (ModelState.IsValid)
            {
                if (utilisateur != null)
                {
                    utilisateur.Id = up.Id;
                    utilisateur.Prenom = up.Prenom;
                    utilisateur.NomFamille = up.NomFamille;
                    utilisateur.Courriel = User.Identity.Name;
                    utilisateurs.Update(utilisateur);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "error";
                    return this.RedirectToAction("Index", "Home");
                }

            }
            else
            {
                ViewBag.error = "error";
                return this.RedirectToAction("Index", "Home");
            }
        }

        [HttpGet, Authorize]
        public ActionResult ModifierMDP()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ModifierMDP(UtilisateursModifierMDP um)
        {
            ViewBag.error = "";

            if (ModelState.IsValid)
            {
                try
                {
                    utilisateur = utilisateurs.FindByCourriel(User.Identity.Name);

                    if (PasswordHash.ValidatePassword(um.AncienMDP, utilisateur.MDP))
                    {
                        utilisateur.MDP = PasswordHash.CreateHash(um.NouveauMDP);
                        utilisateurs.Update(utilisateur.Id, utilisateur.MDP);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Error = StringsUtilisateurs.ErreurAncienMDP;
                        return View(um);
                    }
                }
                catch (Exception e)
                {
                    return View(um);
                }
            }
            return View(um);
        }

        [HttpGet]
        public ActionResult Deconnexion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}