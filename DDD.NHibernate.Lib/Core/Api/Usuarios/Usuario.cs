using DDD.NHibernate.Libs.Core.Api.Usuarios.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace DDD.NHibernate.Libs.Core.Api.Usuarios
{
    public class AspNetUser : IUsuario
    {
        private readonly IHttpContextAccessor acessor;
        public AspNetUser(IHttpContextAccessor acessor)
        {
            this.acessor = acessor;
        }
        public string Id => acessor.HttpContext.User.Identity.Name;
        private string _token;
        public string Token => _token ?? acessor.HttpContext.Request.Headers["Token_Autorizacao"];
        public bool IsAuthenticated()
        {
            return acessor.HttpContext.User.Identity.IsAuthenticated;
        }
        public string GetInfo(string name)
        {
            return acessor.HttpContext.User.Claims
                          .Where(x => x.Type == name)
                          .Select(x => x.Value).SingleOrDefault();
        }
        public void SetToken(string token)
        {
            _token = token;
        }
    }
}
