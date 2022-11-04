using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.Libs.Core.Api.Usuarios.Interfaces
{
    public interface IUsuario
    {
        string Id { get; }
        string Token { get; }
        bool IsAuthenticated();
        string GetInfo(string name);
        void SetToken(string token);
    }
}
