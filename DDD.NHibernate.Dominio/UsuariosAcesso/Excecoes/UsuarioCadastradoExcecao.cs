using DDD.NHibernate.Libs.Dominio.Excecoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.Dominio.UsuariosAcesso.Excecoes
{
    public class UsuarioCadastradoExcecao : RegraDeNegocioExcecao
    {
        public UsuarioCadastradoExcecao() : base("Usuário já cadastrado") { }
    }
}
