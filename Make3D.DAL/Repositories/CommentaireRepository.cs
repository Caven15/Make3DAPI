using Make3D.DAL.Data;
using Make3D.DAL.Interfaces;
using Make3D.DAL.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Connection;

namespace Make3D.DAL.Repositories
{
    public class CommentaireRepository : ICommentaireRepository
    {
        private readonly Connection _connection;

        public CommentaireRepository(Connection connection)
        {
            _connection = connection;
        }

        #region récupération

        public IEnumerable<CommentaireData> GetAll()
        {
            Command command = new Command("spCommentaireGetAll", true);
            return _connection.ExecuteReader(command, dr => dr.DbToCommentaire());
        }

        public IEnumerable<CommentaireData> GetAllByArticleId(int id)
        {
            Command command = new Command("spCommentaireGetAllByArticleId", true);
            command.AddParameter("Id_article", id);
            return _connection.ExecuteReader(command, dr => dr.DbToCommentaire());
        }

        public CommentaireData GetById(int id)
        {
            Command command = new Command("spCommentaireGetById", true);
            command.AddParameter("Id_article", id);
            return _connection.ExecuteReader(command, dr => dr.DbToCommentaire()).SingleOrDefault();
        }

        #endregion

        #region Création / Modification / Suppression

        public void Create(CommentaireData model)
        {
            Command command = new Command("spCommentaireCreate", true);
            command.AddParameter("Commentaire", model.Commentaire);
            command.AddParameter("Id_article", model.Id_article);
            command.AddParameter("Id_utilisateur", model.Id_utilisateur);
            _connection.ExecuteNonQuery(command);
        }

        public void Update(int id, CommentaireData model)
        {
            Command command = new Command("spCommentaireUpdate", true);
            command.AddParameter("Id", id);
            command.AddParameter("Commentaire", model.Commentaire);
            _connection.ExecuteNonQuery(command);
        }

        public void Delete(int id)
        {
            Command command = new Command("spCommentaireDelete", true);
            command.AddParameter("Id", id);
            _connection.ExecuteNonQuery(command);
        }
        #endregion
    }
}
