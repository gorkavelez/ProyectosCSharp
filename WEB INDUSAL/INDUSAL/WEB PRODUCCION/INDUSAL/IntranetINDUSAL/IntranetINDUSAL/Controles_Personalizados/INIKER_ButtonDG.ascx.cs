using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Text;

namespace IntranetINDUSAL
{
    public partial class INIKER_ButtonDG : System.Web.UI.UserControl
    {
        // Publicacion de Eventos Personalizados del control
        // Delegado sobre el que se monta el evento
        public delegate void ProductClickHandler(object sender, ProductClickEventArgs e);

        // Evento ProductClick
        public event ProductClickHandler ProductClick;

        // Clase que encapsula los datos que se pasan en el segundo parámetro del evento
        public class ProductClickEventArgs : EventArgs
        {
            private string prodCode;
            private string prodDesc;                               

            public string ProdCode
            {
                get { return prodCode; }
                set { prodCode = value; }
            }

            public string ProdDesc
            {
                get { return prodDesc; }
                set { prodDesc = value; }
            }

            public ProductClickEventArgs(string prodCode, string prodDesc)
            {
                this.prodCode = prodCode;
                this.prodDesc = prodDesc;
            }

        }

        protected virtual void OnProductClick(ProductClickEventArgs e)
        {
            if (ProductClick != null)
            {
                ProductClick(this, e);
            }
        }



        private DataTable _datos;
        private int _width;
        private System.Drawing.Color _headerBackColor;

        public System.Drawing.Color HeaderBackColor
        {
            get { return _headerBackColor; }
            set { _headerBackColor = value; }
        }

        public DataTable Datos
        {
            get { return _datos; }
            set { _datos = value; }
        }
        
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }     

        #region Funcionalidad DataGrid

        public void GridDataBind()
        {
            grid.DataSource = _datos;
            grid.DataBind();
            GenerarBotonesGrid(grid);
        }


        protected void GenerarBotonesGrid(GridView grd)
        {
            string textoCompleto;
            string[] datosProducto;

            for (int rowIndex = 0; rowIndex < grd.Rows.Count; rowIndex++)
            {
                if (grd.Rows[rowIndex].RowType == DataControlRowType.DataRow)
                {
                    for (int cellIndex = 0; cellIndex < grd.Rows[rowIndex].Cells.Count; cellIndex++)
                    {
                        if (string.Compare(grd.Rows[rowIndex].Cells[cellIndex].Text.ToString(), "&nbsp;") != 0)
                        {
                            Button link = new Button();
                            grd.Rows[rowIndex].Cells[cellIndex].HorizontalAlign = HorizontalAlign.Center;
                            grd.Rows[rowIndex].Cells[cellIndex].Controls.Add(link);
                            textoCompleto = grd.Rows[rowIndex].Cells[cellIndex].Text.ToString();
                            textoCompleto = HttpUtility.HtmlDecode(textoCompleto);
                            datosProducto = textoCompleto.Split(';');
                            link.Text = datosProducto[1];                            
                            link.Font.Size = 8;
                            //link.CssClass="botonGrid";
                            link.Height = 40;
                            link.Width = 180;
                            link.ToolTip = link.Text;
                            link.CommandArgument = datosProducto[0];
                            link.CommandName = "boton_Click";
                            link.Click += new EventHandler(link_Click);
                            link.Attributes.Add("Click", "link_Click");
                        }
                    }

                }
            }
        }

        public void link_Click(object sender, EventArgs e)
        {
            IButtonControl btn = (IButtonControl)sender;
            ProductClickEventArgs pcea = new ProductClickEventArgs(btn.CommandArgument, btn.Text);
            OnProductClick(pcea);
        }

        #endregion Funcionalidad DataGrid

        protected void Page_Load(object sender, EventArgs e)
        {
            GenerarBotonesGrid(grid);
            grid.Width = _width;
            grid.HeaderStyle.BackColor = _headerBackColor;
        }
    }
}