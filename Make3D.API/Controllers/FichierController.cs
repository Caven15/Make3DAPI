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
        private readonly string _imageDirectory = "images";

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
                _fileService.UploadFile(formFiles, _imageDirectory, articleId, _fichierService);
                return Ok(new { formFiles.Count, Size = _fileService.SizeConverter(formFiles.Sum(f => f.Length)) });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        #region Téléchargement de fichiers

        [HttpGet("Telechargement/{id}")]
        public IActionResult Telechargement(int id)// id => ID du fichier dans la DB
        {
            try
            {
                FichierModel fichierModel = _fichierService.GetById(id);
                if(fichierModel is not null)
                {
                    string filename = fichierModel.Name;
                    byte[] byteFile = _fileService.DownloadFile(_imageDirectory, filename);
                    if(byteFile is not null)
                    {
                        return Ok(File(byteFile, "image/jpeg", filename));
                    }

                }
            }
            catch(Exception e)
            {

            }
            return BadRequest("Not found");
        }

        [HttpPost("Telechargements")]
        public IActionResult Telechargement(int[] id_fichiers)// ids => liste des ID des fichier dans la DB qu'on veut récupérer
        {
            try
            {
                List<FileContentResult> files = new List<FileContentResult>();
                foreach(int id_fichier in id_fichiers)
                {
                    FichierModel fichierModel = _fichierService.GetById(id_fichier);
                    if (fichierModel is not null)
                    {
                        string filename = fichierModel.Name;
                        byte[] byteFile = _fileService.DownloadFile(_imageDirectory, filename);
                        if (byteFile is not null)
                        {
                            files.Add(File(byteFile, "image/jpeg", filename));
                        }

                    }
                }
                return Ok(files);
            }
            catch (Exception e)
            {

            }
            return BadRequest("Not found");
        }
        #endregion
    }
}
