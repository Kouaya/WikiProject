using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using Wiki.Models.Biz;
using Wiki.Models.DAL;
using Wiki.Resources.Models;


namespace Wiki.Models.Views
{
    public class UtilisateursInscription
    {
        public static string[] Langues = { "fr-CA", "en-CA", "es-MX"};

        // public static string[] Langues = Utilisateur.Langues;

        [Key, Display(AutoGenerateField = true)]
        public int Id { get; set; }

        [MaxLength(50), Display(Name = "Prenom", ResourceType = typeof(StringsUtilisateur))]
        public string Prenom { get; set; }

        [MaxLength(50), Display(Name = "NomDeFamille", ResourceType = typeof(StringsUtilisateur))]
        public string NomFamille { get; set; }

        [Required, MaxLength(50), EmailAddress, Display(Name = "Courriel", ResourceType = typeof(StringsUtilisateur))]
        public string Courriel { get; set; }

        [Required, MaxLength(50), MinLength(6), Display(Name = "MdP", ResourceType = typeof(StringsUtilisateur))]
        public string MDP { get; set; }

        [Required, MaxLength(50), MinLength(6), Display(Name = "MdPConfirme", ResourceType = typeof(StringsUtilisateur)), Compare("MDP")]
        public string ConfirmerMDP { get; set; }

        [Display(Name = "Langue", ResourceType = typeof(StringsUtilisateur))]
        public string Langue { get; set; }

        [Required, Display(Name = "MaintienDeConnexion", ResourceType = typeof(StringsUtilisateur))]
        public bool Persistence { get; set; }
    }
}