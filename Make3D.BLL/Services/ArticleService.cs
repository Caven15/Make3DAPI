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

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        
        #region Récupération
        public IEnumerable<ArticleModel> GetAll()
        {
            return _articleRepository.GetAll().Select(a => a.DalToBll()); // dr pour data reader
        }

        public IEnumerable<ArticleModel> GetAllByUserId(int id)
        {
            return _articleRepository.GetAllByUserId(id).Select(a => a.DalToBll());
        }

        public ArticleModel GetById(int id)
        {
            return _articleRepository.GetById(id).DalToBll();
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
        public void Designaler(int articleId, int designaleurId)
        {
            _articleRepository.Designaler(articleId, designaleurId);
        }

        public bool EstSignale(int articleId)
        {
            return _articleRepository.EstSignale(articleId);
        }

        public bool EstSignaleParUserId(int articleId, int signaleurId)
        {
            return _articleRepository.EstSignaleParUserId(articleId, signaleurId);
        }

        public void Signalement(int articleId, int signaleurId)
        {
            _articleRepository.Signalement(articleId, signaleurId);
        }
        #endregion

        #region Bloquage

        public void Bloquer(int articleId, int bloqeurId, string motivation)
        {
            _articleRepository.Bloquer(articleId, bloqeurId, motivation);
        }

        public void Debloquer(int articleId, int debloqeurId)
        {
            _articleRepository.Debloquer(articleId, debloqeurId);
        }

        public bool EstBloquer(int articleId)
        {
            return _articleRepository.EstBloquer(articleId);
        }

        #endregion

    }
}
