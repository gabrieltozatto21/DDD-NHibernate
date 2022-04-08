using DDD.NHibernate.Aplicacao.Despesas.Servicos.Interfaces;
using DDD.NHibernate.DataTransfer.Despesas.Request;
using DDD.NHibernate.DataTransfer.Despesas.Response;
using DDD.NHibernate.Dominio.Despesas.Entidades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.NHibernate.API.Controllers.Despesas
{
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
        [Route("api/despesas")]
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
        [Route("api/despesas")]
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
        [HttpDelete]
        [Route("api/despesas/{id}")]
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
        [HttpGet]
        [Route("api/despesas/{id}")]
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
        [HttpPut]
        [Route("api/despesas/{id}")]
        public ActionResult Editar(int id, [FromBody] DespesaEditarRequest request)
        {
            var response = despesaAppServico.Editar(id, request);

            return Ok(response);
        }
    }
}
