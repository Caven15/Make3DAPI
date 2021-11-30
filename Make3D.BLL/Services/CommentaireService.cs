using Make3D.BLL.Interfaces;
using Make3D.BLL.Mapper;
using Make3D.BLL.Models;
using Make3D.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Make3D.BLL.Services
{
    class CommentaireService : ICommentaireService
    {
        private readonly ICommentaireRepository _commentaireRepository;

        public CommentaireService(ICommentaireRepository commentaireRepository)
        {
            _commentaireRepository = commentaireRepository;
        }

        #region recupération

        public IEnumerable<CommentaireModel> GetAll()
        {
            return _commentaireRepository.GetAll().Select(c => c.DalToBll());
        }

        public IEnumerable<CommentaireModel> GetAllByArticleId(int id)
        {
            return _commentaireRepository.GetAllByArticleId(id).Select(c => c.DalToBll());
        }

        public CommentaireModel GetById(int id)
        {
            return _commentaireRepository.GetById(id)?.DalToBll();
        }

        #endregion


        #region Création / modification / Update

        public void Create(CommentaireModel model)
        {
            _commentaireRepository.Create(model.BllToDal());
        }

        public void Update(int id, CommentaireModel model)
        {
            _commentaireRepository.Update(id, model.BllToDal());
        }

        public void Delete(int id)
        {
            _commentaireRepository.Delete(id);
        }
        #endregion

    }
}
