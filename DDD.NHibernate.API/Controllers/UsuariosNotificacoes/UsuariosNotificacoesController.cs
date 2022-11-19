using DDD.NHibernate.Aplicacao.UsuariosAcesso.Servicos;
using DDD.NHibernate.Aplicacao.UsuariosNotificacoes.Servicos.Interfaces;
using DDD.NHibernate.DataTransfer.UsuariosAcesso.Request;
using DDD.NHibernate.DataTransfer.UsuariosAcesso.Response;
using DDD.NHibernate.DataTransfer.UsuariosNotificacoes.Request;
using DDD.NHibernate.DataTransfer.UsuariosNotificacoes.Response;
using DDD.NHibernate.Dominio.UsuarioNotificacoes.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace DDD.NHibernate.API.Controllers.UsuariosNotificacoes
{
    [Route("api/usuarios-notificacoes")]
    [ApiController]
    public class UsuariosNotificacoesController : ControllerBase
    {
        private IUsuariosNotificacoesAppServico usuariosNotificacoesAppServico;

        public UsuariosNotificacoesController(IUsuariosNotificacoesAppServico usuariosNotificacoesAppServico)
        {
            this.usuariosNotificacoesAppServico = usuariosNotificacoesAppServico;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IList<UsuarioNotificacoesResponse>> Notificar([FromQuery] UsuarioNotificacaoRequest request)
        {
            var response = usuariosNotificacoesAppServico.Notificar(request);

            return Ok(response);
        }
    }
}
