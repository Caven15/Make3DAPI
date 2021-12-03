using Make3D.API.Mapper;
using Make3D.API.Models;
using Make3D.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Make3D.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize("IsConnected")]
    [ApiController]
    public class UtilisateurController : ConnectedController
    {
        private readonly IUtilisateurService _utilisateurService;

        public UtilisateurController(IUtilisateurService utilisateurService)
        {
            _utilisateurService = utilisateurService;
        }


        [HttpGet(nameof(GetOne))]
        public IActionResult GetOne()
        {
            try
            {
                int? userid = GetConnectedUserId();
                // if (userid is null) return Unauthorized(); => a activer si on appelle pas authorize ([Authorize("IsConnected")])
                UtilisateurViewModel utilisateur = _utilisateurService.GetUtilisateurById((int)userid).BllToApi();
                if (utilisateur is null) return NotFound();
                return Ok(utilisateur);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                int? userid = GetConnectedUserId();
                // if (userid is null) return Unauthorized(); => a activer si on appelle pas authorize ([Authorize("IsConnected")])
                UtilisateurViewModel utilisateur = _utilisateurService.GetUtilisateurById(id).BllToApi();
                if (utilisateur is null) return NotFound();
                Object utilisateurPublique = new { Id = utilisateur.Id, Nom = utilisateur.Nom, Prenom = utilisateur.Prenom };
                return Ok(utilisateurPublique);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
