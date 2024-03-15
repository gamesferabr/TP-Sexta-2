using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova
{
    class BLL
    {
        public static void conecta()
        {
            DAL.conecta();
        }

        public static void desconecta()
        {
            DAL.desconecta();
        }

        public static bool isValidoCNPJ(String _cnpj)
        {
            return true;
        }

        public static void validaCNPJ()
        {
            Erro.setErro(false);
            if (Cliente.getCNPJ().Equals(""))
            {
                Erro.setMsg("O código é de preenchimento obrigatório!");
                return;
            }
            if (!isValidoCNPJ(Cliente.getCNPJ()))
            {
                Erro.setMsg("O CNPJ digitado não é válido!");
                return;
            }

            DAL.consultaUmCliente();
        }

        public static List<string> consultaVendasClienteData(string j)
        {
            List<string> datasVendas = DAL.consultaVendasClienteData(j);

            List<string> datas = new List<string>();

            foreach (string data in datasVendas)
            {
                datas.Add(data); // Adiciona cada data ao ListBox 
            }

            Erro.setErro(false);

            if (Erro.getErro())
            {
                Erro.setMsg("Problemas ao consultar as " + j + " do cliente!");
            }
            return datas;
        }
    }
} 