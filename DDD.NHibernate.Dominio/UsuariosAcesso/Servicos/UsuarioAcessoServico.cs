using DDD.NHibernate.Dominio.UsuariosAcesso.Entidades;
using DDD.NHibernate.Dominio.UsuariosAcesso.Excecoes;
using DDD.NHibernate.Dominio.UsuariosAcesso.Repositorios;
using DDD.NHibernate.Dominio.UsuariosAcesso.Servicos.Interfaces;
using DDD.NHibernate.Libs.Dominio.Excecoes;
using System.Linq;

namespace DDD.NHibernate.Dominio.UsuariosAcesso.Servicos
{
    public class UsuarioAcessoServico : IUsuarioAcessoServico
    {
        private readonly IUsuarioAcessoRepositorio usuarioAcessoRepositorio;

        public UsuarioAcessoServico(IUsuarioAcessoRepositorio usuarioAcessoRepositorio)
        {
            this.usuarioAcessoRepositorio = usuarioAcessoRepositorio;
        }

        public SessaoAcesso Autenticar(string login, string senha)
        {
            var hashSenha = usuarioAcessoRepositorio.CriptografarSenhaAcesso(login, senha);
            UsuarioAcesso usuario = usuarioAcessoRepositorio.ListarTodos()
                .Where(x => x.Senha == hashSenha)
                .FirstOrDefault();

            if(usuario == null)
            {
                throw new AutenticacaoInvalidaExcecao();
            }

            var sessao = new SessaoAcesso()
            {
                Codigo = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email
            };

            sessao.Token = usuarioAcessoRepositorio.GerarTokenJwt(sessao);

            return sessao;

        }

        public UsuarioAcesso Instanciar(string nome, string email, string senha, string login)
        {
            UsuarioAcesso usuario = usuarioAcessoRepositorio.ListarTodos()
                .Where(x => x.Email.ToUpper() == email.ToUpper() || x.Login.ToUpper() == login.ToUpper())
                .FirstOrDefault();

            if (usuario != null)
            {
                throw new UsuarioCadastradoExcecao();
            }

            usuario = new UsuarioAcesso(nome, email.ToLower(), login.ToLower());

            var senhaHash = usuarioAcessoRepositorio.CriptografarSenhaAcesso(usuario.Login, senha);

            usuario.SetSenha(senhaHash);

            return usuario;
        }

        public UsuarioAcesso Validar(int id)
        {
            UsuarioAcesso usuario = usuarioAcessoRepositorio.PesquisarPor(id);

            if(usuario == null)
            {
                throw new RegraDeNegocioExcecao("Usuário não encontrado!");
            }

            return usuario;
        }

    }
}
