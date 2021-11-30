using Make3D.API.Models.Forms.Commentaire;
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
    public class CommentaireController : ConnectedController
    {
        private readonly ICommentaireService _commentaireService;

        public CommentaireController(ICommentaireService commentaireService)
        {
            _commentaireService = commentaireService;
        }

        #region récupération

        // récupération de tout les Commetaires
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            IEnumerable<CommentaireModel> commentaires = _commentaireService.GetAll();
            return Ok(commentaires);
        }

        // récupération de tout les commentaire par l'id de son article
        [HttpGet("GetAllByArticleId/{id}")]
        public IActionResult GetAllByArticleId(int id)
        {
            IEnumerable<CommentaireModel> commentaires = _commentaireService.GetAllByArticleId(id);
            return Ok(commentaires);
        }

        // récupération d'un commentaire par son id 
        [HttpGet("{id}/Detail")]
        public IActionResult GetById(int id)
        {
            CommentaireModel commentaire = _commentaireService.GetById(id);
            return Ok(commentaire);
        }

        #endregion

        #region Création / Modification / Suppression

        // création d'un commentaire 
        [HttpPost("Create")]
        public IActionResult Create(CommentaireCreateForm form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modele d'article incorecte...");
            }

            try
            {
                int? userid = GetConnectedUserId();
                if (userid is null) return Unauthorized();// => a activer si on appelle pas authorize ([Authorize("IsConnected")])
                CommentaireModel commentaireModel = new CommentaireModel
                {
                    Commentaire = form.Commentaire,
                    Id_article = form.Id_article,
                    Id_utilisateur = userid.Value
                };

                _commentaireService.Create(commentaireModel);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // modification d'un commentaire par son créateur
        // /api/Article/{id}/Update
        [HttpPut("{id}/Update")]
        public IActionResult Update(int id, CommentaireUpdateForm form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modele d'article incorecte...");
            }

            try
            {
                int? userid = GetConnectedUserId();
                if (userid is null) return Unauthorized();// => a activer si on appelle pas authorize ([Authorize("IsConnected")])
                CommentaireModel commentaireModel = _commentaireService.GetById(id);

                // Si l'utilisateur connecté est le créateur de l'article
                if (userid.Value == commentaireModel.Id_utilisateur)
                {
                    CommentaireModel updatedCommentaire = new CommentaireModel
                    {
                        Commentaire = form.Commentaire
                    };
                    _commentaireService.Update(id, updatedCommentaire);
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

        // supprime un commentaire par son id 
        [HttpDelete("{id}/Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                int? userid = GetConnectedUserId();
                if (userid is null) return Unauthorized();// => a activer si on appelle pas authorize ([Authorize("IsConnected")])
                CommentaireModel commentaireModel = _commentaireService.GetById(id);

                // Si l'utilisateur connecté est le créateur de l'article
                if (userid.Value == commentaireModel.Id_utilisateur)
                {
                    _commentaireService.Delete(id);
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
    }
}
