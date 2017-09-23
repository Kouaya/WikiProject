using System;
using System.ComponentModel.DataAnnotations;
using Wiki.Resources.Views;
using Wiki.Resources.Models;

namespace Wiki.Models.Biz
{

    
    public class Article
    {
        
        public int Quantite { get; set; }

        [Required Display(Name = "ArticleTitre", ResourceType = typeof(StringsUtilisateurs))]
        public string Titre { get; set; }


        [Required Display(Name = "ArticleContenu", ResourceType = typeof(StringsUtilisateurs))]
        public string Contenu { get; set; }

        [Display(Name = "ArticleDateModification", ResourceType = typeof(StringsUtilisateurs))]
        public DateTime DateModification { get; set; }

        public int Revision { get; set; }

        public int IdContributeur { get; set; }

        internal static void Add()
        {
            throw new NotImplementedException();
        }
    }
}