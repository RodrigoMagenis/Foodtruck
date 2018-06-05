using Foodtruck.Negocio;
using Foodtruck.Negocio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Foodtruck.Grafico
{
    public partial class ManterLanches : Form
    {
        public ManterLanches()
        {
            InitializeComponent();
        }
        public Lanche LancheSelecionado { get; set; }
        Lanche lanche = new Lanche();

        private void btSalvar_Click(object sender, EventArgs e)
        {
            if (Int64.TryParse(tbId.Text, out long longConvertido))
            {
                lanche.Id = longConvertido;
            }
            else
            {
                lanche.Id = -1;
                //passa indentificador com valor negativo se não conseguir converter
            }
            if (decimal.TryParse(tbValor.Text, out decimal decimalConvertido))
            {
                lanche.Valor = decimalConvertido;
            }
                lanche.Nome = tbNome.Text;
                Validacao validacao;
            if (LancheSelecionado == null)
            {
                validacao = Program.Gerenciador.CadastraLanche(lanche);
            }
            else
            {
                validacao = Program.Gerenciador.AlterarLanches(lanche);

            }



            if (!validacao.Valido)
            {
                String mensagemValidacao = "";
                foreach (var chave in validacao.Mensagens.Keys)
                {
                    String msg = validacao.Mensagens[chave];
                    mensagemValidacao += msg;
                    mensagemValidacao += Environment.NewLine;
                }
                MessageBox.Show(mensagemValidacao);
            }
            else
            {
                MessageBox.Show("Lanche salvo com sucesso");
            }

            this.Close();
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ManterLanches_Shown(object sender, EventArgs e)
        {
            if (LancheSelecionado != null)
            {
                this.tbId.Text = LancheSelecionado.Id.ToString();
                this.tbNome.Text = LancheSelecionado.Nome;
                this.tbValor.Text = LancheSelecionado.Valor.ToString();           
            }
        }
    }
    
}
