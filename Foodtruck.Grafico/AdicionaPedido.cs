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
    public partial class AdicionaPedido : Form
    {
        Pedido pedido = new Pedido();
        public Pedido PedidoSelecionado { get; set; }
        public AdicionaPedido()
        {
            InitializeComponent();
        }

        private void AdicionaPedido_Load(object sender, EventArgs e)
        {
            CarregaComboBoxes();
            CarregaPedido();
            CarregaDatagrids();
            CarregaTotal();
        }

        private void CarregaTotal()
        {
            lbTotal.Text = pedido.ValorTotal.ToString();
        }

        private void CarregaComboBoxes()
        {
            cbClientes.DisplayMember = "Descricao";
            cbClientes.ValueMember = "Id";

        //    if (PedidoSelecionado != null)
        //    {
        //        cbClientes.DataSource = PedidoSelecionado.Cliente.Nome.AsEnumerable();
        //    }
        //    else
        //    {
                cbClientes.DataSource = Program.Gerenciador.TodosOsClientes();
        //    }

            cbLanches.DisplayMember = "Nome";
            cbLanches.ValueMember = "Id";
            cbLanches.DataSource = Program.Gerenciador.TodosOsLanches();

            cbBebidas.DisplayMember = "Nome";
            cbBebidas.ValueMember = "Id";
            cbBebidas.DataSource = Program.Gerenciador.TodasAsBebidas();
        }

        private void CarregaDatagrids()
        {
            dgBebidas.AutoGenerateColumns = false;
            dgBebidas.DataSource = pedido.Bebidas.ToList();

            dgLanches.AutoGenerateColumns = false;
            dgLanches.DataSource = pedido.Lanches.ToList();

            CarregaTotal();
        }

        private void btAdicionaBebida_Click(object sender, EventArgs e)
        {
            Bebida bebidaSelecionada = (Bebida)cbBebidas.SelectedItem;
            pedido.Bebidas.Add(bebidaSelecionada);
            CarregaDatagrids();
        }

        private void btAdicionaLanche_Click(object sender, EventArgs e)
        {
            Lanche lancheSelecionado = cbLanches.SelectedItem as Lanche;
            pedido.Lanches.Add(lancheSelecionado);
            CarregaDatagrids();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                pedido.Cliente = cbClientes.SelectedItem as Cliente;
                pedido.DataCompra = DateTime.Now;

                Validacao validacao;
                if (PedidoSelecionado == null)
                {
                    validacao = Program.Gerenciador.CadastraPedido(pedido);
                }
                else
                {
                    validacao = Program.Gerenciador.AlteraPedido(pedido);
                }

                if (validacao.Valido)
                {
                    MessageBox.Show("Pedido cadastrado com sucesso!");
                }
                else
                {
                    String msg = "";
                    foreach (var mensagem in validacao.Mensagens)
                    {
                        msg += mensagem + Environment.NewLine;
                    }
                    MessageBox.Show(msg, "Erro");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro grave, fale com o administrador");
            }

        }
        private bool VerificarSelecaoBebidas()
        {
            if (dgBebidas.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Selecione uma linha");
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool VerificarSelecaoLanches()
        {
            if (dgLanches.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Selecione uma linha");
                return false;
            }
            else
            {
                return true;
            }
        }
        private void btRemoverBebida_Click(object sender, EventArgs e)
        {
            if (VerificarSelecaoBebidas())
            {
                Bebida bebidaSelecionada = (Bebida)dgBebidas.SelectedRows[0].DataBoundItem;
                bebidaSelecionada = cbBebidas.SelectedItem as Bebida;
                pedido.Bebidas.Remove(bebidaSelecionada);
                CarregaDatagrids();
            }


        }

        private void btRemoverLanche_Click(object sender, EventArgs e)
        {
            {
                if (VerificarSelecaoLanches())
                {
                    Lanche lancheSelecionado = (Lanche)dgLanches.SelectedRows[0].DataBoundItem;
                    lancheSelecionado = cbLanches.SelectedItem as Lanche;
                    pedido.Lanches.Remove(lancheSelecionado);
                    CarregaDatagrids();
                }
            }
        }

        private void CarregaPedido()
        {
            if (PedidoSelecionado != null)
            {
                foreach (Bebida bebida in PedidoSelecionado.Bebidas)
                {
                    pedido.Bebidas.Add(bebida);
                }
             
                foreach (Lanche lanche in PedidoSelecionado.Lanches)
                {
                    pedido.Lanches.Add(lanche);
                }
            }
        }
    }
}



