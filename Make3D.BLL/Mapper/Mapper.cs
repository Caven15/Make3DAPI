using Make3D.BLL.Interfaces;
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
                Id_utilisateur = model.Id_utilisateur,
                Date_envoi = model.Date_envoi,
                Date_modif = model.Date_modif
            };
        }

        internal static ArticleModel DalToBll(this ArticleData data, IUtilisateurService utilisateurService = null)
        {
            ArticleModel articleModel = new ArticleModel()
            {
                Id = data.Id,
                Nom = data.Nom,
                Description = data.Description,
                Id_utilisateur = data.Id_utilisateur,
                Date_envoi = data.Date_envoi,
                Date_modif = data.Date_modif
            };
            if(utilisateurService is not null)
            {
                // récupération du créateur de l'article
                UtilisateurModel utilisateurModel = utilisateurService.GetUtilisateurById(data.Id_utilisateur);
                // récupération du nom et prénom
                articleModel.NomCreateur = utilisateurModel.Nom + " " + utilisateurModel.Prenom;
            }
            return articleModel;
        }
        #endregion

        #region Commentaire

        internal static CommentaireData BllToDal(this CommentaireModel model)
        {
           return new CommentaireData()
            {
                Id = model.Id,
                Id_article = model.Id_article,
                Id_utilisateur = model.Id_utilisateur,
                Commentaire = model.Commentaire,
                Date_envoi = model.Date_envoi,
                Date_modif = model.Date_modif
            };
        }

        internal static CommentaireModel DalToBll(this CommentaireData data, IUtilisateurService utilisateurService = null)
        {
            CommentaireModel commentaireData = new CommentaireModel()
            {
                Id = data.Id,
                Id_article = data.Id_article,
                Id_utilisateur = data.Id_utilisateur,
                Commentaire = data.Commentaire,
                Date_envoi = data.Date_envoi,
                Date_modif = data.Date_modif
            };
            if (utilisateurService is not null)
            {
                // récupération du créateur de l'article
                UtilisateurModel utilisateurModel = utilisateurService.GetUtilisateurById(data.Id_utilisateur);
                // récupération du nom et prénom
                commentaireData.NomCreateur = utilisateurModel.Nom + " " + utilisateurModel.Prenom;
            }
            return commentaireData;
        }
        #endregion

        #region Fichier

        internal static FichierData BllToDal(this FichierModel model)
        {
            return new FichierData()
            {
                Id = model.Id,
                Id_article = model.Id_article,
                Name = model.Name
            };
        }

        internal static FichierModel DalToBll(this FichierData data)
        {
            return new FichierModel()
            {
                Id = data.Id,
                Id_article = data.Id_article,
                Name = data.Name
            };
        }
        #endregion
    }
}
