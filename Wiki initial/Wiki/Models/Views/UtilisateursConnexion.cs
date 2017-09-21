using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Wiki.Resources.Models;
using Wiki.Resources.Views;

namespace Wiki.Models.Views
{
    public class UtilisateursConnexion
    {
        [Required, Display(Name = "Courriel", ResourceType = typeof(StringsUtilisateur))]
        [RegularExpression(@"^\w+@\w+\.\w+$", ErrorMessage = "Invalid email")]
        public string Courriel { get; set; }

        [Required, MaxLength(50), MinLength(6), Display(Name = "MdP", ResourceType = typeof(StringsUtilisateur))]
        public string MDP { get; set; }

        public string Langue { get; set; }

        [Required, Display(Name = "MaintienDeConnexion", ResourceType = typeof(StringsUtilisateur))]
        public bool Persistence { get; set; }
    }
}
