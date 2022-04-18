using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.Dominio.UsuariosAcesso.Entidades
{
    public class UsuarioAcesso
    {
        public virtual int Id { get; protected set; }
        public virtual string Nome { get; protected set; }
        public virtual string Email { get; protected set; }
        public virtual string Senha { get; protected set; }
        public virtual string Login { get; protected set; }
        public virtual DateTime DataCadastro { get; protected set; }


    }
}
