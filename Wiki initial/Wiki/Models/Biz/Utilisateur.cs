using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Wiki.Models.Views;
using Wiki.Resources.Models;
using Wiki.Resources.Views;


namespace Wiki.Models.Biz
{
    public class Utilisateur
    {
        private UtilisateursModifierMDP um;
        public static string[] Langues = { "fr-CA", "en-CA", "es-MX", "pt-PT" };

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire")]
        //ErrorMessageResourceName="ErreurPrenom", ErrorMessageResourceType = typeof(StringsUtilisateur)) 
        //Display(Name = "Prenom", ResourceType = typeof(StringsUtilisateurs))]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Le nom de famille est obligatoire")]
        //ErrorMessageResourceName="ErreurNom", ErrorMessageResourceType = typeof(StringsUtilisateur)),
        //Display(Name = "NomDeFamille", ResourceType = typeof(StringsUtilisateurs))]
        public string NomFamille { get; set; }

        [Required(ErrorMessage = "Le courriel est obligatoire"), EmailAddress]
        //ErrorMessageResourceName="ErreurCourriel", ErrorMessageResourceType = typeof(StringsUtilisateur)), 
        //Display(Name = "Courriel", ResourceType = typeof(StringsUtilisateurs))]
        public string Courriel { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire")
                    //ErrorMessageResourceName="ErreurMDP",
                    //ErrorMessageResourceType = typeof(StringsUtilisateur))
                    Range(6, (double)decimal.MaxValue)]
        //Display(Name = "MdP", ResourceType = typeof(StringsUtilisateurs))]
        public string MDP { get; set; }

        public string Langue { get; set; }

        //Utilisateur
        public Utilisateur()
        {
            Prenom = this.Prenom;
            NomFamille = this.NomFamille;
            Courriel = this.Courriel;
            MDP = this.MDP;
            Langue = this.Langue;
        }

        public Utilisateur(Utilisateur u)
        {
            Id = u.Id;
            Prenom = u.Prenom;
            NomFamille = u.NomFamille;
            Courriel = u.Courriel;
            MDP = u.MDP;
            Langue = u.Langue;
        }


        //UtilisateursInscription
        public Utilisateur(UtilisateursInscription ui)
        {
            Prenom = ui.Prenom;
            NomFamille = ui.NomFamille;
            Courriel = ui.Courriel;
            MDP = PasswordHash.CreateHash(ui.MDP);
            Langue = ui.Langue;
        }

        //UtilisateursProfil
        public Utilisateur(UtilisateursProfil up)
        {
            Courriel = this.Courriel;
            Prenom = up.Prenom;
            NomFamille = up.NomFamille;
            MDP = this.MDP;
            Langue = up.Langue;
        }

        public Utilisateur(UtilisateursModifierMDP um)
        {
            this.um = um;
        }

        public void ChangerMDP(string mdp)
        {
            this.MDP = CrypterMDPSHA256(mdp);
        }

        public bool ComparerMDP(string mdp)
        {
            if (this.MDP == CrypterMDPSHA256(mdp))
                return true;
            else
                return false;
        }

        static public string CrypterMDPSHA256(string mdp)
        {
            SHA256 encryption = SHA256.Create();
            byte[] res = encryption.ComputeHash(Encoding.Default.GetBytes(mdp));
            StringBuilder sRes = new StringBuilder(64);

            for (int i = res.GetLowerBound(0); i <= res.GetUpperBound(0); i++)
            {
                // x2: Hexa sur 2 chiffres
                sRes.Append(res[i].ToString("x2"));
            }

            return sRes.ToString().ToLower();
        }
    }
}