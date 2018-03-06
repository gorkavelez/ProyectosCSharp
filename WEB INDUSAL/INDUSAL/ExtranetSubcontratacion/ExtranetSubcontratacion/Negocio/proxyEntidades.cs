using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ExtranetSubcontratacion.Negocio
{
    public class proxyEntidades
    {
        public proxyEntidades(string empresa)
        {
            // Constructor de clase
            _empresaLogin = empresa;
            ObtenerDatosLogin();
        }

        #region VARIABLES PRIVADAS

        private string _empresaLogin;
        private string _userLogin;
        private string _pwdLogin;

        #endregion

        #region PROPIEDADES

        public string EmpresaLogin
        {
            get { return _empresaLogin; }
            set { _empresaLogin = value; }
        }

        public string UserLogin
        {
            get { return _userLogin; }
            set { _userLogin = value; }
        }

        public string PwdLogin
        {
            get { return _pwdLogin; }
            set { _pwdLogin = value; }
        }

        #endregion

        #region METODOS PRIVADOS

        private void ObtenerDatosLogin()
        {
            ExtranetSubcontratacion.Properties.Settings mySettings = new ExtranetSubcontratacion.Properties.Settings();
            _userLogin = mySettings.usuarioPruebas;
            _pwdLogin = mySettings.passwordPruebas;
        }

        #endregion

        #region EMPRESAS

        public DataTable GetCompanyList()
        {
            INIKER.ListaEmpresas.ListaEmpresas[] listaEmpresas;
            INIKER.ListaEmpresas.ListaEmpresas_Service oCompanyList = new INIKER.ListaEmpresas.ListaEmpresas_Service(_userLogin, _pwdLogin, _empresaLogin);
            listaEmpresas = oCompanyList.ReadMultiple("", 0, 0, "");

            return (ArrayEmpresasToDataTable(listaEmpresas));
        }

        private DataTable ArrayEmpresasToDataTable(INIKER.ListaEmpresas.ListaEmpresas[] listaEmpresas)
        {
            DataTable tabla = new DataTable("empresas");
            DataColumn newColumn;
            DataRow newRow;

            newColumn = tabla.Columns.Add("nombre");
            newColumn.DataType = System.Type.GetType("System.String");
            newColumn.AllowDBNull = false;


            foreach (INIKER.ListaEmpresas.ListaEmpresas empresa in listaEmpresas)
            {
                newRow = tabla.NewRow();
                newRow["nombre"] = empresa.Name;
                tabla.Rows.Add(newRow);
            }

            return (tabla);

        }

        #endregion

        #region PEDIDOS DE COMPRA

            public DataTable GetOpenPurchaseOrder(string vendorCode)
            {
                INIKER.PurchaseOrder.PurchaseOrder[] orderList;
                INIKER.PurchaseOrder.PurchaseOrder_Service pOrderService = new INIKER.PurchaseOrder.PurchaseOrder_Service(_userLogin, _pwdLogin, _empresaLogin);
                orderList = pOrderService.ReadMultiple(vendorCode, 0, 0, "");

                return (OrdersArrayToDataTable(orderList));
            }

            public DataTable GetOpenPurchaseOrder(string vendorCode, string customerCode)
            {
                INIKER.PurchaseOrder.PurchaseOrder[] orderList;
                INIKER.PurchaseOrder.PurchaseOrder_Service pOrderService = new INIKER.PurchaseOrder.PurchaseOrder_Service(_userLogin, _pwdLogin, _empresaLogin);
                orderList = pOrderService.ReadMultiple(vendorCode + ";" + customerCode, 0, 0, "");

                return (OrdersArrayToDataTable(orderList));
            }

            private DataTable OrdersArrayToDataTable(INIKER.PurchaseOrder.PurchaseOrder[] orderList)
            {
                DataTable tabla = new DataTable("pedidos");
                DataColumn newColumn;
                DataRow newRow;

                newColumn = tabla.Columns.Add("numero");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = false;

                newColumn = tabla.Columns.Add("Fecha pedido");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = false;

                newColumn = tabla.Columns.Add("Fecha registro");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = false;

                newColumn = tabla.Columns.Add("Cod cliente final");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = true;

                newColumn = tabla.Columns.Add("Cliente final");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = true;


                foreach (INIKER.PurchaseOrder.PurchaseOrder order in orderList)
                {
                    newRow = tabla.NewRow();
                    newRow["numero"] = order.No;
                    newRow["Fecha pedido"] = order.Order_Date;
                    newRow["Fecha registro"] = order.Posting_Date;
                    newRow["Cod cliente final"] = order.Cliente_final_subcontr;
                    newRow["Cliente final"] = order.Nom_cliente_subcontr;
                    tabla.Rows.Add(newRow);
                }

                return (tabla);

            }

            public DataTable GetPurchOrderLines(string orderNumber)
            {
                INIKER.PurchOrderLine.PurchOrderLine[] orderLines;
                INIKER.PurchOrderLine.PurchOrderLine_Service pOrderLineService = new INIKER.PurchOrderLine.PurchOrderLine_Service(_userLogin, _pwdLogin, _empresaLogin);
                orderLines = pOrderLineService.ReadMultiple(orderNumber, 0, 0, "");

                return (OrderLinesArrayToDataTable(orderLines));
            }

            private DataTable OrderLinesArrayToDataTable(INIKER.PurchOrderLine.PurchOrderLine[] orderLines)
            {
                DataTable tabla = new DataTable("lineasPedido");
                DataColumn newColumn;
                DataRow newRow;

                newColumn = tabla.Columns.Add("LINEA");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = false;

                newColumn = tabla.Columns.Add("PRODUCTO");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = false;

                newColumn = tabla.Columns.Add("DESCRIPCION");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = false;

                newColumn = tabla.Columns.Add("CANTIDAD");
                newColumn.DataType = System.Type.GetType("System.Decimal");
                newColumn.AllowDBNull = false;

                newColumn = tabla.Columns.Add("UD. MEDIDA");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = false;


                foreach (INIKER.PurchOrderLine.PurchOrderLine line in orderLines)
                {
                    newRow = tabla.NewRow();
                    newRow["LINEA"] = line.Line_No;
                    newRow["PRODUCTO"] = line.No;
                    if ((line.Descripcion_Planta != "")&&(line.Descripcion_Planta!=null))
                        newRow["DESCRIPCION"] = line.Descripcion_Planta;
                    else
                        newRow["DESCRIPCION"] = line.Description + " " + line.Description_2;
                    newRow["CANTIDAD"] = line.Quantity;
                    newRow["UD. MEDIDA"] = line.Unit_of_Measure;
                    tabla.Rows.Add(newRow);
                }

                return (tabla);

            }

            public string CreatePurchOrder(string vendorCode, string customerCode, string postingDate)
            {            
                INIKER.FSubcontratacion.Subcontratacion oSubctr = new INIKER.FSubcontratacion.Subcontratacion(_userLogin,_pwdLogin,_empresaLogin);
                return(oSubctr.GenerarPedidoCompra(vendorCode,customerCode, DateTime.Parse(postingDate)));
            }

            public void DeletePurchOrder(string orderNumber)
            {
                INIKER.FSubcontratacion.Subcontratacion oSubctr = new INIKER.FSubcontratacion.Subcontratacion(_userLogin, _pwdLogin, _empresaLogin);
                oSubctr.EliminarPedidoCompra(orderNumber);
            }

            public string LaunchPurchOrder(string orderNumber)
            {
                INIKER.FSubcontratacion.Subcontratacion oSubctr = new INIKER.FSubcontratacion.Subcontratacion(_userLogin, _pwdLogin, _empresaLogin);
                return(oSubctr.LanzarPedidoCompra(orderNumber));
            }

            public void CreatePurchOrderLine(string orderNumber,string vendorCode, string itemCode, decimal quantity)
            {
                INIKER.FSubcontratacion.Subcontratacion oSubctr = new INIKER.FSubcontratacion.Subcontratacion(_userLogin, _pwdLogin, _empresaLogin);
                oSubctr.GenerarLineaPedidoCompra(orderNumber,vendorCode, itemCode,quantity);
            }

            public void DeletePurchOrderLine(string orderNumber, int orderLineNumber)
            {
                INIKER.FSubcontratacion.Subcontratacion oSubctr = new INIKER.FSubcontratacion.Subcontratacion(_userLogin, _pwdLogin, _empresaLogin);
                oSubctr.EliminarLineaPedidoCompra(orderNumber, orderLineNumber);
            }

            public bool ExistKgLine(string orderNumber)
            {
                INIKER.FSubcontratacion.Subcontratacion oSubctr = new INIKER.FSubcontratacion.Subcontratacion(_userLogin, _pwdLogin, _empresaLogin);
                return(oSubctr.ExisteLineaPedidoKilos(orderNumber));
            }

            public void DeleteAllOrderLines(string orderNumber)
            {
                INIKER.FSubcontratacion.Subcontratacion oSubctr = new INIKER.FSubcontratacion.Subcontratacion(_userLogin, _pwdLogin, _empresaLogin);
                oSubctr.EliminarLineasPedidoCompra(orderNumber);
            }

        #endregion

        #region ALBARANES VENTA

            public DataTable GetSalesShipments(string customerCode)
            {
                INIKER.Albaran.AlbaranesSubcontratacion[] shptList;
                INIKER.Albaran.AlbaranesSubcontratacion_Service salesShptService = new INIKER.Albaran.AlbaranesSubcontratacion_Service(_userLogin, _pwdLogin, _empresaLogin);
                shptList = salesShptService.ReadMultiple(customerCode, 0, 0, "");

                return (ShipmentsArrayToDataTable(shptList));
            }
            
            private DataTable ShipmentsArrayToDataTable(INIKER.Albaran.AlbaranesSubcontratacion[] shptList)
            {
                DataTable tabla = new DataTable("pedidos");
                DataColumn newColumn;
                DataRow newRow;

                newColumn = tabla.Columns.Add("numero");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = false;

                newColumn = tabla.Columns.Add("Fecha registro");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = false;

                newColumn = tabla.Columns.Add("Cod cliente final");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = true;

                newColumn = tabla.Columns.Add("Cliente final");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = true;

                newColumn = tabla.Columns.Add("Pedido");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = true;

                newColumn = tabla.Columns.Add("Alb. compra");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = true;

                foreach (INIKER.Albaran.AlbaranesSubcontratacion salesShpt in shptList)
                {
                    newRow = tabla.NewRow();
                    newRow["numero"] = salesShpt.No;
                    newRow["Fecha registro"] = salesShpt.Posting_Date;
                    newRow["Cod cliente final"] = salesShpt.Sell_to_Customer_No;
                    newRow["Cliente final"] = salesShpt.Alias_Cliente;
                    newRow["pedido"] = salesShpt.Pedido_Compra_Subcontr;
                    newRow["Alb. compra"] = salesShpt.Albaran_compra_Subcontr;
                    tabla.Rows.Add(newRow);
                }

                return (tabla);

            }

            public DataTable GetSalesShipmentHeader(string shptNumber)
            {
                INIKER.Albaran.AlbaranesSubcontratacion[] shptList;
                INIKER.Albaran.AlbaranesSubcontratacion_Service salesShptService = new INIKER.Albaran.AlbaranesSubcontratacion_Service(_userLogin, _pwdLogin, _empresaLogin);
                shptList = salesShptService.ReadMultiple(";"+shptNumber, 0, 0, "");

                return (ShipmentHeaderToDataTable(shptList[0]));
            }

            private DataTable ShipmentHeaderToDataTable(INIKER.Albaran.AlbaranesSubcontratacion shptHdr)
            {
                DataTable tabla = new DataTable("cabeceraAlbaran");
                DataColumn newColumn;
                DataRow newRow;

                newColumn = tabla.Columns.Add("numero");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = false;

                newColumn = tabla.Columns.Add("fecha");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = false;
                
                newColumn = tabla.Columns.Add("cliente");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = true;

                newColumn = tabla.Columns.Add("pedido");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = true;

                newColumn = tabla.Columns.Add("direccion");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = true;

                newColumn = tabla.Columns.Add("direccion2");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = true;

                newColumn = tabla.Columns.Add("direccion3");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = true;

                newColumn = tabla.Columns.Add("contacto");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = true;
               
                newRow = tabla.NewRow();
                newRow["numero"] = shptHdr.No;
                newRow["fecha"] = shptHdr.Posting_Date.ToShortDateString();
                newRow["cliente"] = shptHdr.Alias_Cliente;
                newRow["pedido"] = shptHdr.Pedido_Compra_Subcontr;
                newRow["direccion"] = shptHdr.Ship_to_Address + shptHdr.Ship_to_Address_2;
                newRow["direccion2"] = shptHdr.Ship_to_Post_Code + " - " + shptHdr.Ship_to_City;
                newRow["direccion3"] = shptHdr.Ship_to_County;
                newRow["contacto"] = shptHdr.Ship_to_Contact;
                tabla.Rows.Add(newRow);
                

                return (tabla);

            }

            public void UndoSalesShipment(string shptNumber)
            {
                INIKER.FSubcontratacion.Subcontratacion oSubctr = new INIKER.FSubcontratacion.Subcontratacion(_userLogin, _pwdLogin, _empresaLogin);
                oSubctr.DeshacerAlbaranVenta(shptNumber);
            }

            public DataTable GetSalesShptLines(string shptNumber)
            {
                INIKER.LineasAlbaran.LineasAlbaranVenta[] shptLines;
                INIKER.LineasAlbaran.LineasAlbaranVenta_Service pShptLineService = new INIKER.LineasAlbaran.LineasAlbaranVenta_Service(_userLogin, _pwdLogin, _empresaLogin);
                shptLines = pShptLineService.ReadMultiple(shptNumber, 0, 0, "");

                return (ShipmentLinesArrayToDataTable(shptLines));
            }

            private DataTable ShipmentLinesArrayToDataTable(INIKER.LineasAlbaran.LineasAlbaranVenta[] shptLines)
            {
                DataTable tabla = CreateShipmentLineDataTable();                
                DataRow newRow;              

                foreach (INIKER.LineasAlbaran.LineasAlbaranVenta line in shptLines)
                {
                    newRow = tabla.NewRow();
                    newRow["LINEA"] = line.Line_No;
                    newRow["PRODUCTO"] = line.No;
                    newRow["DESCRIPCION"] = line.Description + " " + line.Description_2;
                    newRow["CANTIDAD"] = line.Quantity;
                    newRow["UD. MEDIDA"] = line.Unit_of_Measure_Code;
                    tabla.Rows.Add(newRow);
                }

                return (tabla);

            }

            public DataTable CreateShipmentLineDataTable()
            {
                DataTable tabla = new DataTable("lineasAlbaran");
                DataColumn newColumn;                

                newColumn = tabla.Columns.Add("LINEA");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = false;

                newColumn = tabla.Columns.Add("PRODUCTO");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = false;

                newColumn = tabla.Columns.Add("DESCRIPCION");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = false;

                newColumn = tabla.Columns.Add("CANTIDAD");
                newColumn.DataType = System.Type.GetType("System.Decimal");
                newColumn.AllowDBNull = true;

                newColumn = tabla.Columns.Add("UD. MEDIDA");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = false;

                return (tabla);
            }

        #endregion

        #region SURTIDO CLIENTE

        public DataTable GetCustCrossRefs(string customerCode)
        {
            INIKER.Surtido.SurtidosCompletos[] refList;
            INIKER.Surtido.SurtidosCompletos_Service crossRefsService = new INIKER.Surtido.SurtidosCompletos_Service(_userLogin, _pwdLogin, _empresaLogin);
            refList = crossRefsService.ReadMultiple(customerCode, 0, 0, "");

            return (CrossRefsArrayToDataTable(refList));
        }

        private DataTable CrossRefsArrayToDataTable(INIKER.Surtido.SurtidosCompletos[] refList)
        {
            DataTable tabla = new DataTable("referenciasCliente");
            DataColumn newColumn;
            DataRow newRow;

            newColumn = tabla.Columns.Add("CodProducto");
            newColumn.DataType = System.Type.GetType("System.String");
            newColumn.AllowDBNull = false;

            newColumn = tabla.Columns.Add("DescrProducto");
            newColumn.DataType = System.Type.GetType("System.String");
            newColumn.AllowDBNull = false;

            newColumn = tabla.Columns.Add("CodFamilia");
            newColumn.DataType = System.Type.GetType("System.String");
            newColumn.AllowDBNull = false;

            newColumn = tabla.Columns.Add("DescrFamilia");
            newColumn.DataType = System.Type.GetType("System.String");
            newColumn.AllowDBNull = false;

            newColumn = tabla.Columns.Add("CodSubfamilia");
            newColumn.DataType = System.Type.GetType("System.String");
            newColumn.AllowDBNull = false;

            newColumn = tabla.Columns.Add("DescrSubfamilia");
            newColumn.DataType = System.Type.GetType("System.String");
            newColumn.AllowDBNull = false;

            newColumn = tabla.Columns.Add("Cantidad");
            newColumn.DataType = System.Type.GetType("System.Decimal");
            newColumn.AllowDBNull = true;

            foreach (INIKER.Surtido.SurtidosCompletos crossRef in refList)
            {
                newRow = tabla.NewRow();
                newRow["CodProducto"] = crossRef.Cross_Reference_No;
                newRow["DescrProducto"] = crossRef.Descripcion_Planta;
                newRow["CodFamilia"] = crossRef.Cod_categ_prod;
                newRow["DescrFamilia"] = crossRef.Desc_categ_prod;
                newRow["CodSubfamilia"] = crossRef.Cod_grupo_prod;
                newRow["DescrSubfamilia"] = crossRef.Desc_grupo_prod;
                newRow["Cantidad"] = 0;
                tabla.Rows.Add(newRow);
            }

            return (tabla);

        }


        #endregion

        #region CLIENTES

        public DataTable GetCustomerList(string vendorCode)
        {
            INIKER.Cliente.ClientesSubcontratados[] custList;
            INIKER.Cliente.ClientesSubcontratados_Service custListService = new INIKER.Cliente.ClientesSubcontratados_Service(_userLogin, _pwdLogin, _empresaLogin);
            custList = custListService.ReadMultiple(vendorCode, 0, 0, "");

            return (CustomerArrayToDataTable(custList));
        }

        private DataTable CustomerArrayToDataTable(INIKER.Cliente.ClientesSubcontratados[] customerList)
        {
            DataTable tabla = new DataTable("clientes");
            DataColumn newColumn;
            DataRow newRow;

            newColumn = tabla.Columns.Add("codigo");
            newColumn.DataType = System.Type.GetType("System.String");
            newColumn.AllowDBNull = false;

            newColumn = tabla.Columns.Add("nombre");
            newColumn.DataType = System.Type.GetType("System.String");
            newColumn.AllowDBNull = false;


            foreach (INIKER.Cliente.ClientesSubcontratados customer in customerList)
            {
                newRow = tabla.NewRow();
                newRow["codigo"] = customer.No;
                newRow["nombre"] = customer.Search_Name;
                tabla.Rows.Add(newRow);
            }

            return (tabla);

        }
        #endregion

        #region COMPANY INFORMATION

            public DataTable GetCompanyInformation()
            {
                INIKER.CompanyInformation.CompanyInformation[] companyInfo;
                INIKER.CompanyInformation.CompanyInformation_Service sCompanyInfo =
                    new INIKER.CompanyInformation.CompanyInformation_Service(_userLogin, _pwdLogin, _empresaLogin);
                companyInfo = sCompanyInfo.ReadMultiple("", 0, 0, "");

                return (InformacionEmpresaToDataTable(companyInfo[0]));
            }

            private DataTable InformacionEmpresaToDataTable(INIKER.CompanyInformation.CompanyInformation companyInfo)
            {
                DataTable tabla = new DataTable("informacionEmpresa");
                DataColumn newColumn;
                DataRow newRow;

                newColumn = tabla.Columns.Add("name");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = false;                

                newColumn = tabla.Columns.Add("address");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = true;

                //newColumn = tabla.Columns.Add("address 2");
                //newColumn.DataType = System.Type.GetType("System.String");
                //newColumn.AllowDBNull = true;

                newColumn = tabla.Columns.Add("post code");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = true;

                newColumn = tabla.Columns.Add("city");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = true;

                newColumn = tabla.Columns.Add("county");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = true;

                newColumn = tabla.Columns.Add("phone no.");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = true;

                newColumn = tabla.Columns.Add("fax no.");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = true;

                newColumn = tabla.Columns.Add("home page");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = true;

                newColumn = tabla.Columns.Add("e-mail");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = true;

                newColumn = tabla.Columns.Add("VAT registration no.");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = true;
                                
                
                newRow = tabla.NewRow();
                newRow["name"] = companyInfo.Name;
                newRow["address"] = companyInfo.Address + companyInfo.Address_2;
                //newRow["address 2"] = companyInfo.Address_2;
                newRow["post code"] = companyInfo.Post_Code;
                newRow["city"] = companyInfo.City;
                newRow["county"] = companyInfo.County;
                newRow["phone no."] = companyInfo.Phone_No;
                newRow["fax no."] = companyInfo.Fax_No;
                newRow["home page"] = companyInfo.Home_Page;
                newRow["e-mail"] = companyInfo.E_Mail;
                newRow["VAT registration no."] = companyInfo.VAT_Registration_No;                
                
                tabla.Rows.Add(newRow);
                

                return (tabla);

            }

        #endregion

        #region LOGIN

            public string VendorLogin(string userID, string userPassword)
            {
                if (userID.Length > 0 && userPassword.Length > 0)
                {
                    INIKER.Vendor.VendorList[] vendors;
                    INIKER.Vendor.VendorList_Service sVendorList =
                        new INIKER.Vendor.VendorList_Service(_userLogin, _pwdLogin, _empresaLogin);
                    vendors = sVendorList.ReadMultiple(userID + ";" + userPassword, 0, 0, "");

                    if (vendors.Count() > 0)
                        return (vendors[0].Name);
                    else
                        return ("");
                }
                else
                {
                    return ("");
                }
            }
  
        #endregion
    }
}
