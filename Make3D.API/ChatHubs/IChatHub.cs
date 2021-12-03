using Make3D.API.Models.Forms.Commentaire;
using System;
using System.Threading.Tasks;

namespace Make3D.API.ChatsHubs
{
    public interface IChatHub
    {
        void CreateCommentaire(int userId, CommentaireCreateForm form);
        Task OnConnectedAsync();
        Task OnDisconnectedAsync(Exception exception);
    }
}