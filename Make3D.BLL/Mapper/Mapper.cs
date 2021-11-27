using Make3D.BLL.Models;
using Make3D.DAL.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Make3D.BLL.Mapper
{
    public static class Mapper
    {
        #region Utilisateur
        internal static UtilisateurData BllToDal(this UtilisateurModel model)
        {
            return new UtilisateurData()
            {
                Id = model.Id,
                Nom = model.Nom,
                Prenom = model.Prenom,
                Email = model.Email,
                DateNaissance = model.DateNaissance,
                Password = model.Password,
                IsAdmin = model.IsAdmin
            };
        }

        internal static UtilisateurModel DalToBll(this UtilisateurData data)
        {
            if (data is null) return null;
            return new UtilisateurModel()
            {
                Id = data.Id,
                Nom = data.Nom,
                Prenom = data.Prenom,
                Email = data.Email,
                DateNaissance = data.DateNaissance,
                IsAdmin = data.IsAdmin
            };
        }
        #endregion

        #region Article
        internal static ArticleData BllToDal(this ArticleModel model)
        {
            return new ArticleData()
            {
                Id = model.Id,
                Nom = model.Nom,
                Description = model.Description,
                Id_utilisateur = model.Id_utilisateur
            };
        }

        internal static ArticleModel DalToBll(this ArticleData data)
        {
            return new ArticleModel()
            {
                Id = data.Id,
                Nom = data.Nom,
                Description = data.Description,
                Id_utilisateur = data.Id_utilisateur
            };
        }
        #endregion
    }
}
