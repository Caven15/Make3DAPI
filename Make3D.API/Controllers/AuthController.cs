using Make3D.API.Mapper;
using Make3D.API.Models.Forms.Utilisateur;
using Make3D.BLL.Interfaces;
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

        public AuthController(IUtilisateurService utilisateurService)
        {
            _utilisateurService = utilisateurService;
        }

        [HttpPost(nameof(Register))]
        public IActionResult Register(UtilisateurRegisterForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _utilisateurService.RegisterUtilisateur(form.ApiToBll());
            return Ok();
        }
    }
}
