using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Wiki.Models.Views;
using Wiki.Resources.Models;
using Wiki.Resources.Views;


namespace Wiki.Models.Biz
{
    public class UtilisateurV2
    {
        public static string[] Langues = { "fr", "en", "es", "pt" };

        [Key]
        public int Id { get; set; }

        [Required   (ErrorMessage = "Le prénom est obligatoire")]
                    //ErrorMessageResourceName="ErreurPrenom", ErrorMessageResourceType = typeof(StringsUtilisateur)) 
                    //Display(Name = "Prenom", ResourceType = typeof(StringsUtilisateurs))]
        public string Prenom { get; set; }

        [Required   (ErrorMessage = "Le nom de famille est obligatoire")]
                    //ErrorMessageResourceName="ErreurNom", ErrorMessageResourceType = typeof(StringsUtilisateur)),
                    //Display(Name = "NomDeFamille", ResourceType = typeof(StringsUtilisateurs))]
        public string NomFamille { get; set; }

        [Required   (ErrorMessage = "Le courriel est obligatoire"), DataType(DataType.EmailAddress)]
                    //ErrorMessageResourceName="ErreurCourriel", ErrorMessageResourceType = typeof(StringsUtilisateur)), 
                    //Display(Name = "Courriel", ResourceType = typeof(StringsUtilisateurs))]
        [EmailAddress]
        public string Courriel { get; set; }

        [Required   (ErrorMessage = "Le mot de passe est obligatoire")
                    //ErrorMessageResourceName="ErreurMDP",
                    //ErrorMessageResourceType = typeof(StringsUtilisateur))
                    Range(6, (double)decimal.MaxValue)]
                    //Display(Name = "MdP", ResourceType = typeof(StringsUtilisateurs))]
        public string MDP { get; set; }

        [Required, Range(6, (double)decimal.MaxValue)]
        [System.ComponentModel.DataAnnotations.Compare("MDP")]
        public string ConfirmerMDP { get; set; }

        public string Langue { get; set; }

        public UtilisateurV2(Utilisateur u)
        {
            Id = u.Id;
            Prenom = u.Prenom;
            NomFamille = u.NomFamille;
            Courriel = u.Courriel;
            MDP = u.MDP;
            //Langue = u.Langue;
        }

        public UtilisateurV2(UtilisateursInscription ui)
        {
            Courriel = ui.Courriel;
            MDP = /*_hashMotDePasse(*/ui.MDP/*)*/;
            //Langue = ui.Langue;
        }
        public UtilisateurV2(UtilisateursProfil up)
        {
            Id = up.Id;
            Courriel = up.Courriel;
            //Langue = up.Langue;
        }

        public UtilisateurV2()
        {
        }

        //public IList<Article> Articles
        //{
        //    get {
        //        if(_articles == null)
        //            _articles = new Articles().GetArt
        //    }
        //}
    }

}