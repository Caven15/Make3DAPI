using Make3D.API.Mapper;
using Make3D.API.Models.Forms.Article;
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
    public class ArticleController : ConnectedController
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        [AllowAnonymous] // permet de surpasser Authorize et donner l'accès a tout le monde pour la méthode en cours
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            IEnumerable<ArticleModel> articles = _articleService.GetAll();
            return Ok(articles);
        }

        [HttpPost("Create")]
        public IActionResult Create(ArticleCreateForm form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modele d'article incorecte...");
            }

            try
            {
                int? userid = GetConnectedUserId();
                if (userid is null) return Unauthorized();// => a activer si on appelle pas authorize ([Authorize("IsConnected")])
                ArticleModel articleModel = new ArticleModel
                {
                    Nom = form.Nom,
                    Description = form.Description,
                    Id_utilisateur = userid.Value
                };

                _articleService.Create(articleModel);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }











            
        }
    }
}
