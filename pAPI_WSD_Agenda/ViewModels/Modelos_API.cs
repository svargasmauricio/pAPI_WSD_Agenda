using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pAPI_WSD_Agenda.ViewModels
{

    public class getServico : BaseResponse
    {
        public List<Servico> lstServicos { get; set; }
    }

    public class Servico
    {
        public int id { get; set; }
        public String descricao { get; set; }
        public string url_img { get; set; }
        public List<AgendaServico> lstAgendaServico { get; set; }
    }

    public class AgendaServico
    {
        public int id { get; set; }
        public Servico servico { get; set; }
        public DateTime dthr_ini { get; set; }
        public DateTime dthr_fim { get; set; }
        
        public ConfirmacaoAgendaServico confirmacao { get; set; }
    }

    public class ConfirmacaoAgendaServico
    {
        public AgendaServico agendaservico { get; set; }
        public String usuario { get; set; }
        public String observacao { get; set; }
    }

    public class Mensagem
    {
        public int id { get; set; }
        public String usuario { get; set; }
        public String nome { get; set; }
        public String sobrenome { get; set; }
        public String email { get; set; }
        public String conteudo { get; set; }
    }
}