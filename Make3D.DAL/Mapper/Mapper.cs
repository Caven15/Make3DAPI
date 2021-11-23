﻿using Make3D.DAL.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Make3D.DAL.Mapper
{
    internal static class Mapper
    {
        /*--------------------------------------* Utilisateur *----------------------------------------*/

        internal static UtilisateurData DbToUtilisateur(this IDataRecord record)
        {
            return new UtilisateurData()
            {
                Id_utilisateur = (int)record["Id"],
                Nom = (string)record["Nom"],
                Prenom = (string)record["Prenom"],
                Email = (string)record["Email"],
                DateNaissance = (DateTime)record["DateNaissance"],
                IsAdmin = (bool)record["IsAdmin"]
            };
        }
    }
}
