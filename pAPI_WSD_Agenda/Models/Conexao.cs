using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace pAPI_WSD_Agenda.Models
{
    public class Conexao
    {
        public string Sistema = "WEB";
        public string senha = "";
        public string user = "";
        public string conectionString = "server=localhost;user id=root; persist security info=True;database=agenda_wsd";
       // public string conectionString = Program.STRING_CONN;

        public MySqlConnection ConexaoMySql { get; set; }

        public Conexao()
        {
            this.ConexaoMySql = new MySqlConnection(this.conectionString);
        }

        public Conexao(string connectionString)
        {
            if (this.conectionString == null)
                return;
            this.ConexaoMySql = new MySqlConnection(this.conectionString);
        }

        public void FechaConexao()
        {
            if (this.ConexaoMySql == null || !(((object)this.ConexaoMySql.State).ToString() == "Open"))
                return;
            this.ConexaoMySql.Close();
        }

        public MySqlDataReader ExecutarConsulta(MySqlCommand comando)
        {
            MySqlDataReader MySqlDataReader = (MySqlDataReader)null;
            if (this.ConexaoMySql != null)
            {
                if (comando.Connection == null)
                    comando.Connection = this.ConexaoMySql;
                using (comando)
                {
                    try
                    {
                        if (this.ConexaoMySql.State != System.Data.ConnectionState.Open)
                        {
                            this.ConexaoMySql.Open();
                        }

                        MySqlDataReader = comando.ExecuteReader();
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine(ex.Message);
                        this.FechaConexao();
                    }
                }
            }
            return MySqlDataReader;
        }

        public MySqlDataReader ExecutarConsultaConnAberta(MySqlCommand comando)
        {
            MySqlDataReader MySqlDataReader = (MySqlDataReader)null;
            if (this.ConexaoMySql != null)
            {
                if (comando.Connection == null)
                    comando.Connection = this.ConexaoMySql;
                using (comando)
                {
                    try
                    {
                        MySqlDataReader = comando.ExecuteReader();
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine(ex.Message);
                        this.FechaConexao();
                    }
                }
            }
            return MySqlDataReader;
        }

        public int ExecutaMySql(MySqlCommand comando)
        {
            int num = 0;
            if (this.ConexaoMySql != null)
            {
                if (comando.Connection == null)
                    comando.Connection = this.ConexaoMySql;
                using (comando)
                {
                    try
                    {
                        if (((object)this.ConexaoMySql.State).ToString() != "Open")
                            this.ConexaoMySql.Open();
                        num = comando.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        num = -1;
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        this.FechaConexao();
                    }
                }
            }
            return num;
        }


        ////
        public bool AbreTransacao()
        {

            return true;
        }


    }
}