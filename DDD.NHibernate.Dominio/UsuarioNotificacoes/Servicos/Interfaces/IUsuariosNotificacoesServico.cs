using DDD.NHibernate.Dominio.UsuarioNotificacoes.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.Dominio.UsuarioNotificacoes.Servicos.Interfaces
{
    public interface IUsuariosNotificacoesServico
    {
        UsuarioNotificacao Instanciar(int idUsuario, int IdNotificacao);
        UsuarioNotificacao Inserir(UsuarioNotificacao usuarioNotificacao);
        UsuarioNotificacao Atualizar(int codigo, bool visualizado);
        UsuarioNotificacao Validar(int codigo);
    }
}
