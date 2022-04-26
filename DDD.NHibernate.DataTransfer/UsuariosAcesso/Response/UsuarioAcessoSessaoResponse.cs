using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.DataTransfer.UsuariosAcesso.Response
{
    public class UsuarioAcessoSessaoResponse
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Jwt { get; set; }
    }
}
