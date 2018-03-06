using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntranetINDUSAL.Negocio;
using INIKER.CrossReferences;

namespace IntranetINDUSAL.Controles_Personalizados
{
    public partial class INIKER_surtido : System.Web.UI.UserControl
    {
        #region VARIABLES Y PROPIEDADES
            // Variables privadas

            //static bool _surtidoFacturacion;
            //static int _datos.Nivel;
            //static string _datos.CodCliente;
            //static string _datos.EmpresaLogin;
            //static Tipo_Planchado _tipoPlanchado;
            //static bool _usarFiltroTipo;

            //static cSurtidoCliente _datos.Surtido;
            //static int _datos.NivelSel;

            //static string _datos.CodFamilia;
            //static string _datos.DesFamilia;
            //static string _datos.CodSubfamilia;
            //static string _datos.DesSubfamilia;
            //static string _datos.CodProducto;
            //static string _datos.DesProducto;

            private cINIKER_surtido _datos;

            private string _codSeleccion;
            private string _desSeleccion;

            // Propiedades

            public bool SurtidoFacturacion
            {
                get { return _datos.SurtidoFacturacion; }
                set { _datos.SurtidoFacturacion = value; }
            }

            public int Nivel
            {
                get { return _datos.Nivel; }
                set { _datos.Nivel = value; }
            }

            public string CodCliente
            {
                get { return _datos.CodCliente; }
                set { _datos.CodCliente = value; }
            }

            public string EmpresaLogin
            {
                get { return _datos.EmpresaLogin; }
                set { _datos.EmpresaLogin = value; }
            }

            public Tipo_Planchado TipoPlanchado
            {
                get { return _datos.TipoPlanchado; }
                set 
                { 
                    _datos.TipoPlanchado = value;
                    _datos.UsarFiltroTipo = true;
                }
            }

            public string CodFamilia
            {
                get { return _datos.CodFamilia; }
                set 
                { 
                    _datos.CodFamilia = value;
                    _datos.NivelSel = 1;
                }
            }

            public string DesFamilia
            {
                get { return _datos.DesFamilia; }
                set { _datos.DesFamilia = value; }
            }

            public string CodSubfamilia
            {
                get { return _datos.CodSubfamilia; }
                set 
                {
                    _datos.CodSubfamilia = value;
                    _datos.NivelSel = 2;
                }
            }

            public string DesSubfamilia
            {
                get { return _datos.DesSubfamilia; }
                set { _datos.DesSubfamilia = value; }
            }

            public string CodProducto
            {
                get { return _datos.CodProducto; }
                set { _datos.CodProducto = value; }
            }

            public string DesProducto
            {
                get { return _datos.DesProducto; }
                set { _datos.DesProducto = value; }
            }

        #endregion

        #region EVENTOS

            // delegados
            public delegate void OKClickHandler(object sender, OKClickEventArgs e);
            
            //eventos
            public event OKClickHandler OKClick;

            // Clase que encapsula los datos que se pasan en el segundo parámetro del evento
            public class OKClickEventArgs : EventArgs
            {
                private string _codigo;
                private string _descripcion;
                                
                public string Codigo
                {
                    get { return _codigo; }
                    set { _codigo = value; }
                }

                public string Descripcion
                {
                    get { return _descripcion; }
                    set { _descripcion = value; }
                }

                public OKClickEventArgs(string codigo,string descripcion)
                {
                    this._codigo = codigo;
                    this._descripcion = descripcion;
                }

            }

            protected virtual void OnOKClick(OKClickEventArgs e)
            {
                if (OKClick != null)
                {
                    OKClick(this, e);                    
                }
            }

            public void OK_Click(object sender, EventArgs e)
            {
                IButtonControl btn = (IButtonControl)sender;
                // se deja el valor en la propiedad, antes de lanzar el evento
                // se lanza el evento para recogerlo en el objeto contenedor del control
                OKClickEventArgs pcea = new OKClickEventArgs(_codSeleccion,_desSeleccion);
                OnOKClick(pcea);
            }

 

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (_datos == null)
                {
                    _datos = new cINIKER_surtido();
                    _datos.UsarFiltroTipo = false;
                    Session["Surtido"] = _datos;
                }
            }
            else
            {
                _datos = (cINIKER_surtido)Session["Surtido"];
                MostrarBotonesSurtido();
            }
        }
        
        #region METODOS PRIVADOS

            private void MostrarBotonesSurtido()
            {
                switch (_datos.NivelSel)
                {
                    case 0:
                        lbTitSeleccion.Text = "SELECCIONE FAMILIA";
                        MostrarBotonesFamilia();
                        break;
                    case 1:
                        lbTitSeleccion.Text = "SELECCIONE SUBFAMILIA";
                        MostrarBotonesSubfamilia();
                        break;
                    case 2:
                        lbTitSeleccion.Text = "SELECCIONE PRODUCTO";
                        MostrarBotonesProductos();
                        break;
                }                
            }

            private void MostrarBotonesFamilia()
            {
                panelSurtido.Controls.Clear();

                if (_datos.Surtido != null)
                {
                    //string[] familias = _datos.SurtidoSAL.ArrayFamilias();
                    string[] familias = _datos.Surtido.ArrayFamiliasInclCodigo();
                    foreach (string oFam in familias)
                    {
                        string[] datosFamilia = oFam.Split(';');
                        panelSurtido.Controls.Add(CreateButton(datosFamilia[0], "f_" + datosFamilia[0], datosFamilia[1]));
                    }
                }
            }
            
            private void MostrarBotonesSubfamilia()
            {
                panelSurtido.Controls.Clear();

                //if ((_datos.Surtido != null) && (_datos.CodFamilia != ""))
                if ((_datos.Surtido != null) && (_datos.Surtido.famSel != ""))
                {
                    try
                    {
                        string[] subfamilias = _datos.Surtido.ArraySubfamiliasInclCodigo();
                        foreach (string oSubfam in subfamilias)
                        {
                            string[] datosSubfamilia = oSubfam.Split(';');
                            panelSurtido.Controls.Add(CreateButton(datosSubfamilia[0], "s_" + datosSubfamilia[0], datosSubfamilia[1]));
                        }
                    }
                    catch 
                    { }
                }
            }

            private void MostrarBotonesProductos()
            {
                panelSurtido.Controls.Clear();

                if ((_datos.Surtido != null) && (_datos.Surtido.subfamSel != ""))
                {
                    string productos = _datos.Surtido.ArrayProductos("|");
                    string[] arrayProductos = productos.Split('|');

                    foreach (string oProd in arrayProductos)
                    {
                        string[] datosProducto = oProd.Split(';');
                        panelSurtido.Controls.Add(CreateButton(datosProducto[1], "p_" + datosProducto[0], datosProducto[0]));
                    }
                }
            }

        
            private Button CreateButton(string pText, string pID, string code)
            {
                // instancia de objeto
                Button newButton = new Button();
                // establecimiento de propiedades
                newButton.ID = pID;                
                //newButton.Text = pText;
                newButton.Text = PrepararTexto(pText, 15, 2);
                newButton.ToolTip = pText;
                newButton.CssClass = "textoBotonSurtido";

                if (code != "")
                    newButton.CommandName = code;

                newButton.Click += new EventHandler(FamilyButton_Click);

                return (newButton);
            }

            private string PrepararTexto(string initText, int maxLength, int maxLineas)
            {
                // Método para dividir un texto en líneas de longitud determinada
                string[] words;
                string endText = "";
                string linea = "";
                int nLineas = 0;

                if (initText.Length > maxLength)
                {
                    // se separa la cadena original en palabras, utilizando los espacios como separador
                    words = initText.Split(' ');

                    for (int iWord = 0; iWord < words.Length; iWord++)
                    {
                        if ((linea.Length + words[iWord].Length) > maxLength)
                        {
                            endText += (endText.Length == 0) ? "" : "&#10;";
                            endText += linea;
                            nLineas++;
                            linea = "";
                        }

                        linea += (linea.Length == 0) ? "" : " ";
                        linea += words[iWord];
                    }
                    //hay que añadir la última línea generada
                    if (linea.Length > 0)
                    {
                        endText += (endText.Length == 0) ? "" : "&#10;";
                        endText += linea;
                        nLineas++;
                    }
                }
                else
                {
                    endText = initText;
                    nLineas++;
                }

                for (int iLinea = nLineas; iLinea <= maxLineas; iLinea++)
                {
                    endText += "&#10;";
                }

                return (HttpUtility.HtmlDecode(endText));
            }


            protected void FamilyButton_Click(object sender, EventArgs e)
            {
                Button buttonSender = (Button)sender;
                switch (buttonSender.ID.Substring(0, 2))
                {
                    case "f_":
                        if (_datos.Nivel == 0)
                        {
                            _codSeleccion = buttonSender.CommandName;
                            _desSeleccion = buttonSender.ToolTip;
                            OK_Click(sender, e);
                        }
                        else
                        {
                            _datos.CodFamilia = buttonSender.CommandName;
                            _datos.DesFamilia = buttonSender.ToolTip;                            
                            _datos.Surtido.famSel = buttonSender.ToolTip;                           
                            _datos.NivelSel = 1;
                            MostrarBotonesSurtido();
                            MostrarDatos();
                        }
                        break;
                    case "s_":
                        if (_datos.Nivel == 1)
                        {
                            _codSeleccion = buttonSender.CommandName;                            
                            _desSeleccion = buttonSender.ToolTip;
                            OK_Click(sender, e);
                        }
                        else
                        {
                            _datos.CodSubfamilia = buttonSender.CommandName;
                            _datos.DesSubfamilia = buttonSender.ToolTip;
                            _datos.Surtido.subfamSel = buttonSender.ToolTip;
                            _datos.NivelSel = 2;
                            MostrarBotonesSurtido();
                            MostrarDatos();
                        }
                        break;
                    case "p_":
                        if (_datos.Nivel == 2)
                        {
                            _datos.CodProducto = buttonSender.CommandName;
                            _datos.DesProducto = buttonSender.ToolTip;
                            _codSeleccion = buttonSender.CommandName;
                            _desSeleccion = buttonSender.ToolTip;
                            OK_Click(sender, e);
                        }                        
                        break;                    
                }
                Session["Surtido"] = _datos;
            }

            protected void btFamilia_Click(object sender, EventArgs e)
            {
                _datos.CodFamilia = "";
                _datos.DesFamilia = "";
                _datos.CodSubfamilia = "";
                _datos.DesSubfamilia = "";
                _datos.Surtido.famSel = "";
                _datos.Surtido.subfamSel = "";
                _datos.NivelSel = 0;
                MostrarBotonesSurtido();
                MostrarDatos();
                Session["Surtido"] = _datos;
            }

            protected void btSubfamilia_Click(object sender, EventArgs e)
            {
                if (_datos.NivelSel == 2)
                {
                    _datos.CodSubfamilia = "";
                    _datos.DesSubfamilia = "";
                    _datos.Surtido.subfamSel = "";
                    _datos.NivelSel = 1;
                    MostrarBotonesSurtido();
                    MostrarDatos();
                    Session["Surtido"] = _datos;
                }
            }
        
            private void MostrarDatos()
            {
                lbFamilia.Text = _datos.DesFamilia;
                lbSubfamilia.Text = _datos.DesSubfamilia;                
            }
            
            private void InitProperties()
            {                
                _codSeleccion = "";
                _desSeleccion = "";                
            }  
            
            
        #endregion

        #region METODOS PUBLICOS

            public void Load()
        {
            
            if ((_datos.CodCliente != "") && (_datos.EmpresaLogin != ""))
            {

                if (_datos.UsarFiltroTipo)
                    _datos.Surtido = new cSurtidoCliente(_datos.CodCliente, _datos.EmpresaLogin, _datos.TipoPlanchado);                    
                else
                    _datos.Surtido = new cSurtidoCliente(_datos.CodCliente, _datos.EmpresaLogin);
                
                _datos.Surtido.famSel = _datos.DesFamilia;
                _datos.Surtido.subfamSel = _datos.DesSubfamilia;
                InitProperties();
                lbTitSeleccion.Text = "";
                panelSurtido.Controls.Clear();
                MostrarDatos();                
            }
        }

            public void Reset()
        {
            //_datos.UsarFiltroTipo = false;
            if (_datos == null)
            {
                _datos = new cINIKER_surtido();
                _datos.UsarFiltroTipo = false;
                Session["Surtido"] = _datos;
            }
            InitProperties();
        }

        #endregion
    }
}