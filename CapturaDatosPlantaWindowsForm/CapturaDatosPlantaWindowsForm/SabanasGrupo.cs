using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CapturaDatosPlantaWindowsForm
{
    public partial class SabanasGrupo : Form
    {
        string _usuario;
        string _Grupo = string.Empty;           


        public string usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public string Grupo
        {
            get { return _Grupo; }
            set { _Grupo = value; }
        }
        
        public SabanasGrupo()
        {
            InitializeComponent();
        }

        private void SabanasGrupo_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtSabanas = new DataTable();
                FuncionesWs.FuncionesWs funWebService = new FuncionesWs.FuncionesWs();
                funWebService.traerSabanasGruposNav(ref dtSabanas, Grupo);                
                grdSabanasGrupo.DataSource = dtSabanas;
                DataGridViewButtonColumn EditColumn = new DataGridViewButtonColumn();
                EditColumn.HeaderText = "Sabanas";                
                grdSabanasGrupo.Columns.Add(EditColumn);     

                int indiceId = 0;
                
                foreach (DataGridViewRow line in grdSabanasGrupo.Rows)
                {
                    indiceId = line.Index + 1;
                    Button btnSabana = new Button();                    
                    btnSabana.Text = line.Cells[2].Value.ToString();
                    btnSabana.Click += new EventHandler(btnSabana_Click);                    
                    //btnSabana.CommandArgument = line.Cells[0].Text + ";" + line.Cells[1].Text;//grupo;Sabana                   
                    btnSabana.Name = line.Cells[2].Value.ToString();
                    line.Cells[3].Value = line.Cells[2].Value.ToString();
                    grdSabanasGrupo.Columns[0].Visible = false;
                    grdSabanasGrupo.Columns[1].Visible = false;
                    grdSabanasGrupo.Columns[2].Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        void btnSabana_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void grdSabanasGrupo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {   
            string SabanaOrigen = grdSabanasGrupo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();            
            switch (SabanaOrigen)
            {                
                case "MAQUINA 4":
                    Maquina4 vMaq4 = new Maquina4();
                    vMaq4.usuario = _usuario;
                    vMaq4.sabana = grdSabanasGrupo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    vMaq4.Show();
                    break;

                case "MAQUINA 3":
                    Maquina3 vMaq3 = new Maquina3();
                    vMaq3.usuario = _usuario;
                    vMaq3.sabana = grdSabanasGrupo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    vMaq3.Show();                    
                    break;

                case "REFINOS":
                    REFINOS vRefinos = new REFINOS();
                    vRefinos.usuario = _usuario;
                    vRefinos.sabana = grdSabanasGrupo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    vRefinos.Show();
                    break;
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
