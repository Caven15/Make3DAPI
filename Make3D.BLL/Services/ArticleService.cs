using Make3D.BLL.Interfaces;
using Make3D.BLL.Mapper;
using Make3D.BLL.Models;
using Make3D.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Connection;

namespace Make3D.BLL.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUtilisateurService _utilisateurService;

        public ArticleService(IArticleRepository articleRepository, IUtilisateurService utilisateurService)
        {
            _articleRepository = articleRepository;
            _utilisateurService = utilisateurService;
        }
        
        #region Récupération
        public IEnumerable<ArticleModel> GetAll() // service admin
        {
            return _articleRepository.GetAll().Select(a => a.DalToBll(_utilisateurService)); // dr pour data reader
        }

        public IEnumerable<ArticleModel> GetAllByUserId(int id) // service admin
        {
            return _articleRepository.GetAllByUserId(id).Select(a => a.DalToBll(_utilisateurService));
        }

        public ArticleModel GetById(int id) // service admin
        {
            return _articleRepository.GetById(id).DalToBll(_utilisateurService);
        }
        #endregion

        #region Création / Modification / Suppression

        public void Create(ArticleModel entity)
        {
            _articleRepository.Create(entity.BllToDal());
        }

        public void Update(int id, ArticleModel entity)
        {
            _articleRepository.Update(id, entity.BllToDal());
        }

        public void Delete(int id)
        {
            _articleRepository.Delete(id);
        }
        #endregion

        #region Signalement
        public void Designaler(int articleId, int designaleurId) // service admin
        {
            _articleRepository.Designaler(articleId, designaleurId);
        }

        public void DesignalerAdmin(int articleId, int designaleurId) // service admin
        {
            _articleRepository.DesignalerAdmin(articleId, designaleurId);
        }

        public bool EstSignale(int articleId) // service admin
        {
            return _articleRepository.EstSignale(articleId);
        }

        public bool EstSignaleParUserId(int articleId, int signaleurId) // service admin
        {
            return _articleRepository.EstSignaleParUserId(articleId, signaleurId);
        }

        public void Signalement(int articleId, int signaleurId)
        {
            _articleRepository.Signalement(articleId, signaleurId);
        }
        #endregion

        #region Bloquage

        public void Bloquer(int articleId, int bloqeurId, string motivation) // service admin
        {
            _articleRepository.Bloquer(articleId, bloqeurId, motivation);
        }

        public void Debloquer(int articleId, int debloqeurId) // service admin
        {
            _articleRepository.Debloquer(articleId, debloqeurId);
        }

        public bool EstBloquer(int articleId) // service admin
        {
            return _articleRepository.EstBloquer(articleId);
        }

        #endregion

    }
}
