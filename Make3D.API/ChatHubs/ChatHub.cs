using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Make3D.API.Models.Forms.Commentaire;
using Make3D.BLL.Interfaces;
using Make3D.BLL.Models;
using Microsoft.AspNetCore.SignalR;

namespace Make3D.API.ChatsHubs
{
    public class ChatHub : Hub, IChatHub
    {
        private readonly ICommentaireService _commentaireService;

        public ChatHub(ICommentaireService commentaireService)
        {
            _commentaireService = commentaireService;
        }

        public void CreateCommentaire(int userId, CommentaireCreateForm form)
        {
            CommentaireModel commentaireModel = new CommentaireModel
            {
                Commentaire = form.Commentaire,
                Id_article = form.Id_article,
                Id_utilisateur = userId
            };
            _commentaireService.Create(commentaireModel);
            //
            IEnumerable<CommentaireModel> commentaires = _commentaireService.GetAllByArticleId(form.Id_article);

            Clients.All.SendAsync("recevoirCommentaires", commentaires, form.Id_article);
            
        }


        public override Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            Clients.Client(connectionId).SendAsync("WelcomeMethodName", connectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            return base.OnDisconnectedAsync(exception);
        }
    }
}
