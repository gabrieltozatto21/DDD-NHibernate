
using DDD.NHibernate.Dominio.Despesas.Entidades;
using DDD.NHibernate.Libs.Dominio.Excecoes;
using FizzWare.NBuilder;
using FluentAssertions;
using Xunit;

namespace DDD.NHibernate.Dominio.Testes.Despesas.Entidade
{
    public class DespesaTestes
    {
        private readonly Despesa sut;

        public DespesaTestes()
        {
            sut = Builder<Despesa>.CreateNew().Build();
        }

        public class Construtor : DespesaTestes
        {
            [Fact]
            public void Quando_DespesaForInstanciada_Espero_ObjetoIntegro()
            {
                Despesa despesa = new Despesa(sut.Descricao, sut.Tipo, sut.NumPagamentos, sut.ValorTotal);

                despesa.Equals(sut);

            }
        }

        public class SetDescricao : DespesaTestes
        {
            [Fact]
            public void Quando_DescricaoForVazia_Espero_AtributoObrigatorioException()
            {
                sut.Invoking(x => x.SetDescricao("")).Should().Throw<AtributoObrigatorioExcecao>();
            }
            [Fact]
            public void Quando_DescricaoForNula_Espero_AtributoObrigatorioException()
            {
                sut.Invoking(x => x.SetDescricao(null)).Should().Throw<AtributoObrigatorioExcecao>();
            }
            [Fact]
            public void Quando_DescricaoForValida_Espero_PropriedadePreenchida()
            {
                sut.SetDescricao("Despesa Teste");
                sut.Descricao.Should().Be("Despesa Teste");
            }
        }
    }
}
