using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Wiki.Models.Biz
{
    public class Article
    {
        [Required]
        public string Titre { get; set; }

        [Required]
        public string Contenu { get; set; }
        [DisplayName("Date de modification")]
        public DateTime DateModification { get; set; }

        public int Revision { get; set; }
        [DisplayName("Id contributeur")]
        public int IdContributeur { get; set; }

    }
}