using Make3D.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Make3D.DAL.Interfaces
{
    public interface IFichierRepository
    {
        IEnumerable<FichierData> GetAll();

        IEnumerable<FichierData> GetByArticleId(int id);

        FichierData GetById(int id);

        void Create(FichierData fichier);

        void Update(int id, FichierData fichier);

        void Delete(int id);
    }
}
