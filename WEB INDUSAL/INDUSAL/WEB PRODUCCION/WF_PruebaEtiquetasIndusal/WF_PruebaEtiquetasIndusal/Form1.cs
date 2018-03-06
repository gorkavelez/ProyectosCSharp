using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WF_PruebaEtiquetasIndusal
{
    public partial class Form1 : Form
    {
        private cPrintDocument oPrinter;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void btEtiqPlegado_Click(object sender, EventArgs e)
        {
            //oPrinter = new cPrintDocument(cboDimensiones.SelectedItem.ToString());
            oPrinter = new cPrintDocument();
            oPrinter.CodCliente = txCodCliente.Text;
            oPrinter.NomCliente = txNomCliente.Text;
            oPrinter.NumPedido = txNumPedido.Text;
            oPrinter.CodProducto = txCodProducto.Text;
            oPrinter.NomProducto = txDescProducto.Text;
            oPrinter.Mensaje = txLiteral.Text;
            oPrinter.Dimensiones = cboDimensiones.SelectedItem.ToString();
            oPrinter.TipoDocumento = cPrintDocument.eTipoDocumento.paqueteProducto;
            oPrinter.Print(int.Parse(txCopias.Text));
        }

        private void btEtiqCliente_Click(object sender, EventArgs e)
        {
            //oPrinter = new cPrintDocument(cboDimensiones.SelectedItem.ToString());
            oPrinter = new cPrintDocument();
            oPrinter.CodCliente = txCodCliente.Text;
            oPrinter.NomCliente = txNomCliente.Text;
            oPrinter.Dimensiones = cboDimensiones.SelectedItem.ToString();
            oPrinter.TipoDocumento = cPrintDocument.eTipoDocumento.carroTransporte;
            oPrinter.Print(int.Parse(txCopias.Text));
        }

        private void btEtiqLavado_Click(object sender, EventArgs e)
        {
            //oPrinter = new cPrintDocument(cboDimensiones.SelectedItem.ToString());
            oPrinter = new cPrintDocument();
            oPrinter.CodCliente = txCodCliente.Text;
            oPrinter.NomCliente = txNomCliente.Text;
            oPrinter.NumPedido = txNumPedido.Text;            
            oPrinter.Mensaje = txLiteral.Text;
            oPrinter.Dimensiones = cboDimensiones.SelectedItem.ToString();
            oPrinter.TipoDocumento = cPrintDocument.eTipoDocumento.carroLavado;
            oPrinter.Print(int.Parse(txCopias.Text));
        }

        private void btEtiqOxido_Click(object sender, EventArgs e)
        {
            //oPrinter = new cPrintDocument(cboDimensiones.SelectedItem.ToString());
            oPrinter = new cPrintDocument();
            oPrinter.CodCliente = txCodCliente.Text;
            oPrinter.NomCliente = txNomCliente.Text;            
            oPrinter.Mensaje = txLiteral.Text;
            oPrinter.Dimensiones = cboDimensiones.SelectedItem.ToString();
            oPrinter.TipoDocumento = cPrintDocument.eTipoDocumento.carroOxido;
            oPrinter.Print(int.Parse(txCopias.Text));
        }

        private void btEtiqIncompleto_Click(object sender, EventArgs e)
        {
            //oPrinter = new cPrintDocument(cboDimensiones.SelectedItem.ToString());
            oPrinter = new cPrintDocument();
            oPrinter.CodCliente = txCodCliente.Text;
            oPrinter.NomCliente = txNomCliente.Text;
            oPrinter.NumPedido = txNumPedido.Text;            
            oPrinter.NCarro = txNCarro.Text;
            oPrinter.Mensaje = txLiteral.Text;
            oPrinter.Dimensiones = cboDimensiones.SelectedItem.ToString();
            oPrinter.TipoDocumento = cPrintDocument.eTipoDocumento.carroIncompleto;
            oPrinter.Print(int.Parse(txCopias.Text));
        }
    }
}
