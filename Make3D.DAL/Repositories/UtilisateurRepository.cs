using Make3D.DAL.Data;
using Make3D.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Connection;

namespace Make3D.DAL.Repositories
{
    public class UtilisateurRepository : IUtilisatreurRepository
    {
        private readonly Connection _connection;

        public UtilisateurRepository(Connection connection)
        {
            _connection = connection;
        }
        
        public void RegisterUtilisateur(UtilisateurData data)
        {
            Command command = new Command("spUtilisateurRegister", true);
            command.AddParameter("Nom", data.Nom);
            command.AddParameter("Prenom", data.Prenom);
            command.AddParameter("Email", data.Email);
            command.AddParameter("DateNaissance", data.DateNaissance);
            command.AddParameter("Password", data.Password);
            _connection.ExecuteNonQuery(command);
        }
    }
}
