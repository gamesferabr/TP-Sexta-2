using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.DataVisualization.Charting;

namespace Prova
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BLL.conecta();
            if (Erro.getErro())
                MessageBox.Show(Erro.getMsg());

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            BLL.desconecta();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cliente.setCNPJ(textBox1.Text);
            VendaCliente.setCNPJ(textBox1.Text);
            BLL.validaCNPJ();

            if (Erro.getErro())
            {
                MessageBox.Show(Erro.getMsg());
            }
            else
            {
                textBox2.Text = Cliente.getNome();

                // Determina qual tipo de dados será consultado com base no RadioButton selecionado
                string tipoConsulta = radioButton1.Checked ? "toneladas" : "valor";

                // Consulta as datas das vendas do cliente
                List<string> datas = BLL.consultaVendasClienteData(tipoConsulta);

                if (Erro.getErro())
                {
                    MessageBox.Show(Erro.getMsg());
                }
                else
                {
                    chart1.Series[0].Points.Clear();
                    chart1.Titles.Clear();

                    fillChart(datas);

                    // Somar os valores das toneladas vendidas 

                    if(radioButton1.Checked)
                    {
                        double total = 0;

                        foreach (string tonelada in datas)
                        {
                            total += Convert.ToDouble(tonelada);
                        }
                        textBox3.Text = total.ToString();
                    }
                    else
                    {
                        double total = 0;

                        foreach (string valor in datas)
                        {
                            total += Convert.ToDouble(valor.Replace(",00", ""));
                        }
                        
                        textBox3.Text = total.ToString() + ",00";
                    }
                  
                    
                }
            }
        }

        private void fillChart(List<string> datas)
        {
            // Adiciona um título para o gráfico
            chart1.Titles.Add("Vendas do Cliente");
            
            if (radioButton1.Checked)
            {
                chart1.Series[0].Name = "Toneladas";
            }
            else
            {
                chart1.Series[0].Name = "Valor";

                // replace para remover a formatação monetária
                for (int i = 0; i < datas.Count; i++)
                {
                    datas[i] = datas[i].Replace(",00", "");
                }
            }

            // Adiciona um título para a série
            for (int i = 0; i < datas.Count ; i++)
            {
                chart1.Series[0].Points.AddXY(i, datas[i]);
            }
        }
    }
}
