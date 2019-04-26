using pAPI_WSD_Agenda.Filters;
using pAPI_WSD_Agenda.Models;
using pAPI_WSD_Agenda.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace pAPI_WSD_Agenda.Controllers
{
    [RoutePrefix("Servico")]
    public class ServicoController : BaseApiController
    {
        //NOTA: Essa controller é utilizado somente para retornar uma página padrão ao acessar a url da API


        [Route("Add")]
        [HttpPost]
        public CustomActionResult<MessageItem> add_servico(Servico servico)
        {
            
            MessageItem m = new MessageItem();
           
            ServicoDAO servDAO = new ServicoDAO();
            int ID = servDAO.BuscaSeqServico(null);

            servico.id = ID;
            if (!servDAO.Inclui(servico))
            {
                m.Code = "500";
                return GetResultBadRequest(m);
            }
            else
            {
                foreach (var item in servico.lstAgendaServico)
                {
                    item.servico = new Servico();
                    item.servico.id = ID;
                    new AgendaServicoDAO().Inclui(item);
                }
                m.Code = ID+"";
                return GetResultOK(m);
            }
        }

        [Route("ConfAgenda")]
        [HttpPost]
        public CustomActionResult<MessageItem> conf_servico(ConfirmacaoAgendaServico servico)
        {

            MessageItem m = new MessageItem();
            ConfirmacaoAgendaServicoDAO servDAO = new ConfirmacaoAgendaServicoDAO();

            if (!servDAO.Inclui(servico))
            {
                m.Code = "500";
                return GetResultBadRequest(m);
            }
            else
            {
                m.Code = "200";
                return GetResultOK(m);
            }
        }

        [Route("DesConfAgenda")]
        [HttpPost]
        public CustomActionResult<MessageItem> desconf_servico(ConfirmacaoAgendaServico servico)
        {

            MessageItem m = new MessageItem();
            ConfirmacaoAgendaServicoDAO servDAO = new ConfirmacaoAgendaServicoDAO();

            if (!servDAO.Delete(servico))
            {
                m.Code = "500";
                return GetResultBadRequest(m);
            }
            else
            {
                m.Code = "200";
                return GetResultOK(m);
            }
        }

        [Route("DeleteAgendaServico")]
        [HttpPost]
        public CustomActionResult<MessageItem> deleteagenda_servico(AgendaServico agendaservico)
        {

            MessageItem m = new MessageItem();
            AgendaServicoDAO agendaservDAO = new AgendaServicoDAO();

            if (!agendaservDAO.Delete(agendaservico))
            {
                m.Code = "500";
                return GetResultBadRequest(m);
            }
            else
            {
                m.Code = "200";
                return GetResultOK(m);
            }
        }

        [Route("DeleteServico")]
        [HttpPost]
        public CustomActionResult<MessageItem> delete_servico(Servico servico)
        {

            MessageItem m = new MessageItem();
            ServicoDAO servDAO = new ServicoDAO();

            if (!servDAO.Delete(servico))
            {
                m.Code = "500";
                return GetResultBadRequest(m);
            }
            else
            {
                m.Code = "200";
                return GetResultOK(m);
            }
        }

        [Route("EnviaMensagem")]
        [HttpPost]
        public CustomActionResult<MessageItem> envia_mensagem(Mensagem mensagem)
        {
            MessageItem m = new MessageItem();

            MensagemDAO mensagemDAO = new MensagemDAO();
            
            if (!mensagemDAO.Inclui(mensagem))
            {
                m.Code = "500";
                return GetResultBadRequest(m);
            }
            else
            {
                m.Code = "200";
                return GetResultOK(m);
            }
        }

        [Route("Get")]
        [HttpGet]
        public CustomActionResult<getServico> get_servicos()
        {
            getServico get = new getServico();
            get.lstServicos = new ServicoDAO().Buscaservico(null);
            List<object> Filtro = new List<object>();

            foreach (var servico in get.lstServicos)
            {
                Filtro = new List<object>();
                Filtro.Add(" AND agendaservico.idservico = " + servico.id);
                servico.lstAgendaServico = new AgendaServicoDAO().BuscaAgendaServico(Filtro);

                foreach (var item in servico.lstAgendaServico)
                {
                    try
                    {
                        Filtro = new List<object>();
                        Filtro.Add(" AND ConfirmacaoAgendaServico.idagendaservico = " + item.id);
                        item.confirmacao = new ConfirmacaoAgendaServicoDAO().BuscaConfirmacaoAgendaServico(Filtro).First();
                    }
                    catch
                    {
                        item.confirmacao = null;
                    }
                }
            }

            return GetResultOK(get);
        }

        [Route("GetDisponivel")]
        [HttpGet]
        public CustomActionResult<getServico> get_servicos_disp()
        {
            getServico get = new getServico();
            get.lstServicos = new ServicoDAO().Buscaservico(null);
            List<object> Filtro = new List<object>();

            foreach (var servico in get.lstServicos)
            {
                Filtro = new List<object>();
                Filtro.Add(" AND agendaservico.idservico = " + servico.id);
                Filtro.Add(@" AND not exists ( select 0 from 
                                                ConfirmacaoAgendaServico conf
                                            where conf.idagendaservico = agendaservico.id) ");

                servico.lstAgendaServico = new AgendaServicoDAO().BuscaAgendaServico(Filtro);
                
            }

            return GetResultOK(get);
        }
    }
}
