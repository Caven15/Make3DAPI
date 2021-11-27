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
            Command command = new Command("spArticleGetAllByUserId", true);
            command.AddParameter("Id_utilisateur", id);
            return _connection.ExecuteReader(command, dr => dr.DbToArticle());
        }

        public ArticleData GetById(int id)
        {
            Command command = new Command("spArticleGetById", true);
            command.AddParameter("Id", id);
            return _connection.ExecuteReader(command, dr => dr.DbToArticle()).SingleOrDefault();
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
            Command command = new Command("spArticleUpdate", true);
            command.AddParameter("Id", id);
            command.AddParameter("Nom", entity.Nom);
            command.AddParameter("Description", entity.Description);
            _connection.ExecuteNonQuery(command);
        }

        public void Delete(int id)
        {
            Command command = new Command("spArticleDelete", true);
            command.AddParameter("Id", id);
            _connection.ExecuteNonQuery(command);
        }
        #endregion

        #region Signalements des articles

        public void Designaler(int articleId, int designaleurId)
        {
            Command command = new Command("spArticleDesignaler", true);
            command.AddParameter("Id_article", articleId);
            command.AddParameter("Id_utilisateur", designaleurId);
            _connection.ExecuteNonQuery(command);
        }

        public bool EstSignale(int articleId)
        {
            Command command = new Command("spArticleEstSignale", true);
            command.AddParameter("Id_article", articleId);
            return (int)_connection.ExecuteScalar(command) > 0;
        }

        public bool EstSignaleParUserId(int articleId, int signaleurId)
        {
            Command command = new Command("spArticleEstSignaleParUserId", true);
            command.AddParameter("Id_article", articleId);
            command.AddParameter("Id_utilisateur", signaleurId);
            return (int)_connection.ExecuteScalar(command) > 0;
        }

        public void Signalement(int articleId, int signaleurId)
        {
            Command command = new Command("spArticleSignalement", true);
            command.AddParameter("Id_article", articleId);
            command.AddParameter("Id_utilisateur", signaleurId);
            _connection.ExecuteNonQuery(command);
        }

        #endregion

        #region Bloquage

        public void Bloquer(int articleId, int bloqeurId, string motivation)
        {
            Command command = new Command("spArticleBloquer", true);
            command.AddParameter("Id_article", articleId);
            command.AddParameter("Id_utilisateur", bloqeurId);
            command.AddParameter("Motivation", motivation);
            _connection.ExecuteNonQuery(command);
        }

        public void Debloquer(int articleId, int debloqeurId)
        {
            Command command = new Command("spArticleDebloquer", true);
            command.AddParameter("Id_article", articleId);
            command.AddParameter("Id_utilisateur", debloqeurId);
            _connection.ExecuteNonQuery(command);
        }

        public bool EstBloquer(int articleId)
        {
            Command command = new Command("spArticleEstBloquer", true);
            command.AddParameter("Id_article", articleId);
            return (int)_connection.ExecuteScalar(command) > 0;
        }
        #endregion

    }
}
