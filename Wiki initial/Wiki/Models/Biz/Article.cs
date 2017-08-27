using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Wiki.Models.Biz
{
    public class Article
    {
        [Required]
        public string Titre { get; set; }

        [Required]
        [AllowHtml]
        public string Contenu { get; set; }

        public DateTime DateModification { get; set; }

        public int Revision { get; set; }

        public int IdContributeur { get; set; }

    }
}