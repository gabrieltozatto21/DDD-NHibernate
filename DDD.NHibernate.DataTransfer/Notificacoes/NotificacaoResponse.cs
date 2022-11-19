using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.NHibernate.DataTransfer.Notificacoes
{
    public class NotificacaoResponse
    {
        public int Id { get;  set; }
        public DateTime DataCriacao { get;  set; }
        public DateTime? DataExibicao { get;  set; }
        public string Descricao { get;  set; }
        public string Link { get;  set; }
        public bool Ativo { get;  set; }
        public int Tipo { get;  set; } //enum
    }
}
