using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wiki.Models.Views {
    public class UtilisateursConnexion {

        [Required(ErrorMessageResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur), ErrorMessageResourceName = "ErrorEmail")]
        [EmailAddress(ErrorMessageResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur), ErrorMessageResourceName = "ErrorEmailFormat")]
        [Display(Name = "Email", ResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur))]
        public string Courriel { set; get; }

        [Required(ErrorMessageResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur), ErrorMessageResourceName = "ErrorPassWord")]
        [StringLength(50, MinimumLength = 6, ErrorMessageResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur), ErrorMessageResourceName = "ErrorPassWordLength")]
        [DataType(DataType.Password)]
        [Display(Name = "PassWord", ResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur))]
        public string MDP { set; get; }
    }

    public class UtilisateurModificationMdp {

        [Required(ErrorMessageResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur), ErrorMessageResourceName = "ErrorPassWord")]
        [StringLength(50, MinimumLength = 6, ErrorMessageResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur), ErrorMessageResourceName = "ErrorPassWordLength")]
        [DataType(DataType.Password)]
        [Display(Name = "OldPassword", ResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur))]
        public string AMDP { set; get; }   

        [Required(ErrorMessageResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur), ErrorMessageResourceName = "ErrorPassWord")]
        [StringLength(50, MinimumLength = 6, ErrorMessageResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur), ErrorMessageResourceName = "ErrorPassWordLength")]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur))]
        public string NMDP { set; get; } 

        [Required(ErrorMessageResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur), ErrorMessageResourceName = "ErrorConfirmPw")]
        [Compare("NMDP", ErrorMessageResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur), ErrorMessageResourceName = "ErrorConfirmPassWord")]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassWord", ResourceType = typeof(Wiki.Ressources.Utilisateur.Utilisateur))]
        public string CMDP { set; get; } 




    }
}