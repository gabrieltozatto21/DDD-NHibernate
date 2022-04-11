
using DDD.NHibernate.Dominio.Despesas.Entidades;
using DDD.NHibernate.Dominio.Despesas.Repositorios;
using DDD.NHibernate.Dominio.Despesas.Servicos;
using DDD.NHibernate.Libs.Dominio.Excecoes;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace DDD.NHibernate.Dominio.Testes.Despesas.Servico
{
    public class DespesaServicoTestes
    {
        private readonly DespesaServico sut;
        private readonly Despesa despesaValida;
        private readonly IDespesaRepositorio despesaRepositorio;

        public DespesaServicoTestes()
        {
            despesaValida = Builder<Despesa>.CreateNew().Build();
            despesaRepositorio = Substitute.For<IDespesaRepositorio>();

            sut = new DespesaServico(despesaRepositorio);

        }

        public class AtualizarMetodo : DespesaServicoTestes
        {
            [Fact]
            public void Quando_DespesaNaoForEncontrada_Espero_RegraDeNegocioExcecao()
            {
                despesaRepositorio.PesquisarPor(1).Returns(x => null);
                sut.Invoking(x => x.Validar(1)).Should().Throw<RegraDeNegocioExcecao>();
            }
            [Fact]
            public void Quando_DespesaForEncontrada_Espero_ObjetoIntegro()
            {
                despesaRepositorio.PesquisarPor(1).Returns(x => despesaValida);
                sut.Validar(1).Should().BeSameAs(despesaValida);
            }
        }
    }
}
