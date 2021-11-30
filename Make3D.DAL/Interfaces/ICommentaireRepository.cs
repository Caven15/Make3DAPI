using Make3D.DAL.Data;
using Make3D.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Make3D.DAL.Interfaces
{
    public interface ICommentaireRepository
    {
        #region récupération

        // récupération de tout les Commetaires
        IEnumerable<CommentaireData> GetAll();

        // récupération de tout les commentaire par l'id de son article
        IEnumerable<CommentaireData> GetAllByArticleId(int id);

        // récupération d'un commentaire par son id 
        CommentaireData GetById(int id);

        #endregion

        #region Création / Modification / Suppression

        // création d'un commentaire 
        void Create(CommentaireData model);

        // modification d'un commentaire par son créateur
        void Update(int id,CommentaireData model);

        // supprime un commentaire par son id 
        void Delete(int id); 

        #endregion
    }
}
