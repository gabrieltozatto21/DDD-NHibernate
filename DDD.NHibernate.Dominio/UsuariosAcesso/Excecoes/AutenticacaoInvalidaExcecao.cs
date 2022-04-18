using DDD.NHibernate.Libs.Dominio.Excecoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.Dominio.UsuariosAcesso.Excecoes
{
    public class AutenticacaoInvalidaExcecao : RegraDeNegocioExcecao
    {
        public AutenticacaoInvalidaExcecao() : base("Autenticação inválida") { }
    }
}
