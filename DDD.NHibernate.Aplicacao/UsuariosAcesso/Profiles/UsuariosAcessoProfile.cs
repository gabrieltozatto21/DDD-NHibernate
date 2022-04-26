using AutoMapper;
using DDD.NHibernate.DataTransfer.UsuariosAcesso.Response;
using DDD.NHibernate.Dominio.UsuariosAcesso.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.Aplicacao.UsuariosAcesso.Profiles
{
    public class UsuariosAcessoProfile : Profile
    {
        public UsuariosAcessoProfile()
        {
            CreateMap<UsuarioAcesso, UsuarioAcessoResponse>();
            CreateMap<UsuarioAcesso, UsuarioAcessoSessaoResponse>();

        }
    }
}
