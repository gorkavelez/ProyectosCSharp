using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntranetINDUSAL
{
    public partial class cLineasRecepcion
    {
        #region Variables Privadas

        // variables para las propiedades de la clase
        private int _nLinea;
        private DateTime _fecha;
        private string _codTta;
        private string _nomTta;
        private string _codRuta;
        private string _nomRuta;
        private string _codCte;
        private string _nomCte;
        private string _cantEnt;
        private string _cantRec;
        private string _peso;

        #endregion

        #region Propiedades

        public int nLinea
        {
            get { return _nLinea; }
            set { _nLinea = value; }
        }

        public DateTime fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public string codTta
        {
            get { return _codTta; }
            set { _codTta = value; }
        }

        public string nomTta
        {
            get { return _nomTta; }
            set { _nomTta = value; }
        }

        public string codRuta
        {
            get { return _codRuta; }
            set { _codRuta = value; }
        }

        public string nomRuta
        {
            get { return _nomRuta; }
            set { _nomRuta = value; }
        }

        public string codCte
        {
            get { return _codCte; }
            set { _codCte = value; }
        }

        public string nomCte
        {
            get { return _nomCte; }
            set { _nomCte = value; }
        }

        public string cantEnt
        {
            get { return _cantEnt; }
            set { _cantEnt = value; }
        }

        public string cantRec
        {
            get { return _cantRec; }
            set { _cantRec = value; }
        }

        public string peso
        {
            get { return _peso; }
            set { _peso = value; }
        }
        #endregion

        #region Metodos Publicos
        #endregion

        #region Metodos Privados
        #endregion
    }

    public partial class cRecepcion
    {
        #region Variables Privadas        
            private DataSet _lineasRecepcion;
            private string _userLogin;
            private string _pwdLogin;
        #endregion

        #region Propiedades
        #endregion

        #region Metodos Privados

            private void CrearDataSet()
        {
            
            _lineasRecepcion = new DataSet();
            DataTable newTable = CrearTabla();
            _lineasRecepcion.Tables.Add(newTable);                        
        }

            private DataTable CrearTabla()
            {
                DataTable newTable = new DataTable("LineasRecepcion");
                DataColumn newColumn;

                newColumn = new DataColumn("Linea");
                newColumn.DataType = Type.GetType("System.Int16");
                newColumn.AllowDBNull = false;
                newColumn.AutoIncrement = true;
                newColumn.AutoIncrementSeed = 1;
                newColumn.AutoIncrementStep = 1;            
                newTable.Columns.Add(newColumn);

                //UniqueConstraint myConstraint = new UniqueConstraint(newColumn);
                //newTable.Constraints.Add(myConstraint);

                //newColumn = new DataColumn("Fecha");
                //newColumn.DataType = Type.GetType("System.String");
                //newTable.Columns.Add(newColumn);

                //newColumn = new DataColumn("CodTransportista");
                //newColumn.DataType = Type.GetType("System.String");
                //newTable.Columns.Add(newColumn);

                //newColumn = new DataColumn("Transportista");
                //newColumn.DataType = Type.GetType("System.String");
                //newTable.Columns.Add(newColumn);

                //newColumn = new DataColumn("codRuta");
                //newColumn.DataType = Type.GetType("System.String");
                //newTable.Columns.Add(newColumn);

                //newColumn = new DataColumn("Ruta");
                //newColumn.DataType = Type.GetType("System.String");
                //newTable.Columns.Add(newColumn);

                newColumn = new DataColumn("CodCliente");
                newColumn.DataType = Type.GetType("System.String");
                newTable.Columns.Add(newColumn);

                newColumn = new DataColumn("Cliente");
                newColumn.DataType = Type.GetType("System.String");
                newTable.Columns.Add(newColumn);

                newColumn = new DataColumn("Recibido");
                newColumn.DataType = Type.GetType("System.Int16");
                newTable.Columns.Add(newColumn);

                newColumn = new DataColumn("Entregado");
                newColumn.DataType = Type.GetType("System.Int16");
                newTable.Columns.Add(newColumn);

                newColumn = new DataColumn("Peso");
                newColumn.DataType = Type.GetType("System.Single");
                newTable.Columns.Add(newColumn);

                return (newTable);
            }

            private void ObtenerDatosLogin()
            {
                IntranetINDUSAL.Properties.Settings mySettings = new IntranetINDUSAL.Properties.Settings();
                _userLogin = mySettings.usuarioPruebas;
                _pwdLogin = mySettings.passwordPruebas;
            }
        #endregion

        #region Metodos Publicos

            public cRecepcion()
            {
                //Constructor por defecto de la clase
                CrearDataSet();
                ObtenerDatosLogin();
            }

            public void AddLine(cLineasRecepcion pNewLine)
            {
                DataTable tablaLineas = _lineasRecepcion.Tables["LineasRecepcion"];
                DataRow newRow = tablaLineas.NewRow();
                //DataRow newRow = _lineasRecepcion.NewRow();            
                newRow["Fecha"] = pNewLine.fecha.ToString();
                newRow["CodTransportista"] = pNewLine.codTta;
                newRow["Transportista"] = pNewLine.nomTta;            
                newRow["CodRuta"] = pNewLine.codRuta;
                newRow["Ruta"] = pNewLine.nomRuta;
                newRow["CodCliente"] = pNewLine.codCte;
                newRow["Cliente"] = pNewLine.nomCte;
                newRow["Entregado"] = pNewLine.cantEnt.ToString();
                newRow["Recibido"] = pNewLine.cantRec.ToString();
                newRow["Peso"] = pNewLine.peso.ToString();
                
                tablaLineas.Rows.Add(newRow);            
                //_lineasRecepcion.Rows.Add(newRow);
            }

            public void DeleteLine(Int16 pNLinea)
            {
                DataTable tablaLineas = _lineasRecepcion.Tables["LineasRecepcion"];
                Int16 valor;
                foreach (DataRow oRow in tablaLineas.Rows)
                {
                    valor=Int16.Parse(oRow["Linea"].ToString());
                    if ( valor== pNLinea)
                    {
                        oRow.Delete();
                        return;
                    }
                }
            }

            public void Registrar(string empresa)
            {
                DataTable tablaLineas = _lineasRecepcion.Tables["LineasRecepcion"];
                INIKER.FuncFabricacion.FuncFabricacion fabricacion = 
                    new INIKER.FuncFabricacion.FuncFabricacion(_userLogin,_pwdLogin, empresa);
                
                foreach (DataRow oRow in tablaLineas.Rows)
                {
                    fabricacion.GenerarMovRecepcion(oRow["CodTransportista"].ToString(), oRow["CodCliente"].ToString(),
                        decimal.Parse(oRow["Recibido"].ToString()), decimal.Parse(oRow["Entregado"].ToString()), 
                        decimal.Parse(oRow["Peso"].ToString()));
                }
            }

            public DataTable GetLines()
            {
                return (_lineasRecepcion.Tables["LineasRecepcion"]);
                //return (_lineasRecepcion);
            }
        

        #endregion
    }

}
