using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DDD.NHibernate.Libs.Dominio.Excecoes
{
    [Serializable]
    public class AtributoInvalidoExcecao : RegraDeNegocioExcecao
    {
        public AtributoInvalidoExcecao(string atributo) : base(atributo + " inválido")
        {
        }

        protected AtributoInvalidoExcecao(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
