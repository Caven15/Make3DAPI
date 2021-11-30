using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Make3D.API.Models.Forms.Commentaire
{
    public class CommentaireCreateForm
    {
        public int Id_article { get; set; }
        public string Commentaire { get; set; }
    }
}
