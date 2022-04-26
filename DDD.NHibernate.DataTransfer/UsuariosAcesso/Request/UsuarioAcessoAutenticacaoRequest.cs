using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.DataTransfer.UsuariosAcesso.Request
{
    public class UsuarioAcessoAutenticacaoRequest
    {
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
