using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Wiki.Models.Biz
{
    public class Article
    {
        [Display(Name="Title", ResourceType=typeof(Wiki.Ressources.Article.Articles))]
        [Required(ErrorMessageResourceType = typeof(Wiki.Ressources.Article.ErrorMessage), ErrorMessageResourceName = "TitleOmission")]
        public string Titre { get; set; }

        [Display(Name = "EditingDate", ResourceType = typeof(Wiki.Ressources.Article.Articles))]
        public DateTime DateModification { get; set; }

        public int Revision { get; set; }

        [Display(Name = "IdContributor", ResourceType = typeof(Wiki.Ressources.Article.Articles))]
        public int IdContributeur { get; set; }

        [Display(Name = "Content", ResourceType = typeof(Wiki.Ressources.Article.Articles))]
        [Required(ErrorMessageResourceType = typeof(Wiki.Ressources.Article.ErrorMessage), ErrorMessageResourceName = "ContentOmission")]
        public string Contenu { get; set; }

    }
}