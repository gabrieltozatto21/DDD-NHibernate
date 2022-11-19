using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.DataTransfer.UsuariosNotificacoes.Request
{
    public class UsuarioNotificacaoRequest
    {
        public int? IdUsuario { get; set; }
        public int? IdUsuarioNotificacao { get; set; }
    }
}
