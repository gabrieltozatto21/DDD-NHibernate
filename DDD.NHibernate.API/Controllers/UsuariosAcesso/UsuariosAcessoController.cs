using DDD.NHibernate.Aplicacao.UsuariosAcesso.Servicos.Interfaces;
using DDD.NHibernate.DataTransfer.UsuariosAcesso.Request;
using DDD.NHibernate.DataTransfer.UsuariosAcesso.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.NHibernate.API.Controllers.UsuariosAcesso
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosAcessoController : ControllerBase
    {

        private IUsuariosAcessoAppServico usuariosAcessoAppServico;

        public UsuariosAcessoController(IUsuariosAcessoAppServico usuariosAcessoAppServico)
        {
            this.usuariosAcessoAppServico = usuariosAcessoAppServico;
        }

        /// <summary>
        /// Cadastro de acesso usuarios
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        //[Authorize]
        public ActionResult<UsuarioAcessoResponse> CadastraUsuario([FromBody] UsuarioAcessoRequest request)
        {
            var response = usuariosAcessoAppServico.Cadastrar(request);

            return Ok(response);
        }

        /// <summary>
        /// Autenticação de usuários
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("autenticar")]
        public ActionResult<UsuarioAcessoSessaoResponse> AutenticarUsuario([FromBody] UsuarioAcessoAutenticacaoRequest request)
        {
            var response = usuariosAcessoAppServico.Autenticar(request);

            return Ok(response);
        }

    }
}
