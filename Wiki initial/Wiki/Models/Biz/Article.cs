using System;
using System.ComponentModel.DataAnnotations;
using Wiki.Resources.Views;

namespace Wiki.Models.Biz
{

    public class Article
    {
        [Required]
        public string Titre { get; set; }

        [Required]
        public string Contenu { get; set; }

        public DateTime DateModification { get; set; }

        public int Revision { get; set; }

        public int IdContributeur { get; set; }

        internal static void Add()
        {
            throw new NotImplementedException();
        }
    }
}