using DDD.NHibernate.Aplicacao.Despesas.Servicos.Interfaces;
using DDD.NHibernate.DataTransfer.Despesas.Request;
using DDD.NHibernate.DataTransfer.Despesas.Response;
using DDD.NHibernate.Dominio.Despesas.Entidades;
using DDD.NHibernate.Libs.Core.Api.Usuarios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.NHibernate.API.Controllers.Despesas
{
    [Route("api/despesas")]
    [ApiController]
    public class DespesasController : ControllerBase
    {
        private readonly IDespesaAppServico despesaAppServico;
        public DespesasController(IDespesaAppServico despesaAppServico)
        {
            this.despesaAppServico = despesaAppServico;
        }
        /// <summary>
        /// Listar Despesas.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<DespesaResponse>> ListarDespesa()
        {
            var response = despesaAppServico.Listar();

            return Ok(response);
        }

        /// <summary>
        /// Adiciona Despesa.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[Authorize]
        public ActionResult<Despesa> CadastrarDespesa([FromBody] DespesaInserirRequest request)
        {
            var response = despesaAppServico.Inserir(request);

            return Ok(response);
        }

        /// <summary>
        /// Deleta uma despesa específica.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult ExcluirDespesa(int id)
        {
            
            despesaAppServico.Excluir(id);

            return Ok();
            
        }
        /// <summary>
        /// Retorna uma despesa especifica.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<DespesaResponse> PesquisarDespesa(int id)
        {

            var resultado = despesaAppServico.Recuperar(id);

            return Ok(resultado);

        }

        /// <summary>
        /// Editar uma Despesa
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Editar(int id, [FromBody] DespesaEditarRequest request)
        {
            var response = despesaAppServico.Editar(id, request);

            return Ok(response);
        }
    }
}
