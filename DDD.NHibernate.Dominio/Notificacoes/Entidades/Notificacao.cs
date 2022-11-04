using DDD.NHibernate.Dominio.UsuarioNotificacoes.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.Dominio.Notificacoes.Entidades
{
    public class Notificacao
    {
        public virtual int Id { get; protected set; }
        public virtual DateTime DataCriacao { get; protected set; }
        public virtual DateTime? DataExibicao { get; protected set; }
        public virtual string Descricao { get; protected set; }
        public virtual string Link { get; protected set; }
        public virtual bool Ativo { get; protected set; }
        public virtual int Tipo { get; protected set; } //enum
        public virtual IList<UsuarioNotificacao> UsuarioNotificacoes { get; protected set; }

        protected Notificacao()
        {
        }

        public Notificacao(DateTime dataCriacao, DateTime? dataExibicao, string descricao, string link, bool ativo, int tipo)
        {
            DataCriacao = dataCriacao;
            DataExibicao = dataExibicao;
            Descricao = descricao;
            Link = link;
            Ativo = ativo;
            Tipo = tipo;
        }
    }
}
