using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class INIKER_ButtonDataGrid : System.Web.UI.UserControl
{
    #region Evento ProductClick

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

    #endregion

    #region Propiedades

    private DataTable _datos; 

    public DataTable datos
    {
        get { return _datos; }
        set { _datos = value; }
    }

    #endregion Propiedades

    #region Funcionalidad DataGrid

    public void GridDataBind()
    {
        grid.DataSource = _datos;
        grid.DataBind();
        GenerarBotonesGrid(grid);
    }

    protected void GenerarBotonesGrid(GridView grd)
    {
        string textoCodificado;

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
                        textoCodificado = grd.Rows[rowIndex].Cells[cellIndex].Text.ToString(); ;
                        link.Text = HttpUtility.HtmlDecode(textoCodificado);
                        link.Font.Size = 8;
                        link.Height = 40;
                        link.Width = 100;
                        link.ToolTip = link.Text;
                        link.CommandArgument = link.Text;
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
        ProductClickEventArgs pcea = new ProductClickEventArgs(btn.CommandArgument, btn.CommandArgument);
        OnProductClick(pcea);
    }

    #endregion Funcionalidad DataGrid

    protected void Page_Load(object sender, EventArgs e)
    {
        GenerarBotonesGrid(grid);
    }
}
