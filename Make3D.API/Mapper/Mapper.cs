using Make3D.API.Models;
using Make3D.API.Models.Forms.Article;
using Make3D.API.Models.Forms.Utilisateur;
using Make3D.BLL.Interfaces;
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

        internal static ArticleAPIModel BllToApî(this ArticleModel model, IFichierService fichierService)
        {
            ArticleAPIModel articleAPIModel = new ArticleAPIModel()
            {
                Id = model.Id,
                Nom = model.Nom,
                Description = model.Description,
                Id_utilisateur = model.Id_utilisateur,
                Date_envoi = model.Date_envoi,
                Date_modif = model.Date_modif,
                NomCreateur = model.NomCreateur
            };

            // récupération de la liste des fichiers de l'article
            IEnumerable<FichierModel> fichierModels = fichierService.GetByArticleId(articleAPIModel.Id);
            // récupération de la liste des ID des fichiers de l'article
            IEnumerable<int> id_fichiers = fichierModels?.Select(f => f.Id);
            // assignation de la liste des ID des fichiers de l'article
            articleAPIModel.Id_fichiers = id_fichiers;

            return articleAPIModel;
        }

        #endregion
    }
}
