using DDD.NHibernate.Aplicacao.Despesas.Servicos.Interfaces;
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

        [HttpGet]
        [Route("api/despesas")]
        public ActionResult<List<Despesa>> ListarDespesa()
        {
            var response = despesaAppServico.Listar();

            return Ok(response);
        }

        [HttpPost]
        [Route("api/despesas")]
        public ActionResult<Despesa> CadastrarDespesa([FromBody] Despesa request)
        {
            var response = despesaAppServico.Inserir(request);

            return Ok(response);
        }
        [HttpDelete]
        [Route("api/despesas/{id}")]
        public ActionResult ExcluirDespesa(int id)
        {
            try
            {
                despesaAppServico.Excluir(id);

                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
