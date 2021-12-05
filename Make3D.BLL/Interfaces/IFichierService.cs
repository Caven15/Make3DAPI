using Make3D.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Make3D.BLL.Interfaces
{
    public interface IFichierService
    {
        IEnumerable<FichierModel> GetAll();

        IEnumerable<FichierModel> GetByArticleId(int id);

        FichierModel GetById(int id);

        void Create(FichierModel fichier);

        void Update(int id, FichierModel fichier);

        void Delete(int id);
    }
}
