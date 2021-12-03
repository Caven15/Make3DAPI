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
    public class CommentaireService : ICommentaireService
    {
        private readonly ICommentaireRepository _commentaireRepository;
        private readonly IUtilisateurService _utilisateurService;

        public CommentaireService(ICommentaireRepository commentaireRepository, IUtilisateurService utilisateurService)
        {
            _commentaireRepository = commentaireRepository;
            _utilisateurService = utilisateurService;
        }

        #region recupération

        public IEnumerable<CommentaireModel> GetAll()
        {
            return _commentaireRepository.GetAll().Select(c => c.DalToBll(_utilisateurService));
        }

        public IEnumerable<CommentaireModel> GetAllByArticleId(int id)
        {
            return _commentaireRepository.GetAllByArticleId(id).Select(c => c.DalToBll(_utilisateurService));
        }

        public CommentaireModel GetById(int id)
        {
            return _commentaireRepository.GetById(id)?.DalToBll(_utilisateurService);
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
