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
    class UtilisateurService : IUtilisateurService
    {
        private readonly IUtilisatreurRepository _utilisatreurRepository;

        public UtilisateurService(IUtilisatreurRepository utilisatreurRepository)
        {
            _utilisatreurRepository = utilisatreurRepository;
        }

        public void RegisterUtilisateur(UtilisateurModel model)
        {
            _utilisatreurRepository.RegisterUtilisateur(model.BllToDal());
        }
    }
}
