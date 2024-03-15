using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Prova
{
    class DAL
    {
        private static String strConexao = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BDFarinha.mdb";
        private static OleDbConnection conn = new OleDbConnection(strConexao);
        private static OleDbCommand strSQL;
        private static OleDbDataReader result;

        public static void conecta()
        {
            try
            {
                conn.Open();
            }
            catch (Exception)
            {
                Erro.setMsg("Problemas ao se conectar ao Banco de Dados");
            }
        }

        public static void desconecta()
        {
            conn.Close();
        }

        public static void consultaUmCliente()
        {
            strSQL = new OleDbCommand("SELECT Nome FROM TabClientes WHERE CNPJ = '" + Cliente.getCNPJ() + "'", conn);

            result = strSQL.ExecuteReader();

            if (result.Read())
            {
                Cliente.setNome(result.GetString(0));
            }

            else

            {
                Erro.setMsg("Cliente não encontrado!");
            }

        }

        public static List<string> consultaVendasClienteData(string j)
        {
            List<string> datasVendas = new List<string>();

            strSQL = new OleDbCommand("SELECT " + j + " FROM TabVendasCliente WHERE CNPJ = '" + VendaCliente.getCNPJ() + "'", conn);

            result = strSQL.ExecuteReader();

            while (result.Read())
            {
                datasVendas.Add(result.GetString(0));
            }

            if (datasVendas.Count == 0)
            {
                Erro.setMsg("Cliente não possui " + j + "!");
            }

            return datasVendas; // Retorna a lista de datas 
        }
    }

}

