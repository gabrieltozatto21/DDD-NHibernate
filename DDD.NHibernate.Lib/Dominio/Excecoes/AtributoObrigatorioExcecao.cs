using System;
using System.Runtime.Serialization;

namespace DDD.NHibernate.Libs.Dominio.Excecoes
{
    [Serializable]
    public class AtributoObrigatorioExcecao : RegraDeNegocioExcecao
    {
        public AtributoObrigatorioExcecao(string atributo) : base(atributo + " é obrigatório")
        {

        }

        protected AtributoObrigatorioExcecao(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
