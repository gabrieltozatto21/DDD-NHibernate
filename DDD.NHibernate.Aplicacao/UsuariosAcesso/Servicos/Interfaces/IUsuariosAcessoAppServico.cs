using DDD.NHibernate.DataTransfer.UsuariosAcesso.Request;
using DDD.NHibernate.DataTransfer.UsuariosAcesso.Response;

namespace DDD.NHibernate.Aplicacao.UsuariosAcesso.Servicos.Interfaces
{
    public interface IUsuariosAcessoAppServico
    {
        UsuarioAcessoResponse Cadastrar(UsuarioAcessoRequest request);
        UsuarioAcessoSessaoResponse Autenticar(UsuarioAcessoAutenticacaoRequest request);
    }
}
