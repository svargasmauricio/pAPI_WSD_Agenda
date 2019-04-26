using MySql.Data.MySqlClient;
using pAPI_WSD_Agenda.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pAPI_WSD_Agenda.Models
{
    public class ServicoDAO
    {
        public Conexao conn { get; set; }

        public ServicoDAO()
        {
            conn = new Conexao();
        }

        public bool Inclui(Servico servico)
        {
            bool ret = true;

            MySqlCommand comando = new MySqlCommand();
            conn.ConexaoMySql.Open();
            comando.Connection = conn.ConexaoMySql;

            String cSQL = @" INSERT INTO SERVICO (ID,DESCRICAO,url_img) ";
            cSQL = cSQL + @" VALUES (@ID, @DESCRICAO,@url_img)";

            comando.Parameters.Clear();
            comando.CommandText = cSQL;

            comando.Parameters.Add("ID", MySqlDbType.Decimal).Value = servico.id;
            comando.Parameters.Add("descricao", MySqlDbType.VarChar).Value = servico.descricao;
            comando.Parameters.Add("url_img", MySqlDbType.VarChar).Value = servico.url_img;

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

        public bool Delete(Servico servico)
        {
            bool ret = true;

            MySqlCommand comando = new MySqlCommand();
            conn.ConexaoMySql.Open();
            comando.Connection = conn.ConexaoMySql;

            String cSQL = @" DELETE FROM SERVICO";
            cSQL = cSQL + @" WHERE SERVICO.ID = @ID";

            comando.Parameters.Clear();
            comando.CommandText = cSQL;

            comando.Parameters.Add("ID", MySqlDbType.Decimal).Value = servico.id;

            try
            {
                int rt = comando.ExecuteNonQuery();
                if (rt == -1)
                {
                    ret = false;
                }
                else
                {
                    cSQL = @" DELETE FROM AGENDASERVICO";
                    cSQL = cSQL + @" WHERE AGENDASERVICO.IDSERVICO = @ID";

                    comando.Parameters.Clear();
                    comando.CommandText = cSQL;

                    comando.Parameters.Add("ID", MySqlDbType.Decimal).Value = servico.id;

                    rt = comando.ExecuteNonQuery();
                    if (rt == -1)
                    {
                        ret = false;
                    }
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
        
        public List<Servico> Buscaservico(List<Object> Filtro)
        {
            List<Servico> listservico = new List<Servico>();

            #region SQL

            string comandoSQL = @" SELECT servico.* FROM SERVICO ";

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
                        Servico servico = new Servico();                        
                        servico.id = Convert.ToInt32(sqlData["ID"]);
                        servico.descricao = sqlData["descricao"].ToString();
                        servico.url_img = sqlData["url_img"].ToString();
                        listservico.Add(servico);
                    }
                }
                catch (Exception e)
                {
                    listservico.Clear();
                }
                finally
                {
                    conn.FechaConexao();
                }

            }

            return listservico;

        }

        public int BuscaSeqServico(List<Object> Filtro)
        {
            List<Servico> listservico = new List<Servico>();

            #region SQL

            string comandoSQL = @" SELECT MAX(ID)+1 MAX FROM SERVICO ";

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
                        return Convert.ToInt32(sqlData["MAX"].ToString());
                    }
                }
                catch (Exception e)
                {
                    listservico.Clear();
                }
                finally
                {
                    conn.FechaConexao();
                }

            }

            return 0;

        }
    }
}