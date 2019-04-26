using MySql.Data.MySqlClient;
using pAPI_WSD_Agenda.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pAPI_WSD_Agenda.Models
{
    public class MensagemDAO
    {
        public Conexao conn { get; set; }

        public MensagemDAO()
        {
            conn = new Conexao();
        }

        public bool Inclui(Mensagem mensagem)
        {
            bool ret = true;

            MySqlCommand comando = new MySqlCommand();
            conn.ConexaoMySql.Open();
            comando.Connection = conn.ConexaoMySql;

            String cSQL = @" INSERT INTO MENSAGEM (USUARIO, NOME, SOBRENOME, EMAIL, CONTEUDO)";
            cSQL = cSQL + @" VALUES (@USUARIO, @NOME, @SOBRENOME, @EMAIL, @CONTEUDO)";

            comando.Parameters.Clear();
            comando.CommandText = cSQL;

            //comando.Parameters.Add("ID", MySqlDbType.Decimal).Value = mensagem.id;
            comando.Parameters.Add("USUARIO", MySqlDbType.VarChar).Value = mensagem.usuario;
            comando.Parameters.Add("NOME", MySqlDbType.VarChar).Value = mensagem.nome;
            comando.Parameters.Add("SOBRENOME", MySqlDbType.VarChar).Value = mensagem.sobrenome;
            comando.Parameters.Add("EMAIL", MySqlDbType.VarChar).Value = mensagem.email;
            comando.Parameters.Add("CONTEUDO", MySqlDbType.VarChar).Value = mensagem.conteudo;

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
    }
}