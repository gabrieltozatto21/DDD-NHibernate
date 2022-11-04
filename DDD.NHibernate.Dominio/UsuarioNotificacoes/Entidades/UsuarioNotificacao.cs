using DDD.NHibernate.Dominio.Notificacoes.Entidades;
using DDD.NHibernate.Dominio.UsuariosAcesso.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.Dominio.UsuarioNotificacoes.Entidades
{
    public class UsuarioNotificacao
    {
        public virtual int Id { get; protected set; }
        public virtual UsuarioAcesso Usuario { get; protected set; }
        public virtual Notificacao Notificacao { get; protected set; }
        public virtual bool Visualizado { get; protected set; }

        protected UsuarioNotificacao()
        {
        }

        public UsuarioNotificacao(UsuarioAcesso usuario, Notificacao notificacao, bool visualizado)
        {
            Usuario = usuario;
            Notificacao = notificacao;
            Visualizado = visualizado;
        }

        public virtual void SetVisualizado(bool visualizado)
        {
            this.Visualizado = visualizado;
        }
    }
}
