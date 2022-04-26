using AutoMapper;
using DDD.NHibernate.Aplicacao.UsuariosAcesso.Servicos.Interfaces;
using DDD.NHibernate.DataTransfer.UsuariosAcesso.Request;
using DDD.NHibernate.DataTransfer.UsuariosAcesso.Response;
using DDD.NHibernate.Dominio.UsuariosAcesso.Entidades;
using DDD.NHibernate.Dominio.UsuariosAcesso.Repositorios;
using DDD.NHibernate.Dominio.UsuariosAcesso.Servicos.Interfaces;
using DDD.NHibernate.Libs.Aplicacao.Transacoes.Interfaces;
using DDD.NHibernate.Libs.Dominio.Excecoes;

namespace DDD.NHibernate.Aplicacao.UsuariosAcesso.Servicos
{
    public class UsuariosAcessoAppServico : IUsuariosAcessoAppServico
    {
        private readonly IMapper mapper; 
        private readonly IUnitOfWork unitOfWork;
        private readonly IUsuarioAcessoServico usuariosAcessoServico;
        private readonly IUsuarioAcessoRepositorio usuariosAcessoRepositorio;
        public UsuariosAcessoAppServico
        (
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IUsuarioAcessoServico usuariosAcessoServico,
            IUsuarioAcessoRepositorio usuariosAcessoRepositorio
        )
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.usuariosAcessoServico = usuariosAcessoServico;
            this.usuariosAcessoRepositorio = usuariosAcessoRepositorio;

        }

        public UsuarioAcessoSessaoResponse Autenticar(UsuarioAcessoAutenticacaoRequest request)
        {
            if (request == null)
            {
                throw new RegraDeNegocioExcecao("Requisição inválida!");
            }

            try
            {
                this.unitOfWork.BeginTransaction();

                var sessao = usuariosAcessoServico.Autenticar(request.Login, request.Senha);

                this.unitOfWork.Commit();

                var response = new UsuarioAcessoSessaoResponse()
                {
                    Codigo = sessao.Codigo,
                    Nome = sessao.Nome,
                    Jwt = sessao.Token,
                    Email = sessao.Email
                };

                return response;

            }
            catch
            {
                this.unitOfWork.Rollback();
                throw;
            }
        }

        public UsuarioAcessoResponse Cadastrar(UsuarioAcessoRequest request)
        {
            if(request == null)
            {
                throw new RegraDeNegocioExcecao("Requisição inválida!");
            }

            try
            {
                this.unitOfWork.BeginTransaction();
                UsuarioAcesso usuarioAcesso = usuariosAcessoServico.Instanciar(request.Nome, request.Email, request.Senha, request.Login);

                usuariosAcessoRepositorio.Adicionar(usuarioAcesso);

                var response = mapper.Map<UsuarioAcessoResponse>(usuarioAcesso);

                this.unitOfWork.Commit();

                return response;

            }
            catch
            {
                this.unitOfWork.Rollback();
                throw;
            }
        }
    }
}
