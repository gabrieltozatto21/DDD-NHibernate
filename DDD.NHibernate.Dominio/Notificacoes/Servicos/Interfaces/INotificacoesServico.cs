using DDD.NHibernate.Dominio.Notificacoes.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.Dominio.Notificacoes.Servicos.Interfaces
{
    public interface INotificacoesServico
    {
        Notificacao Instanciar(DateTime dataCriacao, DateTime? dataExibicao, string descricao, string link, bool ativo, int tipo);
        Notificacao Inserir(Notificacao notificacao);
        Notificacao Validar(int idNotificacao);
    }
}
