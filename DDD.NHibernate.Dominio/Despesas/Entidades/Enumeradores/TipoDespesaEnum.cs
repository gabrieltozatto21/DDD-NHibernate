using System.ComponentModel;

namespace DDD.NHibernate.Dominio.Despesas.Entidades.Enumeradores
{
    public enum TipoDespesaEnum
    {
        [Description("Fixa")]
        fixa = 1,
        [Description("Variável")]
        variável = 2,
        [Description("Operacional")]
        operacional = 3
    }
}
