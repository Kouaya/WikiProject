using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Wiki.Models.Biz;
using Wiki.Models.DAL;
using Wiki.Resources.Models;

namespace Wiki.Models.Views
{

    public class UtilisateursProfil
    {

        public static string[] Langues = { "fr-CA", "en-CA", "es-MX", "pt-PT" };

        //public static string[] Langues = Utilisateur.Langues;

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50), Display(Name = "Prenom", ResourceType = typeof(StringsUtilisateur))]
        public string Prenom { get; set; }

        [Required, MaxLength(256), Display(Name = "NomDeFamille", ResourceType = typeof(StringsUtilisateur))]
        public string NomFamille { get; set; }

        [MaxLength(5), Display(Name = "Langue", ResourceType = typeof(StringsUtilisateur))]
        public string Langue { get; set; }


        public UtilisateursProfil()
        {

        }

        public UtilisateursProfil(Utilisateur u)
        {
            Id = u.Id;
            Prenom = u.Prenom;
            NomFamille = u.NomFamille;
            Langue = u.Langue;
        }
    }
}