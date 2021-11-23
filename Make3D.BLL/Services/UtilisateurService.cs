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
    public class UtilisateurService : IUtilisateurService
    {
        private readonly IUtilisateurRepository _utilisateurRepository;

        public UtilisateurService(IUtilisateurRepository utilisatreurRepository)
        {
            _utilisateurRepository = utilisatreurRepository;
        }

        public void RegisterUtilisateur(UtilisateurModel model)
        {
            _utilisateurRepository.RegisterUtilisateur(model.BllToDal());
        }

        public UtilisateurModel LoginUtilisateur(string email, string password)
        {
            return _utilisateurRepository.LoginUtilisateur(email, password)?.DalToBll();
        }

        public UtilisateurModel GetUtilisateurById(int id)
        {
            return _utilisateurRepository.GetUtilisateurById(id).DalToBll();
        }
    }
}
