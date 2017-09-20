using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wiki.Models.Biz {
    public class Utilisateur {


        public int Id { get; set; }


        [Required(ErrorMessageResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur), ErrorMessageResourceName = "ErrorLastName")]
        [Display(Name = "LastName", ResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur))]
        public string NomFamille { set; get; }

        [Required(ErrorMessageResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur), ErrorMessageResourceName = "ErrorFirstName")]
        [Display(Name = "FirstName", ResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur))]
        public string Prenom { set; get; }

        [Required(ErrorMessageResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur), ErrorMessageResourceName = "ErrorEmail")]
        [EmailAddress(ErrorMessageResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur), ErrorMessageResourceName = "ErrorEmailFormat")]
        [Display(Name="Email", ResourceType=typeof(Wiki.Ressources.Utilisateur.Utilisateur))]
        public string Courriel { set; get; }

        [Required(ErrorMessageResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur), ErrorMessageResourceName = "ErrorPassWord")]
        [StringLength(50, MinimumLength = 6, ErrorMessageResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur), ErrorMessageResourceName = "ErrorPassWordLength")]
        [DataType(DataType.Password)]
        [Display(Name = "PassWord", ResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur))]
        public string MDP { set; get; }

        [Required(ErrorMessageResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur), ErrorMessageResourceName = "ErrorConfirmPw")]
        [Compare("MDP", ErrorMessageResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur), ErrorMessageResourceName = "ErrorConfirmPassWord")]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassWord", ResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur))]
        public string CMDP { set; get; } 

        [Display(Name = "Language", ResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur))]
        public string Langue { set; get; }




    }
}