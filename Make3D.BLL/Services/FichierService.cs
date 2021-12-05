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
    public class FichierService : IFichierService
    {
        private readonly IFichierRepository _fichierRepository;

        public FichierService(IFichierRepository fichierRepository)
        {
            _fichierRepository = fichierRepository;
        }

        public void Create(FichierModel fichier)
        {
            _fichierRepository.Create(fichier.BllToDal());
        }

        public void Delete(int id)
        {
            _fichierRepository.Delete(id);
        }

        public IEnumerable<FichierModel> GetAll()
        {
            return _fichierRepository.GetAll()?.Select(f => f.DalToBll());
        }

        public IEnumerable<FichierModel> GetByArticleId(int id)
        {
            return _fichierRepository.GetByArticleId(id)?.Select(f => f.DalToBll());
        }

        public FichierModel GetById(int id)
        {
            return _fichierRepository.GetById(id)?.DalToBll();
        }

        public void Update(int id, FichierModel fichier)
        {
            _fichierRepository.Update(id, fichier.BllToDal());
        }
    }
}
