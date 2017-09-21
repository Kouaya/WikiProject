using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wiki.Models.DAL;
using System.ComponentModel.DataAnnotations;
using Wiki.Resources.Models;
using Wiki.Models.Biz;

namespace Wiki.Models.Views
{
    public class UtilisateursModifierMDP
    {


        [Required, MaxLength(50), MinLength(6), Display(Name = "AncienMDP", ResourceType = typeof(StringsUtilisateur))]
        public string AncienMDP { get; set; }

        [Required, MaxLength(50), MinLength(6), Display(Name = "NouveauMDP", ResourceType = typeof(StringsUtilisateur))]
        public string NouveauMDP { get; set; }

        [Required, MaxLength(50), MinLength(6), Display(Name = "NouveauConfirmeMDP", ResourceType = typeof(StringsUtilisateur)), Compare("NouveauMDP")]
        public string NouveauConfirmerMDP { get; set; }

        public UtilisateursModifierMDP()
        {

        }
    }
}