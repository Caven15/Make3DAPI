using Make3D.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Make3D.BLL.Interfaces
{
    public interface ICommentaireService
    {
        #region récupération

        // récupération de tout les Commetaires
        IEnumerable<CommentaireModel> GetAll();

        // récupération de tout les commentaire par l'id de son article
        IEnumerable<CommentaireModel> GetAllByArticleId(int id);

        // récupération d'un commentaire par son id 
        CommentaireModel GetById(int id);

        #endregion

        #region Création / Modification / Suppression

        // création d'un commentaire 
        void Create(CommentaireModel model);

        // modification d'un commentaire par son créateur
        void Update(int id, CommentaireModel model);

        // supprime un commentaire par son id 
        void Delete(int id);

        #endregion
    }
}
