using Make3D.API.Infrastructure;
using Make3D.API.Mapper;
using Make3D.API.Models.Forms.Utilisateur;
using Make3D.BLL.Interfaces;
using Make3D.BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Make3D.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUtilisateurService _utilisateurService;
        private readonly TokenManager _tokenManager;

        public AuthController(IUtilisateurService utilisateurService, TokenManager tokenManager)
        {
            _utilisateurService = utilisateurService;
            _tokenManager = tokenManager;
        }

        [HttpPost(nameof(Register))]
        public IActionResult Register(UtilisateurRegisterForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _utilisateurService.RegisterUtilisateur(form.ApiToBll());
            return Ok();
        }

        [HttpPost(nameof(Login))]
        public IActionResult Login(UtilisateurLoginForm form)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                UtilisateurModel currentuser = _utilisateurService.LoginUtilisateur(form.Email, form.Password);
                if (currentuser is null) return NotFound("Utilisateur n'existe pas ...");

                string nom = currentuser.Nom is null ? string.Empty : currentuser.Nom;
                string prenom = currentuser.Prenom is null ? string.Empty : currentuser.Prenom;
                string email = currentuser.Email is null ? string.Empty : currentuser.Email;
                object utilisateur = new
                {
                    Id = currentuser.Id,
                    Nom = currentuser.Nom,
                    Prenom = currentuser.Prenom,
                    Email = currentuser.Email,
                    Token = _tokenManager.GenerateJWT(currentuser)
                };

                return Ok(utilisateur);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
