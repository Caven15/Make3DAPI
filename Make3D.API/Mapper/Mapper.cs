using Make3D.API.Models;
using Make3D.API.Models.Forms.Article;
using Make3D.API.Models.Forms.Utilisateur;
using Make3D.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Make3D.API.Mapper
{
    public static class Mapper
    {
        #region Utilisateur

        internal static UtilisateurModel ApiToBll(this UtilisateurRegisterForm form)
        {
            return new UtilisateurModel()
            {
                Nom = form.Nom,
                Prenom = form.Prenom,
                Email = form.Email,
                DateNaissance = form.DateNaissance,
                Password = form.Password
            };
        }

        internal static UtilisateurViewModel BllToApi(this UtilisateurModel model)
        {
            return new UtilisateurViewModel
            {
                Id = model.Id,
                Nom = model.Nom,
                Prenom = model.Prenom,
                Email = model.Email,
                DateNaissance = model.DateNaissance
            };
        }

        internal static UtilisateurViewModel BllToApî(this UtilisateurModel model)
        {
            return new UtilisateurViewModel()
            {
                Id = model.Id,
                Nom = model.Nom,
                Prenom = model.Prenom,
                Email = model.Email,
                DateNaissance = model.DateNaissance,
            };
        }
        #endregion

        #region Article

        #endregion
    }
}
