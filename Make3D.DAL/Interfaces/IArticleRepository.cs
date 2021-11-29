using Make3D.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Make3D.DAL.Repositories
{
    public interface IArticleRepository
    {
        #region Récupération
        IEnumerable<ArticleData> GetAll();// : Liste de tous les articles 

        ArticleData GetById(int id);// : Un article par son ID PK

        IEnumerable<ArticleData> GetAllByUserId(int id);// : Liste de tous les articles de l'utilisateur dont l'ID est  id

        #endregion

        #region Création / Modification / suppression => article
        void Create(ArticleData entity);// : créer un article

        void Update(int id, ArticleData entity);// : modifier l'article dont  l'ID est  id

        void Delete(int id);// : supprimer l'article dont  l'ID est  id

        #endregion

        #region Signalements

        IEnumerable<ArticleSignalerData> GetAllSignaler();// : Liste de tous les articles signalés 

        void Signalement(int articleId, int signaleurId);// : signaler un article

        bool EstSignale(int articleId);// : vérifier si l'article articleId est signal
                                       // SQL ==> SELECT COUNT(*) FROM [Signalement_article] WHERE Id_article = @Id_article;
                                       // C#  ==> return (int)_connexion.ExecuteScalar(command) > 0;
        bool EstSignaleParUserId(int articleId, int signaleurId);// : vérifier si l'article articleId est signalé par l'utilisateur signaleurId
                                                                 // SQL ==> SELECT COUNT(*) FROM [Signalement_article] WHERE Id_article = @Id_article AND Id_utilisateur = @Id_utilisateur;

        // Les opérations d'administrations
        void Designaler(int articleId, int designaleurId);// : supprimer le signalement d'un article

        #endregion

        #region Bloquages

        IEnumerable<ArticleBloquerData> GetAllBloquer();// : Liste de tous les articles bloqués

        void Bloquer(int articleId, int bloqeurId, string motivation);// : bloquer un article

        void Debloquer(int articleId, int debloqeurId);// : débloquer un article

        bool EstBloquer(int articleId);// ferifie si l'article est bloquer 
        
        #endregion
    }
}
