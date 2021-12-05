using Make3D.API.Models.Forms.Fichier;
using Make3D.API.Tools;
using Make3D.BLL.Interfaces;
using Make3D.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Make3D.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize("IsConnected")]
    [ApiController]
    public class FichierController : ConnectedController
    {
        private readonly IFileService _fileService;
        private readonly IFichierService _fichierService;
        private readonly IArticleService _articleService;

        public FichierController(IFileService fileService, IFichierService fichierService, IArticleService articleService)
        {
            _fileService = fileService;
            _fichierService = fichierService;
            _articleService = articleService;
        }

        [AllowAnonymous] // permet de surpasser Authorize et donner l'accès a tout le monde pour la méthode en cours
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            IEnumerable<FichierModel> fichiers = _fichierService.GetAll();
            return Ok(fichiers);
        }

        [HttpGet("GetByArticle/{id}")]
        public IActionResult GetByArticleId(int id)
        {
            IEnumerable<FichierModel> fichier = _fichierService.GetByArticleId(id);
            return Ok(fichier);
        }

        [HttpGet("{id}/Detail")]
        public IActionResult GetById(int id)
        {
            FichierModel fichier = _fichierService.GetById(id);
            return Ok(fichier);
        }


        [HttpPost("Create/{articleId}")]
        public IActionResult Create(int articleId, List<IFormFile> formFiles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modele de fichier est incorecte...");
            }

            try
            {
                int? userid = GetConnectedUserId();
                if (userid is null) return Unauthorized();// => a activer si on appelle pas authorize ([Authorize("IsConnected")])
                ArticleModel articleModel = _articleService.GetById(articleId);
                if(articleModel.Id_utilisateur != userid) return Unauthorized();//
                //
                _fileService.UploadFile(formFiles, "images", articleId, _fichierService);
                return Ok(new { formFiles.Count, Size = _fileService.SizeConverter(formFiles.Sum(f => f.Length)) });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
