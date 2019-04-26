using MySql.Data.MySqlClient;
using pAPI_WSD_Agenda.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pAPI_WSD_Agenda.Models
{
    public class AgendaServicoDAO
    {
        public Conexao conn { get; set; }

        public AgendaServicoDAO()
        {
            conn = new Conexao();
        }

        public bool Inclui(AgendaServico AgendaServico)
        {
            bool ret = true;

            String cSQL = @" INSERT INTO AgendaServico (IDSERVICO, DTHR_INI, DTHR_FIM) ";
            cSQL = cSQL + @" VALUES (@IDSERVICO, @DTHR_INI, @DTHR_FIM)";

            MySqlCommand comando = new MySqlCommand();
            conn.ConexaoMySql.Open();
            comando.Connection = conn.ConexaoMySql;
            comando.Parameters.Clear();
            comando.CommandText = cSQL;

            //comando.Parameters.Add("ID", MySqlDbType.Decimal).Value = AgendaServico.id;
            comando.Parameters.Add("idservico", MySqlDbType.Decimal).Value = AgendaServico.servico.id;
            comando.Parameters.Add("dthr_ini", MySqlDbType.DateTime).Value = AgendaServico.dthr_ini;
            comando.Parameters.Add("dthr_fim", MySqlDbType.DateTime).Value = AgendaServico.dthr_fim;

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
                conn.ConexaoMySql.Close();
            }

            return ret;
        }

        public bool Delete(AgendaServico AgendaServico)
        {
            bool ret = true;

            String cSQL = @" DELETE FROM AgendaServico";
            cSQL = cSQL + @" WHERE AgendaServico.ID = @ID";

            MySqlCommand comando = new MySqlCommand();
            conn.ConexaoMySql.Open();
            comando.Connection = conn.ConexaoMySql;
            comando.Parameters.Clear();
            comando.CommandText = cSQL;

            comando.Parameters.Add("id", MySqlDbType.Decimal).Value = AgendaServico.id;

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
                conn.ConexaoMySql.Close();
            }

            return ret;
        }
        
        public List<AgendaServico> BuscaAgendaServico(List<Object> Filtro)
        {
            List<AgendaServico> listAgendaServico = new List<AgendaServico>();

            #region SQL

            string comandoSQL = @" SELECT AgendaServico.* , SERVICO.DESCRICAO, servico.url_img 
                                        FROM AgendaServico, SERVICO 
                                WHERE AGENDASERVICO.IDSERVICO = SERVICO.ID ";

            if (Filtro != null)
            {
                for (int i = 0; i < Filtro.Count; i++)
                {
                    comandoSQL = comandoSQL + " " + Filtro.ElementAt(i);
                }
            }

            #endregion

            MySqlCommand cmd = new MySqlCommand(comandoSQL);
            cmd.Connection = conn.ConexaoMySql;
            MySqlDataReader sqlData = conn.ExecutarConsulta(cmd);

            using (sqlData)
            {
                try
                {
                    while (sqlData.Read())
                    {
                        AgendaServico AgendaServico = new AgendaServico();                        
                        AgendaServico.id = Convert.ToInt32(sqlData["ID"]);

                        AgendaServico.dthr_ini = Convert.ToDateTime(sqlData["dthr_ini"]);
                        AgendaServico.dthr_fim = Convert.ToDateTime(sqlData["dthr_fim"]);

                        AgendaServico.servico = new Servico();
                        AgendaServico.servico.id = Convert.ToInt32(sqlData["IDSERVICO"]);
                        AgendaServico.servico.descricao = sqlData["descricao"].ToString();
                        AgendaServico.servico.url_img = sqlData["url_img"].ToString();
                        listAgendaServico.Add(AgendaServico);                   
                    }
                }
                catch (Exception e)
                {
                    listAgendaServico.Clear();
                }
                finally
                {
                    conn.FechaConexao();
                }
            }

            return listAgendaServico;

        }
    }
}