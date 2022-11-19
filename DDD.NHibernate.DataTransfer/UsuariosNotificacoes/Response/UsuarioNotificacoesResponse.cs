using DDD.NHibernate.DataTransfer.Notificacoes;
using DDD.NHibernate.DataTransfer.UsuariosAcesso.Response;
using DDD.NHibernate.Dominio.Notificacoes.Entidades;
using DDD.NHibernate.Dominio.UsuariosAcesso.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.DataTransfer.UsuariosNotificacoes.Response
{
    public class UsuarioNotificacoesResponse
    {
        public int Id { get; set; }
        public UsuarioAcessoResponse Usuario { get; set; }
        public NotificacaoResponse Notificacao { get; set; }
        public bool Notificado { get; set; }
        public bool Visualizado { get; set; }
    }
}
