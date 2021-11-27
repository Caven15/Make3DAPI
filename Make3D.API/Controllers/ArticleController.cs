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

        #region Récupération

        [AllowAnonymous] // permet de surpasser Authorize et donner l'accès a tout le monde pour la méthode en cours
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            IEnumerable<ArticleModel> articles = _articleService.GetAll();
            return Ok(articles);
        }

        [HttpGet("GetAllByUserId/{id}")]
        public IActionResult GetAllByUserId(int id)
        {
            IEnumerable<ArticleModel> articles = _articleService.GetAllByUserId(id);
            return Ok(articles);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            ArticleModel article = _articleService.GetById(id);
            return Ok(article);
        }

        #endregion

        #region Création / Modification / Suppression

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

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, ArticleModel form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modele d'article incorecte...");
            }

            try
            {
                int? userid = GetConnectedUserId();
                if (userid is null) return Unauthorized();// => a activer si on appelle pas authorize ([Authorize("IsConnected")])
                ArticleModel articleModel = _articleService.GetById(id);

                // Si l'utilisateur connecté est le créateur de l'article
                if (userid.Value == articleModel.Id_utilisateur)
                {
                    ArticleModel updatedArticle = new ArticleModel
                    {
                        Nom = form.Nom,
                        Description = form.Description
                    };
                    _articleService.Update(id, updatedArticle);
                    return Ok();
                }
                // Si l'utilisateur connecté n'est pas le créateur de l'article
                return Unauthorized();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                int? userid = GetConnectedUserId();
                if (userid is null) return Unauthorized();// => a activer si on appelle pas authorize ([Authorize("IsConnected")])
                ArticleModel articleModel = _articleService.GetById(id);

                // Si l'utilisateur connecté est le créateur de l'article
                if (userid.Value == articleModel.Id_utilisateur)
                {
                    _articleService.Delete(id);
                    return Ok();
                }
                // Si l'utilisateur connecté n'est pas le créateur de l'article
                return Unauthorized();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region Signalements des articles

        [HttpPut("Designaler/{id}")]
        public IActionResult Designaler(int id) // Id article
        {
            try
            {
                int? designaleurId = GetConnectedUserId();
                if (designaleurId is null) return Unauthorized();// => a activer si on appelle pas authorize ([Authorize("IsConnected")])
                if (_articleService.EstSignaleParUserId(id, designaleurId.Value))
                {
                    _articleService.Designaler(id, designaleurId.Value);
                    return Ok();
                }
                // Si l'utilisateur connecté n'est pas le créateur de l'article
                return Unauthorized();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("EstSignale/{id}")]
        public IActionResult EstSignale(int id) // Id article
        {
            bool estSignale = _articleService.EstSignale(id);
            return Ok(estSignale);
        }

        [HttpGet("EstSignaleParUserId/{articleId}/{signaleurId}")]
        public IActionResult EstSignaleParUserId(int articleId, int signaleurId)
        {
            bool estSignale = _articleService.EstSignaleParUserId(articleId, signaleurId);
            return Ok(estSignale);
        }

        [HttpGet("Signalement/{articleId}")]
        public IActionResult Signalement(int articleId)
        {
            int? signaleurId = GetConnectedUserId();
            if (signaleurId is null) return Unauthorized();// => a activer si on appelle pas authorize ([Authorize("IsConnected")])
            // Récupération de l'article
            ArticleModel articleModel = _articleService.GetById(articleId);
            // Si l'utilisateur connecté est le créateur de l'article
            if (signaleurId.Value != articleModel.Id_utilisateur)
            {
                _articleService.Signalement(articleId, signaleurId.Value);
                return Ok();
            }
            // Si l'utilisateur connecté n'est pas le créateur de l'article
            return Unauthorized();
        }

        #endregion

        #region Bloquage

        [HttpPost("Bloquer/{articleId}")]
        public IActionResult Bloquer(int articleId, ArticleBloquageForm form)
        {
            try
            {
                int? bloqeurId = GetConnectedUserId();
                if (bloqeurId is null) return Unauthorized();// => a activer si on appelle pas authorize ([Authorize("IsConnected")])
                

                // Si l'utilisateur connecté est un administrateur et que l'article n'est pas encore bloqué
                if (IsAdminConnectedUser().HasValue // Si la valeur de IsAdmin existe
                    && IsAdminConnectedUser().Value // et si cette valeur est true (vraie)
                    && !_articleService.EstBloquer(articleId)) //  et que l'article n'est pas encore bloqué
                {
                    _articleService.Bloquer(articleId, bloqeurId.Value, form.Motivation);
                    return Ok();
                }
                // Si l'utilisateur connecté n'est pas le créateur de l'article
                return Unauthorized();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Debloquer/{articleId}")]
        public IActionResult Debloquer(int articleId)
        {
            try
            {
                int? bloqeurId = GetConnectedUserId();
                if (bloqeurId is null) return Unauthorized();// => a activer si on appelle pas authorize ([Authorize("IsConnected")])


                // Si l'utilisateur connecté est un administrateur et que l'article est bloqué
                if (IsAdminConnectedUser().HasValue // Si la valeur de IsAdmin existe
                    && IsAdminConnectedUser().Value // et si cette valeur est true (vraie)
                    && _articleService.EstBloquer(articleId)) //  et que l'article est bloqué
                {
                    _articleService.Debloquer(articleId, bloqeurId.Value);
                    return Ok();
                }
                // Si l'utilisateur connecté n'est pas le créateur de l'article
                return Unauthorized();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("EstBloquer/{id}")]
        public IActionResult EstBloquer(int id) // Id article
        {
            bool estBloquer = _articleService.EstBloquer(id);
            return Ok(estBloquer);
        }

        #endregion
    }
}
