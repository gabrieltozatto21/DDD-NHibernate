using AutoMapper;
using DDD.NHibernate.Aplicacao.Despesas.Servicos.Interfaces;
using DDD.NHibernate.DataTransfer.Despesas.Request;
using DDD.NHibernate.DataTransfer.Despesas.Response;
using DDD.NHibernate.Dominio.Despesas.Entidades;
using DDD.NHibernate.Dominio.Despesas.Repositorios;
using DDD.NHibernate.Dominio.Despesas.Servicos.Interfaces;
using DDD.NHibernate.Dominio.Notificacoes.Servicos.Interfaces;
using DDD.NHibernate.Dominio.UsuarioNotificacoes.Servicos.Interfaces;
using DDD.NHibernate.Dominio.UsuariosAcesso.Servicos.Interfaces;
using DDD.NHibernate.Libs.Aplicacao.Transacoes.Interfaces;
using DDD.NHibernate.Libs.Core.Api.Usuarios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.NHibernate.Aplicacao.Despesas.Servicos
{
    public class DespesaAppServico : IDespesaAppServico
    {
        private readonly IMapper mapper;
        private readonly IDespesaServico despesaServico;
        private readonly INotificacoesServico notificacoesServico;
        private readonly IUsuariosNotificacoesServico usuariosNotificacoesServico;
        private readonly IDespesaRepositorio despesaRepositorio;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUsuario usuario;
        private readonly IUsuarioAcessoServico usuarioAcessoServico;

        public DespesaAppServico(
            IDespesaServico despesaServico,
            IDespesaRepositorio despesaRepositorio,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IUsuariosNotificacoesServico usuariosNotificacoesServico,
            INotificacoesServico notificacoesServico,
            IUsuario usuario,
            IUsuarioAcessoServico usuarioAcessoServico)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.despesaServico = despesaServico;
            this.despesaRepositorio = despesaRepositorio;
            this.usuariosNotificacoesServico = usuariosNotificacoesServico;
            this.notificacoesServico = notificacoesServico;
            this.usuario = usuario;
            this.usuarioAcessoServico = usuarioAcessoServico;
        }
        public IEnumerable<DespesaResponse> Listar()
        {
            var query = despesaRepositorio.ListarTodos();

            List<Despesa> resultado = query.ToList();

            var response = mapper.Map<List<DespesaResponse>>(resultado);

            return response;
        }

        public DespesaResponse Inserir(DespesaInserirRequest request)
        {
            try
            {
                unitOfWork.BeginTransaction();

                int idUsuario = Convert.ToInt32(this.usuario.Id);

                //var usuario = usuarioAcessoServico.Validar(idUsuario);

                Despesa despesa = despesaServico.Instanciar(request.Descricao, request.Tipo, request.NumPagamentos, request.ValorTotal, request.DataVencimento);

                //despesa.SetUsuario(usuario);

                despesaRepositorio.Adicionar(despesa);

                //cria notificacao padrão para essa despesa
                CriarNotificacaoParaUsuario(despesa);

                var response = mapper.Map<DespesaResponse>(despesa);

                unitOfWork.Commit();

                return response;

            }
            catch(Exception ex)
            {
                unitOfWork.Rollback();
                throw;
            }
        }
        public DespesaResponse Editar(int id, DespesaEditarRequest request)
        {
            try
            {
                unitOfWork.BeginTransaction();

                Despesa despesa = despesaServico.Atualizar(id, request.Descricao, request.Tipo, request.NumPagamentos, request.ValorTotal);

                despesaRepositorio.Editar(despesa);

                DespesaResponse resultado = mapper.Map<DespesaResponse>(despesa);

                unitOfWork.Commit();

                return resultado;

            }
            catch
            {
                unitOfWork.Rollback();

                throw;
            }
        }

        public void Excluir(int id)
        {
            try
            {
                unitOfWork.BeginTransaction();
                Despesa despesa = despesaServico.Validar(id);

                despesaRepositorio.Remover(despesa);

                unitOfWork.Commit();

            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }

        public DespesaResponse Recuperar(int id)
        {
            Despesa despesa = despesaServico.Validar(id);

            var resultado = mapper.Map<DespesaResponse>(despesa);

            return resultado;
     
        }

        public void AplicaJuros()
        {  
            try
            {
                unitOfWork.BeginTransaction();

                IList<Despesa> despesasAtrasadas = this.despesaServico.AplicaJuros();
                despesaRepositorio.Editar(despesasAtrasadas);


                unitOfWork.Commit();
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }

        public void CriarNotificacaoParaUsuario(Despesa despesa)
        {
            try
            {
                unitOfWork.BeginTransaction();

                var notificacao = notificacoesServico.Instanciar(DateTime.Now, despesa.DataVencimento.AddDays(-1), despesa.Descricao, "", true, 1);
                notificacoesServico.Inserir(notificacao);

                var usuarioNotificacao = usuariosNotificacoesServico.Instanciar(4, notificacao.Id);
                usuariosNotificacoesServico.Inserir(usuarioNotificacao);

                unitOfWork.Commit();

            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }

        }

    }
}
