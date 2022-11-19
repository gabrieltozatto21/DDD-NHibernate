using AutoMapper;
using DDD.NHibernate.DataTransfer.UsuariosAcesso.Response;
using DDD.NHibernate.DataTransfer.UsuariosNotificacoes.Response;
using DDD.NHibernate.Dominio.UsuarioNotificacoes.Entidades;
using DDD.NHibernate.Dominio.UsuariosAcesso.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.Aplicacao.UsuariosNotificacoes.Profiles
{
    public class NotificacoesProfile : Profile
    {
        public NotificacoesProfile()
        {
            CreateMap<UsuarioNotificacao, UsuarioNotificacoesResponse>();
        }
    }
}
