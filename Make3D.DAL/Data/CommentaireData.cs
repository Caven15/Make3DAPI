using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Make3D.DAL.Data
{
    public class CommentaireData
    {
        public int Id { get; set; }
        public int Id_utilisateur { get; set; }
        public int Id_article { get; set; }
        public string Commentaire { get; set; }
        public DateTime Date_envoi { get; set; }
        public DateTime Date_modif { get; set; }
    }
}
