using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INIKER.CrossReferences;
using IntranetINDUSAL.Negocio;

namespace IntranetINDUSAL.Controles_Personalizados
{
    public class cINIKER_surtido
    {
        private bool _surtidoFacturacion;
        private int _nivel;
        private string _codCliente;
        private string _empresaLogin;
        private Tipo_Planchado _tipoPlanchado;
        private bool _usarFiltroTipo;


        private cSurtidoCliente oSurtido;

        public cSurtidoCliente Surtido
        {
            get { return oSurtido; }
            set { oSurtido = value; }
        }
        private int _nivelSel;


        private string _codFamilia;
        private string _desFamilia;
        private string _codSubfamilia;
        private string _desSubfamilia;
        private string _codProducto;
        private string _desProducto;

        public bool SurtidoFacturacion
        {
            get { return _surtidoFacturacion; }
            set { _surtidoFacturacion = value; }
        }

        public int Nivel
        {
            get { return _nivel; }
            set { _nivel = value; }
        }

        public string CodCliente
        {
            get { return _codCliente; }
            set { _codCliente = value; }
        }

        public string EmpresaLogin
        {
            get { return _empresaLogin; }
            set { _empresaLogin = value; }
        }

        public Tipo_Planchado TipoPlanchado
        {
            get { return _tipoPlanchado; }
            set
            {
                _tipoPlanchado = value;
                _usarFiltroTipo = true;
            }
        }
        
        public bool UsarFiltroTipo
        {
            get { return _usarFiltroTipo; }
            set { _usarFiltroTipo = value; }
        }
        
        public int NivelSel
        {
            get { return _nivelSel; }
            set { _nivelSel = value; }
        }

        public string CodFamilia
        {
            get { return _codFamilia; }
            set
            {
                _codFamilia = value;
                _nivelSel = 1;
            }
        }

        public string DesFamilia
        {
            get { return _desFamilia; }
            set { _desFamilia = value; }
        }

        public string CodSubfamilia
        {
            get { return _codSubfamilia; }
            set
            {
                _codSubfamilia = value;
                _nivelSel = 2;
            }
        }

        public string DesSubfamilia
        {
            get { return _desSubfamilia; }
            set { _desSubfamilia = value; }
        }

        public string CodProducto
        {
            get { return _codProducto; }
            set { _codProducto = value; }
        }

        public string DesProducto
        {
            get { return _desProducto; }
            set { _desProducto = value; }
        }
    }
}
