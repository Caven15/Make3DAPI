using Make3D.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Make3D.BLL.Interfaces
{
    public interface IUtilisateurService
    {
        void RegisterUtilisateur(UtilisateurModel model);

        UtilisateurModel LoginUtilisateur(string email, string password);

        UtilisateurModel GetUtilisateurById(int id);
    }
}
