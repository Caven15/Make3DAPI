using Make3D.API.Mapper;
using Make3D.API.Models;
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
        private readonly IFichierService _fichierService;

        public ArticleController(IArticleService articleService, IFichierService fichierService)
        {
            _articleService = articleService;
            _fichierService = fichierService;
        }

        #region Récupération

        [AllowAnonymous] // permet de surpasser Authorize et donner l'accès a tout le monde pour la méthode en cours
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            // Récupération de la liste des articles BLL
            IEnumerable<ArticleModel> articleModels = _articleService.GetAll();
            if(GetConnectedUserId() is null)
            {
                articleModels = articleModels.Where(a => !_articleService.EstBloquer(a.Id));
            }
            else if (IsAdminConnectedUser() is not null && !IsAdminConnectedUser().Value)
            {
                articleModels = articleModels.Where(a => !(_articleService.EstBloquer(a.Id) && a.Id_utilisateur != GetConnectedUserId().Value));
            }

            // Transformation du model BLL vers model API grace au Mapper
            IEnumerable<ArticleAPIModel> articleAPIModels = articleModels.Select(articleModel => articleModel.BllToApî(_fichierService));
            // Retourner le modèle API
            return Ok(articleAPIModels);
        }

        // Obtenir tous les article créés par l'utilisateur {id}
        // /api/Article/GetAllByUserId/{id}
        [HttpGet("GetAllByUser/{id}")]
        public IActionResult GetAllByUserId(int id)
        {
            IEnumerable<ArticleModel> articleModels = _articleService.GetAllByUserId(id);
            if (IsAdminConnectedUser() is not null && !IsAdminConnectedUser().Value)
            {
                articleModels = articleModels.Where(a => !(_articleService.EstBloquer(a.Id) && a.Id_utilisateur != GetConnectedUserId().Value));
            }

            // Transformation du model BLL vers model API grace au Mapper
            IEnumerable<ArticleAPIModel> articleAPIModels = articleModels.Select(articleModel => articleModel.BllToApî(_fichierService));
            // Retourner le modèle API
            return Ok(articleAPIModels);
        }

        //
        [HttpGet("{id}/Detail")]
        public IActionResult GetById(int id)
        {
            ArticleModel articleModel = _articleService.GetById(id);
            if (articleModel is not null // si l'article existe
                && IsAdminConnectedUser() is not null 
                && !IsAdminConnectedUser().Value // si l'utilisateur n'est pas administrateur
                && articleModel.Id_utilisateur != GetConnectedUserId().Value // et si l'utilisateur n'est pas le créateur de l'article
                && _articleService.EstBloquer(id) // et que l'article est bloqué
                )
            {
                return BadRequest("L'article n'est pas disponible");
            }

            // Transformation du model BLL vers model API grace au Mapper
           ArticleAPIModel articleAPIModel = articleModel?.BllToApî(_fichierService);
            // Retourner le modèle API
            return Ok(articleAPIModel);
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

        // /api/Article/{id}/Update
        [HttpPut("{id}/Update")]
        public IActionResult Update(int id, ArticleUpdateForm form)
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

        [HttpDelete("{id}/Delete")]
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

        [HttpPut("{id}/Designaler")]
        public IActionResult Designaler(int id) // Id article
        {
            try
            {
                int? designaleurId = GetConnectedUserId();
                if (designaleurId is null) return Unauthorized("V");// => a activer si on appelle pas authorize ([Authorize("IsConnected")])
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

        [HttpPut("{id}/DesignalerAdmin")]
        public IActionResult DesignalerAdmin(int id) // Id article
        {
            try
            {
                int? designaleurId = GetConnectedUserId();
                if (designaleurId is null) return Unauthorized();// => a activer si on appelle pas authorize ([Authorize("IsConnected")])
                if (_articleService.EstSignale(id) && IsAdminConnectedUser() is not null && IsAdminConnectedUser().Value)
                {
                    _articleService.DesignalerAdmin(id, designaleurId.Value);
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

        [HttpGet("{id}/EstSignale")]
        public IActionResult EstSignale(int id) // Id article
        {
            bool estSignale = _articleService.EstSignale(id);
            return Ok(estSignale);
        }

        [HttpGet("{id}/EstSignalePar/{signaleurId}")]
        public IActionResult EstSignaleParUserId(int id, int signaleurId)
        {
            bool estSignale = _articleService.EstSignaleParUserId(id, signaleurId);
            return Ok(estSignale);
        }

        [HttpGet("{id}/Signalement")]
        public IActionResult Signalement(int id)
        {
            int? signaleurId = GetConnectedUserId();
            if (signaleurId is null) return Unauthorized();// => a activer si on appelle pas authorize ([Authorize("IsConnected")])
            // Récupération de l'article
            ArticleModel articleModel = _articleService.GetById(id);
            // Si l'utilisateur connecté n'est pas le créateur de l'article
            if (signaleurId.Value != articleModel.Id_utilisateur)
            {
                _articleService.Signalement(id, signaleurId.Value);
                return Ok();
            }
            // Si l'utilisateur connecté n'est pas le créateur de l'article
            return BadRequest("id signaleur est différent de id utilisateur...");
        }

        #endregion

        #region Bloquage

        [HttpPost("{id}/Bloquer")]
        public IActionResult Bloquer(int id, ArticleBloquageForm form)
        {
            try
            {
                int? bloqeurId = GetConnectedUserId();
                if (bloqeurId is null) return Unauthorized();// => a activer si on appelle pas authorize ([Authorize("IsConnected")])
                

                // Si l'utilisateur connecté est un administrateur et que l'article n'est pas encore bloqué
                if (IsAdminConnectedUser().HasValue // Si la valeur de IsAdmin existe
                    && IsAdminConnectedUser().Value // et si cette valeur est true (vraie)
                    && !_articleService.EstBloquer(id)) //  et que l'article n'est pas encore bloqué
                {
                    _articleService.Bloquer(id, bloqeurId.Value, form.Motivation);
                    return Ok();
                }
                // Si l'utilisateur connecté n'est pas administrateur ou que l'article est déja bloquer
                return Unauthorized();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/Debloquer")]
        public IActionResult Debloquer(int id)
        {
            try
            {
                int? bloqeurId = GetConnectedUserId();
                if (bloqeurId is null) return Unauthorized();// => a activer si on appelle pas authorize ([Authorize("IsConnected")])


                // Si l'utilisateur connecté est un administrateur et que l'article est bloqué
                if (IsAdminConnectedUser().HasValue // Si la valeur de IsAdmin existe
                    && IsAdminConnectedUser().Value // et si cette valeur est true (vraie)
                    && _articleService.EstBloquer(id)) //  et que l'article est bloqué
                {
                    _articleService.Debloquer(id, bloqeurId.Value);
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

        [HttpGet("{id}/EstBloquer")]
        public IActionResult EstBloquer(int id) // Id article
        {
            bool estBloquer = _articleService.EstBloquer(id);
            return Ok(estBloquer);
        }

        #endregion
    }
}
