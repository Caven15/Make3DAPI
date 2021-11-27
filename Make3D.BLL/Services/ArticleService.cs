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
        
        #region Récupération des articles
        public IEnumerable<ArticleModel> GetAll()
        {
            return _articleRepository.GetAll().Select(a => a.DalToBll()); // dr pour data reader
        }

        public IEnumerable<ArticleModel> GetAllByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public ArticleModel GetById(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Création / Modification / Suppression => article

        public void Create(ArticleModel entity)
        {
            _articleRepository.Create(entity.BllToDal());
        }

        public void Update(int id, ArticleModel entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        public void Bloquer(int articleId, int bloqeurId, string motivation)
        {
            throw new NotImplementedException();
        }

        public void Debloquer(int articleId, int debloqeurId)
        {
            throw new NotImplementedException();
        }

        public void Designaler(int articleId, int designaleurId)
        {
            throw new NotImplementedException();
        }

        public bool estSignale(int articleId)
        {
            throw new NotImplementedException();
        }

        public bool estSignaleParUserId(int articleId, int signaleurId)
        {
            throw new NotImplementedException();
        }

        public void Signaler(int articleId, int signaleurId)
        {
            throw new NotImplementedException();
        }
    }
}
