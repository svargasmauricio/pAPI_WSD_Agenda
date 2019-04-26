using MySql.Data.MySqlClient;
using pAPI_WSD_Agenda.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pAPI_WSD_Agenda.Models
{
    public class ConfirmacaoAgendaServicoDAO
    {
        public Conexao conn { get; set; }

        public ConfirmacaoAgendaServicoDAO()
        {
            conn = new Conexao();
        }

        public bool Inclui(ConfirmacaoAgendaServico ConfirmacaoAgendaServico)
        {
            bool ret = true;

            String cSQL = @" INSERT INTO ConfirmacaoAgendaServico (IDAGENDASERVICO, USUARIO, OBSERVACAO) ";
            cSQL = cSQL + @" VALUES (@IDAGENDASERVICO, @USUARIO, @OBSERVACAO)";

            MySqlCommand comando = new MySqlCommand();
            conn.ConexaoMySql.Open();
            comando.Connection = conn.ConexaoMySql;
            
            comando.Parameters.Clear();
            comando.CommandText = cSQL;

            comando.Parameters.Add("IDAGENDASERVICO", MySqlDbType.Decimal).Value = ConfirmacaoAgendaServico.agendaservico.id;
            comando.Parameters.Add("USUARIO", MySqlDbType.VarChar).Value = ConfirmacaoAgendaServico.usuario;
            comando.Parameters.Add("OBSERVACAO", MySqlDbType.VarChar).Value = ConfirmacaoAgendaServico.observacao;

            try
            {
                int rt = comando.ExecuteNonQuery();
                if (rt == -1)
                {
                    ret = false;
                }
            }
            catch (Exception e)
            {
                ret = false;
            }
            finally
            {
                conn.FechaConexao();
            }

            return ret;
        }

        public bool Delete(ConfirmacaoAgendaServico ConfirmacaoAgendaServico)
        {
            bool ret = true;

            String cSQL = @" DELETE FROM ConfirmacaoAgendaServico ";
            cSQL = cSQL + @" WHERE ConfirmacaoAgendaServico.IDAGENDASERVICO = @IDAGENDASERVICO";

            MySqlCommand comando = new MySqlCommand();
            conn.ConexaoMySql.Open();
            comando.Connection = conn.ConexaoMySql;

            comando.Parameters.Clear();
            comando.CommandText = cSQL;

            comando.Parameters.Add("IDAGENDASERVICO", MySqlDbType.Decimal).Value = ConfirmacaoAgendaServico.agendaservico.id;

            try
            {
                int rt = comando.ExecuteNonQuery();
                if (rt == -1)
                {
                    ret = false;
                }
            }
            catch (Exception e)
            {
                ret = false;
            }
            finally
            {
                conn.FechaConexao();
            }

            return ret;
        }
        
        public List<ConfirmacaoAgendaServico> BuscaConfirmacaoAgendaServico(List<Object> Filtro)
        {
            List<ConfirmacaoAgendaServico> listConfirmacaoAgendaServico = new List<ConfirmacaoAgendaServico>();

            #region SQL

            string comandoSQL = @" SELECT ConfirmacaoAgendaServico.*, SERVICO.DESCRICAO 
                                        FROM ConfirmacaoAgendaServico, AgendaServico, SERVICO 
                                WHERE Servico.id = agendaservico.idservico
                                    AND ConfirmacaoAgendaServico.IDAGENDASERVICO = agendaservico.ID ";

            if (Filtro != null)
            {
                for (int i = 0; i < Filtro.Count; i++)
                {
                    comandoSQL = comandoSQL + " " + Filtro.ElementAt(i);
                }
            }

            #endregion

            MySqlCommand cmd = new MySqlCommand(comandoSQL);

            MySqlDataReader sqlData = conn.ExecutarConsulta(cmd);

            using (sqlData)
            {
                try
                {
                    while (sqlData.Read())
                    {
                        ConfirmacaoAgendaServico ConfirmacaoAgendaServico = new ConfirmacaoAgendaServico();
                        ConfirmacaoAgendaServico.agendaservico = new AgendaServico();
                        ConfirmacaoAgendaServico.agendaservico.id = Convert.ToInt32(sqlData["IDAGENDASERVICO"]);

                        ConfirmacaoAgendaServico.usuario = sqlData["USUARIO"].ToString();
                        ConfirmacaoAgendaServico.observacao = sqlData["observacao"].ToString();
                        
                        listConfirmacaoAgendaServico.Add(ConfirmacaoAgendaServico);
                    }
                }
                catch (Exception e)
                {
                    listConfirmacaoAgendaServico.Clear();
                }
                finally
                {
                    conn.FechaConexao();
                }

            }

            return listConfirmacaoAgendaServico;

        }
    }
}