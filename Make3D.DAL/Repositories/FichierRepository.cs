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
    public class FichierRepository : IFichierRepository
    {
        private readonly Connection _connection;

        public FichierRepository(Connection connection)
        {
            _connection = connection;
        }

        public IEnumerable<FichierData> GetAll()
        {
            Command command = new Command("spFichierGetAll", true);
            return _connection.ExecuteReader(command, dr => dr.DbToFichier());
        }

        public IEnumerable<FichierData> GetByArticleId(int id)
        {
            Command command = new Command("spFichierGetByArticleId", true);
            command.AddParameter("Id_article", id);
            return _connection.ExecuteReader(command, dr => dr.DbToFichier());
        }

        public FichierData GetById(int id)
        {
            Command command = new Command("spFichierGetById", true);
            command.AddParameter("Id", id);
            return _connection.ExecuteReader(command, dr => dr.DbToFichier()).SingleOrDefault();
        }

        public void Create(FichierData fichier)
        {
            Command command = new Command("spFichierCreate", true);
            command.AddParameter("Id_article", fichier.Id_article);
            command.AddParameter("Name", fichier.Name);
            _connection.ExecuteNonQuery(command);
        }

        public void Update(int id, FichierData fichier)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        
    }
}
