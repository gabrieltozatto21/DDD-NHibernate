using DDD.NHibernate.Dominio.UsuariosAcesso.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.Dominio.UsuariosAcesso.Servicos.Interfaces
{
    public interface IUsuarioAcessoServico
    {
        SessaoAcesso Autenticar(string login, string senha);
        UsuarioAcesso Instanciar(string nome, string email, string senha, string login);
        UsuarioAcesso Validar(int id);
    }
}
