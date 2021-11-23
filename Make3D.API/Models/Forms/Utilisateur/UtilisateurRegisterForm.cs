using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Make3D.API.Models.Forms.Utilisateur
{
    public class UtilisateurRegisterForm
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Password { get; set; }
    }
}
