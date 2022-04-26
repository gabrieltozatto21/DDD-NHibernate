using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.DataTransfer.UsuariosAcesso.Request
{
    public class UsuarioAcessoRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Login { get; set; }
    }
}
