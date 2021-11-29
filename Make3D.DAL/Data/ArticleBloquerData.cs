using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Make3D.DAL.Data
{
    public class ArticleBloquerData
    {
        public int Id_article { get; set; }
        public int Id_utilisateur { get; set; }
        public string Motivation { get; set; }
        public DateTime Date_bloquage { get; set; }

    }
}
