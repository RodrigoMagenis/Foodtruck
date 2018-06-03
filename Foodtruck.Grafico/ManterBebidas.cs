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
    public partial class ManterBebidas : Form
    {
        public Bebida BebidaSelecionada { get; set; }

        public ManterBebidas()
        {
        InitializeComponent();
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            Bebida bebida = new Bebida();
            if(float.TryParse(tbTamanho.Text, out float floatConvertido))
            {
                bebida.Tamanho = floatConvertido;
            }
            else
            {
                bebida.Tamanho = -1;
            }
            if(decimal.TryParse(tbValor.Text, out decimal decimalConvertido))
            {
                bebida.Valor = decimalConvertido;
            }
            else
            {
                bebida.Valor = -1;
            }
            bebida.Nome = tbNome.Text;
            Validacao validacao;
            if (BebidaSelecionada == null)
            {
                validacao = Program.Gerenciador.CadastraBebida(bebida);
            }
            else
            {
                  validacao = Program.Gerenciador.AlterarBebida(bebida);
               
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
                MessageBox.Show("Bebida salva com sucesso");
            }

            this.Close();
        }

        private void ManterBebidas_Shown(object sender, EventArgs e)
        {
            if (BebidaSelecionada != null)
            {
                this.tbId.Text = BebidaSelecionada.Id.ToString();
                this.tbNome.Text = BebidaSelecionada.Nome;
                this.tbValor.Text = BebidaSelecionada.Valor.ToString();
                this.tbTamanho.Text = BebidaSelecionada.Tamanho.ToString();


            }
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

