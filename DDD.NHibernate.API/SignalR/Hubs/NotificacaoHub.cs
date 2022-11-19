using DDD.NHibernate.Dominio.UsuariosAcesso.Servicos.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace DDD.NHibernate.API.SignalR.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NotificacaoHub : Hub
    {
        private readonly IUsuarioAcessoServico usuarioAcessoServico;

        public NotificacaoHub(IUsuarioAcessoServico usuarioAcessoServico)
        {
            this.usuarioAcessoServico = usuarioAcessoServico;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task NotificarTodos(string mensagem)
        {
            await Clients.All.SendAsync("Notificar", mensagem);
        }

        public async Task NotificarUsuario(string connectionId, string mensagem)
        {
            if (!string.IsNullOrWhiteSpace(connectionId))
            {
                await Clients.User(connectionId).SendAsync("Notificar", mensagem);
            }
        }
    }
}
