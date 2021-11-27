using Make3D.DAL.Data;
using Make3D.DAL.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Connection;

namespace Make3D.DAL.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly Connection _connection;

        public ArticleRepository(Connection connection)
        {
            _connection = connection;
        }

        #region Récupération des articles
        public IEnumerable<ArticleData> GetAll()
        {
            Command command = new Command("spArticleGetAll", true);
            return _connection.ExecuteReader(command, dr => dr.DbToArticle()); // dr pour data reader
        }

        public IEnumerable<ArticleData> GetAllByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public ArticleData GetById(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Création / Modification / Suppression => article
        public void Create(ArticleData data)
        {
            Command command = new Command("spArticleCreate", true);
            command.AddParameter("Nom", data.Nom);
            command.AddParameter("Description", data.Description);
            command.AddParameter("Id_utilisateur", data.Id_utilisateur);
            _connection.ExecuteNonQuery(command);
        }

        public void Update(int id, ArticleData entity)
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
