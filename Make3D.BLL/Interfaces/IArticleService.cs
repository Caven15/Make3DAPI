using Make3D.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Make3D.BLL.Interfaces
{
    public interface IArticleService
    {
        IEnumerable<ArticleModel> GetAll();// : Liste de tous les articles 

        ArticleModel GetById(int id);// : Un article par son ID PK

        IEnumerable<ArticleModel> GetAllByUserId(int id);// : Liste de tous les articles de l'utilisateur dont l'ID est  id

        void Create(ArticleModel entity);// : créer un article

        void Update(int id, ArticleModel entity);// : modifier l'article dont  l'ID est  id

        void Delete(int id);// : supprimer l'article dont  l'ID est  id

        void Signaler(int articleId, int signaleurId);// : signaler un article

        bool estSignale(int articleId);// : vérifier si l'article articleId est signal
                                       // SQL ==> SELECT COUNT(*) FROM [Signalement_article] WHERE Id_article = @Id_article;
                                       // C#  ==> return (int)_connexion.ExecuteScalar(command) > 0;
        bool estSignaleParUserId(int articleId, int signaleurId);// : vérifier si l'article articleId est signalé par l'utilisateur signaleurId
                                                                 // SQL ==> SELECT COUNT(*) FROM [Signalement_article] WHERE Id_article = @Id_article AND Id_utilisateur = @Id_utilisateur;

        // Les opérations d'administrations
        void Designaler(int articleId, int designaleurId);// : supprimer le signalement d'un article

        void Bloquer(int articleId, int bloqeurId, string motivation);// : bloquer un article

        void Debloquer(int articleId, int debloqeurId);// : débloquer un article
    }
}
