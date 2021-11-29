using Make3D.DAL.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Make3D.DAL.Mapper
{
    internal static class Mapper
    {
        #region Utilisateur
        internal static UtilisateurData DbToUtilisateur(this IDataRecord record)
        {
            return new UtilisateurData()
            {
                Id = (int)record["Id"],
                Nom = (string)record["Nom"],
                Prenom = (string)record["Prenom"],
                Email = (string)record["Email"],
                DateNaissance = (DateTime)record["DateNaissance"],
                IsAdmin = (bool)record["IsAdmin"]
            };
        }
        #endregion

        #region Article
        internal static ArticleData DbToArticle(this IDataRecord record)
        {
            return new ArticleData()
            {
                Id = (int)record["Id"],
                Nom = (string)record["Nom"],
                Description = (string)record["Description"],
                Id_utilisateur = (int)record["Id_utilisateur"]
            };
        }

        internal static ArticleSignalerData DbToArticleSignaler(this IDataRecord record)
        {
            return new ArticleSignalerData()
            {
                Id_article = (int)record["Id_article"],
                Id_utilisateur = (int)record["Id_utilisateur"]
            };
        }

        internal static ArticleBloquerData DbToArticleBloquer(this IDataRecord record)
        {
            return new ArticleBloquerData()
            {
                Id_article = (int)record["Id_article"],
                Id_utilisateur = (int)record["Id_utilisateur"],
                Motivation = (string)record["Motivation"]
            };
        }
        #endregion
    }
}
