using AutoMapper;
using DDD.NHibernate.DataTransfer.Notificacoes;
using DDD.NHibernate.DataTransfer.UsuariosAcesso.Response;
using DDD.NHibernate.DataTransfer.UsuariosNotificacoes;
using DDD.NHibernate.Dominio.Notificacoes.Entidades;
using DDD.NHibernate.Dominio.UsuarioNotificacoes.Entidades;
using DDD.NHibernate.Dominio.UsuariosAcesso.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.Aplicacao.Notificacoes.Profiles
{
    public class NotificacoesProfile : Profile
    {
        public NotificacoesProfile()
        {
            CreateMap<Notificacao, NotificacaoResponse>();
        }
    }
}
