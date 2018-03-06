using System;
using System.Collections.Generic;
using System.Text;
using Avalara.AvaTax.RestClient;
using NUnit.Framework;

namespace Avalara
{
    public class entAvalaraConnector
    {
        public bool produccion;
        public bool lineLog;
        public bool fullLog;
        public bool transactionLog;
        public bool console;
        int iCount = 0;
        int iCountNew = 0;
        int iCountError = 0;
        int iCountError2 = 0;
        public string lastStrError = "";
        public string transactionData = "";

        //public string navUser { get { return Seguridad.DesEncriptar("YQBkAG0AaQBuAGkA"); } }
        //public string navPass { get { return Seguridad.DesEncriptar("RQB2AGkAYQBuADcANwA3ACEA"); } }
        public string navUser { get { return Seguridad.DesEncriptar("cwBlAHIAdgBpAGMAZQBzAGEAZABtAGkAbgA="); } }
        public string navPass { get { return Seguridad.DesEncriptar("UwByAHYANwA0ADMAIQBTAGEAbAA="); } }
        
        public string navDomain = "SALTO";

        public string filesPath = @"\\server2\Tasks\WSNav\consAvalaraTax\";
        public string LogPath;

        public AvaTaxClient Client { get; set; }
        public string CompanyCode { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public CompanyModel Company { get; set; }
        public TransactionModel trans;
        public bool Connected { get; set; }

        public string Program = "Avalara";

        public void Connect()
        {
            try
            {
                Connected = false;
                Company = new CompanyModel();
                if (this.produccion)
                {
                    Client = new AvaTaxClient("SALTO USA Connector", "1.0", Environment.MachineName, AvaTaxEnvironment.Production).WithSecurity(username, password);
                    //Client.Ping();
                    Company = Client.GetCompany(129096, "");
                    Connected = true;
                }
                else
                {
                    Client = new AvaTaxClient("SALTO USA Connector", "1.0", Environment.MachineName, AvaTaxEnvironment.Sandbox).WithSecurity(username, password);
                    //Client.Ping();
                    Company = Client.GetCompany(491437, "");
                    Connected = true;
                }
            }
            catch (AvaTaxError ex) 
            {
                Connected = false;
                General.CreateFileAddLine("AvaTaxError (" + this.produccion + "). No hemos podido conectarnos a Avalara.", this.LogPath, true);
                lastStrError = ex.error.ToString(); //ex.Message.ToString();
                if ((ex.error != null) && (ex.error.error != null) && (ex.error.error.message != null))
                {
                    lastStrError = ex.error.error.message.ToString();
                }
                General.CreateFileAddLine("AvaTaxError (" + this.produccion + "). Connect. " + lastStrError, this.LogPath, true);
            }
        }

        public bool EnviarTransaccion(string type, string doc)
        {
            bool response = true;
            iCount = 0;
            iCountNew = 0;
            iCountError = 0;
            iCountError2 = 0;
            //entSATConnector satCon = new entSATConnector();
            //satCon.produccion = this.produccion;
            try
            {
                Taxes.Taxes[] taxes;
                Taxes.Taxes_Service ws = new Taxes.Taxes_Service();
                ws.Credentials = new System.Net.NetworkCredential(navUser, navPass, navDomain); //ws.UseDefaultCredentials = true;
                if (this.produccion)
                    ws.Url = ws.Url.Replace("192.168.0.105", "192.168.0.104");

                Taxes.Taxes item = ws.Read(type, doc);

                item.No = item.No.Replace("/", "-");               
                if (item.CustomerCode != null)
                {
                    response = SendDocument(ref item); 
                }
                else
                { //El documento ya no existe en navision, pero sí en la tabla Impuestos
                    item.DescripcionError = "This document doesn't exists in Navision. Delete these lines from Taxes table.";
                }

                item.No = item.No.Replace("-", "/");
                
                ws.Update(ref item);
                /*
                taxes = ws.ReadMultiple(null, "", 0);

                foreach (Taxes.Taxes element in taxes)
                {
                    iCountNew =  element.TaxesLines.Length;
                    Taxes.Taxes item = element;
                    item.No = item.No.Replace("/","-");
                    
                    if (item.CustomerCode != null) {
                        SendDocument(ref item);
                    } else { //El documento ya no existe en navision, pero sí en la tabla Impuestos
                        item.DescripcionError = "This document doesn't exists in Navision. Delete these lines from Taxes table.";
                    }

                    item.No = item.No.Replace("-", "/");
                    ws.Update(ref item);
                }
                 */
            }
            catch (Exception ex)
            {
                iCountError += 1;
                response = false;
                lastStrError = ex.Message.ToString();
                if ((ex.InnerException != null) && (ex.InnerException.Message != null)) {
                    lastStrError += ". " + ex.InnerException.Message.ToString();
                }
                General.CreateFileAddLine(Program + "Error (" + this.produccion + "). EnviarTransaccion. " + lastStrError, this.LogPath, true);
                General.SendEmail("Nav1@saltosystems.com", "nav.hq@saltosystems.com", Program + "Error (" + this.produccion + "). EnviarTransaccion",
                    "Error: " + lastStrError);
                //General.CreateFileAddLine(Program + " (" + con.produccion + ") Fin", con.LogPath, true);
                //eventLogConnector.WriteEntry("ERROR - EnviarProyectosToSATMovil (" + this.produccion + "): " + ex.Message.ToString(), EventLogEntryType.Error);
            }
            //eventLogConnector.WriteEntry("EnviarProyectosToSATMovil (" + this.produccion + "): " + Coleccion + " nuevos creados " + iCountNew.ToString() + ", modificados " + iCount.ToString() + ", y en error " + iCountError.ToString() + " (error fase " + iCountError2.ToString() + ") el " + DateTime.Now.ToLongDateString() + " a las " + DateTime.Now.ToLongTimeString(), EventLogEntryType.Information);
            //satCon = null;
            return response;
        }

        public bool GuardarErrorEnNavision(string type, string doc, int linea, string Error)
        {
            bool newItem = false;
            bool response = true;
            iCount = 0;
            iCountNew = 0;
            iCountError = 0;
            iCountError2 = 0;
            //entSATConnector satCon = new entSATConnector();
            //satCon.produccion = this.produccion;
            try
            {
                //Taxes.Taxes[] taxes;
                Taxes.Taxes_Service ws = new Taxes.Taxes_Service();
                ws.Credentials = new System.Net.NetworkCredential(navUser, navPass, navDomain); //ws.UseDefaultCredentials = true;
                if (this.produccion)
                    ws.Url = ws.Url.Replace("192.168.0.105", "192.168.0.104");

                Taxes.Taxes item = ws.Read(type, doc);
                if (item == null)
                { //Hay que crear el registro
                    newItem = true;
                    item = new Taxes.Taxes();
                    if (type == "0") item.Tipo = Taxes.Tipo.Pedido;
                    if (type == "1") item.Tipo = Taxes.Tipo.FacturaReg;
                    if (type == "2") item.Tipo = Taxes.Tipo.Abono;
                    if (type == "3") item.Tipo = Taxes.Tipo.AbonoReg;
                    if (type == "4") item.Tipo = Taxes.Tipo.Oferta;
                    if (type == "5") item.Tipo = Taxes.Tipo.FacturaPrev;
                    if (type == "6") item.Tipo = Taxes.Tipo.AbonoPrev;
                    item.TipoSpecified = true;
                    item.No = doc; // item.No.Replace("-", "/");               
                }
                //item.DescripcionError = Error;
                SaveDescripcionError(ref item, Error);

                item.Fecha_respuesta = DateTime.Now;
                item.Procesado_respuesta = true;
                item.Fecha_respuestaSpecified = true;
                item.Procesado_respuestaSpecified = true;
                if (newItem)
                    ws.Create(ref item);
                else
                    ws.Update(ref item);
            }
            catch (Exception ex)
            {
                iCountError += 1;
                response = false;
                lastStrError = ex.Message.ToString();
                if ((ex.InnerException != null) && (ex.InnerException.Message != null))
                {
                    lastStrError += ". " + ex.InnerException.Message.ToString();
                }
                General.CreateFileAddLine(Program + "Error (" + this.produccion + "). GuardarErrorEnNavision. " + lastStrError, this.LogPath, true);
                General.SendEmail("Nav1@saltosystems.com", "nav.hq@saltosystems.com", Program + "Error (" + this.produccion + "). EnviarTransaccion",
                    "Error: " + lastStrError);
            }
            return response;
        }

        public bool SendDocument(ref Taxes.Taxes doc)
        {
            bool ok = true;
            try
            {
                string strCancel = "";
                string strAdjustReason = "";
                bool existeEnAvalara = false;
                int i = 0;
                int numLines = doc.TaxesLines.Length;

                string TransactionCode = doc.No;
                DocumentType docType = DocumentType.SalesOrder;
                
                if (doc.Tipo == Taxes.Tipo.Pedido) { docType = DocumentType.SalesOrder; }
                if (doc.Tipo == Taxes.Tipo.Oferta) { docType = DocumentType.SalesOrder; }
                if (doc.Tipo == Taxes.Tipo.FacturaPrev) { docType = DocumentType.SalesOrder; } //También es temporal
                if (doc.Tipo == Taxes.Tipo.AbonoPrev) { docType = DocumentType.SalesOrder; } //También es temporal
                if (doc.Tipo == Taxes.Tipo.Abono) { docType = DocumentType.ReturnOrder; }
                if (doc.Tipo == Taxes.Tipo.FacturaReg) { docType = DocumentType.SalesInvoice; }
                if (doc.Tipo == Taxes.Tipo.AbonoReg) { docType = DocumentType.ReturnInvoice; } //Si cancelamos un AbonoReg no buscarlo porque no lo encuentra

                if (doc.Accion == Taxes.Accion.Cancel)
                {
                    strCancel = "CANCEL";
                    if (doc.Tipo == Taxes.Tipo.AbonoReg)
                        existeEnAvalara = true;
                }

                General.CreateFileAddLine(Program + " (" + this.produccion + "). SendDocument - " + doc.Tipo.ToString() + " " + doc.No + " " + strCancel, this.LogPath, true);

                //if ((doc.Tipo == Taxes.Tipo.AbonoReg) || (doc.Tipo == Taxes.Tipo.FacturaReg))
                if (doc.Tipo == Taxes.Tipo.FacturaReg)
                {
                    //Ya existe en avalara, habrá que hacer un ajuste
                    var trans = new TransactionModel();
                    try
                    {
                        trans = GetDocNo(TransactionCode);
                    }
                    catch (AvaTaxError AvaEx) {
                        trans = null;
                    }

                    //Assert.NotNull(commitResult, "Should have been able to call CommitTransaction");
                    if (trans != null) 
                        existeEnAvalara = true;
                    trans = null;
                }
                doc.DescripcionError = "";

                // Execute a transaction
                TransactionBuilder transBuild = new TransactionBuilder(Client, Company.companyCode, docType, doc.CustomerCode)
                    .WithAddress(TransactionAddressType.ShipFrom, doc.ShipFromLine1, doc.ShipFromLine2, doc.ShipFromLine3, doc.ShipFromCity, doc.ShipFromRegion, doc.ShipFromPostCode, doc.ShipFromCountry)
                    .WithAddress(TransactionAddressType.ShipTo, doc.ShipToLine1, doc.ShipToLine2, doc.ShipToLine3, doc.ShipToCity, doc.ShipToRegion, doc.ShipToPostCode, doc.ShipToCountry);

                //Add lines
                foreach (Taxes.ImpuestosLineas line in doc.TaxesLines)
                {
                    //***** SALTO, GRE, 04/07/2017   ; SALTO USA - Credit Memos to Avalara with negative Amounts
                    if ((doc.Tipo == Taxes.Tipo.Abono) || (doc.Tipo == Taxes.Tipo.AbonoPrev) || (doc.Tipo == Taxes.Tipo.AbonoReg))
                        line.Importe = (-1)*line.Importe;

                    if (line.TaxCode == null) line.TaxCode = "";
                    General.CreateFileAddLine(Program + " (" + this.produccion + "). SendDocument - " + docType.ToString() + " " + doc.No + " Enviamos - " + line.Linea.ToString() + ": " + line.TaxCode + " - " + line.Codigo + " - " + line.Importe.ToString(), this.LogPath, true);
                    transBuild.WithLine(line.Linea.ToString(), line.Importe, line.Cantidad, line.TaxCode, line.Codigo);
                    if (line.ShipToLine1 != null)
                    {
                        if ((doc.ShipToLine1 != line.ShipToLine1) || (doc.ShipToLine2 != line.ShipToLine2) || (doc.ShipToLine3 != line.ShipToLine3) || (doc.ShipToCity != line.ShipToCity)
                            || (doc.ShipToRegion != line.ShipToRegion) || (doc.ShipToPostCode != line.ShipToPostCode) || (doc.ShipToCountry != line.ShipToCountry))
                        {
                            //14/06/17 - Parece que hay que mandarle también en la línea el ShipFrom, sino te pone la misma que el ShipTo
                            transBuild.WithLineAddress(TransactionAddressType.ShipFrom, doc.ShipFromLine1, doc.ShipFromLine2, doc.ShipFromLine3, doc.ShipFromCity, doc.ShipFromRegion, doc.ShipFromPostCode, doc.ShipFromCountry);
                            transBuild.WithLineAddress(TransactionAddressType.ShipTo, line.ShipToLine1, line.ShipToLine2, line.ShipToLine3, line.ShipToCity, line.ShipToRegion, line.ShipToPostCode, line.ShipToCountry);
                            General.CreateFileAddLine(Program + " (" + this.produccion + "). SendDocument - " + docType.ToString() + " " + doc.No + " ShipFrom - Address: " + doc.ShipFromLine1 + " " + doc.ShipFromLine2 + " " + doc.ShipFromLine3 + ". City: " + doc.ShipFromCity + ". Region: " + doc.ShipFromRegion + ". PostCode: " + doc.ShipFromPostCode + ". Country: " + doc.ShipFromCountry, this.LogPath, true);
                            General.CreateFileAddLine(Program + " (" + this.produccion + "). SendDocument - " + docType.ToString() + " " + doc.No + " ShipTo   - Address: " + line.ShipToLine1 + " " + line.ShipToLine2 + " " + line.ShipToLine3 + ". City: " + line.ShipToCity + ". Region: " + line.ShipToRegion + ". PostCode: " + line.ShipToPostCode + ". Country: " + line.ShipToCountry, this.LogPath, true);
                        }
                    }
                }
                transBuild
                    .WithTransactionDate(doc.DocDate)
                    .WithTransactionCode(doc.No);

                // Esto no lo hago porque una vez commitada casi no se puede hacer nada con ella...de momento la dejo uncommited.
                if (((docType == DocumentType.SalesInvoice) || (docType == DocumentType.ReturnInvoice)) && (doc.Accion == Taxes.Accion._blank_))
                {
                    transBuild.WithCommit();
                }
                var transaction = new Avalara.AvaTax.RestClient.TransactionModel();
                if (existeEnAvalara)
                {
                    if (doc.Accion == Taxes.Accion.Cancel) strAdjustReason = "Cancelled by user";
                    if (doc.Accion == Taxes.Accion._blank_) strAdjustReason = "Changed by user";
                    //// *************** FUNCIONA, AUNQUE ME OBLIGA A LEER Y VOLVER A CREAR LA NEW TRANSACTION IGUAL QUE LA TRANSACTION ***************
                    var transAdjust = transBuild.CreateAdjustmentRequest(strAdjustReason, AdjustmentReason.Other);

                    //No se porqué pero falla al hacer el commit después. Lo hago al crear la transacción
                    if (doc.Accion == Taxes.Accion.Cancel) transAdjust.newTransaction.commit = false;
                    if (doc.Accion == Taxes.Accion._blank_) transAdjust.newTransaction.commit = true;
                    ////transAdjust.adjustmentDescription = "Test adjustment";
                    ////transAdjust.adjustmentReason = AdjustmentReason.Other;
                    transaction = Client.AdjustTransaction(Company.companyCode, TransactionCode, transAdjust);
                }
                else {
                    if (this.fullLog)
                        transactionData = transBuild.FullLog;
                        //General.CreateFileAddLine(transBuild.FullLog, this.LogPath, true); 
                    transaction = transBuild.Create();

                    if (transactionLog) WriteLogTransaction(transaction);

                    // Ensure this transaction was created, and has three lines, and has some tax
                    Assert.NotNull(transaction, "Transaction should have been created");
                    Assert.True(transaction.lines.Count == numLines, "Transaction should have three lines");
                    //Assert.True(transaction.totalTax > 0.0m, "Transaction should have had some tax");
                }
              
                //Show response
                if (console)
                {
                    Console.WriteLine("Total tax: " + transaction.totalTax + " (" + transaction.totalTaxCalculated + ")");
                    foreach (Taxes.ImpuestosLineas line in doc.TaxesLines)
                    {
                        Console.WriteLine("Linea " + line.Linea + ": " + transaction.lines[i].tax + " (" + transaction.lines[i].taxCalculated + ")");
                        i += 1;
                    }
                    Console.ReadLine();
                }

                //***** SALTO, GRE, 04/07/2017   ; SALTO USA - Credit Memos to Avalara with negative Amounts
                if ((doc.Tipo == Taxes.Tipo.Abono) || (doc.Tipo == Taxes.Tipo.AbonoPrev) || (doc.Tipo == Taxes.Tipo.AbonoReg))
                    transaction.totalTax = (-1) * transaction.totalTax;
                
                doc.Impuesto = (decimal)transaction.totalTax;
                doc.Fecha_respuesta = DateTime.Now;
                doc.Procesado_respuesta = true;

                doc.ImpuestoSpecified = true;
                doc.Fecha_respuestaSpecified = true;
                doc.Procesado_respuestaSpecified = true;

                foreach (Taxes.ImpuestosLineas line in doc.TaxesLines)
                {
                    //transaction.lines.Find(x => x.lineNumber == '5000')
                    i = 0;
                    foreach (TransactionLineModel linAva in transaction.lines)
                    {
                        if (i == 0)
                        {
                            if (linAva.lineNumber == line.Linea.ToString())
                            {
                                General.CreateFileAddLine(Program + " (" + this.produccion + "). SendDocument - " + docType.ToString() + " " + doc.No + " Recibimos- " + line.Linea.ToString() + ": " + linAva.tax.ToString(), this.LogPath, true);
                                //***** SALTO, GRE, 04/07/2017   ; SALTO USA - Credit Memos to Avalara with negative Amounts
                                if ((doc.Tipo == Taxes.Tipo.Abono) || (doc.Tipo == Taxes.Tipo.AbonoPrev) || (doc.Tipo == Taxes.Tipo.AbonoReg))
                                {
                                    linAva.tax = (-1) * linAva.tax;
                                    line.Importe = (-1) * line.Importe;
                                }

                                line.Impuesto = (decimal)linAva.tax;
                                line.Fecha_respuesta = DateTime.Now;
                                line.Procesado_respuesta = true;
                                
                                line.ImpuestoSpecified = true;
                                line.Fecha_respuestaSpecified = true;
                                line.Procesado_respuestaSpecified = true;

                                i += 1;
                            }
                        }
                    } 
                }
                // Usefull for Posted documents
                // Now commit that transaction

                //No se porqué pero falla al hacer el commit después. Lo hago al crear la transacción
                //if ((docType == DocumentType.SalesInvoice) || (docType == DocumentType.ReturnInvoice)) {
                //    var commitResult =Client.CommitTransaction(TestCompany.companyCode, transaction.code, new CommitTransactionModel() { commit = true });
                //    //// Ensure that this transaction was committed
                //    Assert.NotNull(commitResult, "Should have been able to call CommitTransaction");
                //    Assert.True(commitResult.status == DocumentStatus.Committed, "Transaction should have been committed");
                //}

                //// Now void the transaction
                //var voidResult = Client.VoidTransaction(TestCompany.companyCode, transaction.code, new VoidTransactionModel()
                //{
                //    code = VoidReasonCode.DocVoided
                //});

                //// Ensure that the transaction was voided
                //Assert.NotNull(voidResult, "Should have been able to call VoidTransactoin");
                //Assert.True(voidResult.status == DocumentStatus.Cancelled, "Transaction should have been voided");
            }
            catch (AvaTaxError AvaEx)
            {
                lastStrError = AvaEx.error.error.message;
                if (lastStrError == null)
                {
                    if (AvaEx.error.error.details.Count > 1) {
                        lastStrError = AvaEx.error.error.details[0].ToString();
                    } else
                        lastStrError = AvaEx.error.error.details.ToString();
                }
                General.CreateFileAddLine("AvaTaxError (" + this.produccion + "). SendDocument " + doc.No + ": " + lastStrError, this.LogPath, true);

                if (this.fullLog )
                 General.CreateFileAddLine(transactionData, this.LogPath, true); 

                if (console)
                {
                    Console.WriteLine("AvaError: " + AvaEx.error.error.message);
                    Console.ReadLine();
                }              
                doc.DescripcionError = lastStrError;
                //***** INI(08/09/2017 - GRE - Los errores también dejar como procesados y con el texto de error)
                doc.Fecha_respuesta = DateTime.Now;
                doc.Procesado_respuesta = true;
                doc.Fecha_respuestaSpecified = true;
                doc.Procesado_respuestaSpecified = true;
                //***** FIN(08/09/2017 - GRE - Los errores también dejar como procesados y con el texto de error)
                ok = false;
            }
            catch (Exception ex)
            {
                General.CreateFileAddLine("Error (" + this.produccion + "). SendDocument " + doc.No + ": " + ex.Message.ToString(), this.LogPath, true);
                if (console)
                {
                    Console.WriteLine("Error: " + ex.Message.ToString());
                    Console.ReadLine();
                }
                lastStrError = ex.Message.ToString();

                //doc.DescripcionError = lastStrError;
                SaveDescripcionError(ref doc, lastStrError);

                //***** INI(08/09/2017 - GRE - Los errores también dejar como procesados y con el texto de error)
                doc.Fecha_respuesta = DateTime.Now;
                doc.Procesado_respuesta = true;
                doc.Fecha_respuestaSpecified = true;
                doc.Procesado_respuestaSpecified = true;
                //***** FIN(08/09/2017 - GRE - Los errores también dejar como procesados y con el texto de error)
                ok = false;
            }
            return ok;
        }

        public bool SaveDescripcionError(ref Taxes.Taxes doc, string errorText) {
            string errText = errorText;
            if (errText.Length < 250)
            {
                doc.DescripcionError = errText;
            }
            else {
                doc.DescripcionError = errText.Substring(0, 250);
                errText = errText.Substring(250, errText.Length - 250);
                if (errText.Length < 250)
                {
                    doc.DescripcionError2 = errText;
                }
                else {
                    doc.DescripcionError2 = errText.Substring(0, 250);
                    errText = errText.Substring(250, errText.Length - 250);
                    doc.DescripcionError3 = errText.Substring(0, 250); //Descartamos el resto del errorText
                }
            }
            return true;
        }

        public TransactionModel GetDocNo(string DocNo)
        {
            var trans = Client.GetTransactionByCode(Company.companyCode, DocNo, "Lines");
            return trans;
        }

        public FetchResult<TransactionModel> GetAllTransactions()
        {
            var trans = Client.ListTransactionsByCompany(Company.companyCode, "", "", 0, 0, "code");
            return trans;
        }

        public void VoidDocNo(string DocNo) {
            // Para hacer un void, el documento tiene que estar Uncommited o Commited
            //// Now void the transaction
            var voidResult = Client.VoidTransaction(Company.companyCode, DocNo, new VoidTransactionModel()
            {
                code = VoidReasonCode.DocVoided,
            });
        }

        public void DeleteDocNo(string DocNo)
        {
            // Para hacer un void, el documento tiene que estar Uncommited o Commited
            //// Now void the transaction
            var voidResult = Client.VoidTransaction(Company.companyCode, DocNo, new VoidTransactionModel()
            {
                code = VoidReasonCode.DocDeleted
            });
        }

        public void ChangeDocNo(string DocNo, string NewDocNo)
        {
            // Para hacer un void, el documento tiene que estar Uncommited
            //// Now void the transaction
            var newDocNoResult = Client.ChangeTransactionCode(Company.companyCode, DocNo, new ChangeTransactionCodeModel()
            {
                newCode = NewDocNo
            });
        }

        public TransactionModel CommitDocNo(string DocNo, bool _commit)
        {
            // Now commit that transaction
            //No se porqué pero falla al hacer el commit después. Lo hago al crear la transacción
            var commitResult = Client.CommitTransaction(Company.companyCode, DocNo, new CommitTransactionModel() { commit = _commit });
            //// Ensure that this transaction was committed
            //Assert.NotNull(commitResult, "Should have been able to call CommitTransaction");
            //Assert.True(commitResult.status == DocumentStatus.Committed, "Transaction should have been committed");
            return commitResult;
        }

        public TransactionModel AdjustDocNo(string DocNo)
        {
            //No funciona, no quiero volver a crear toda la transacción de cero
            //Hay que conseguir implementar el Create desde la this.trans que tengo.
            var transBuild = new TransactionBuilder(Client,Company.companyCode,DocumentType.SalesInvoice,this.trans.customerVendorCode);
            var adjustTrans = transBuild.CreateAdjustmentRequest("Test", AdjustmentReason.Other);
            //adjustTrans.newTransaction = transBuild;
            adjustTrans.newTransaction.commit = false;
            var commitResult = Client.AdjustTransaction(Company.companyCode, DocNo, adjustTrans);
            //// Ensure that this transaction was committed
            Assert.NotNull(commitResult, "Should have been able to call CommitTransaction");
            Assert.True(commitResult.status == DocumentStatus.Committed, "Transaction should have been committed");

            return commitResult;
        }
       




        public void SendOrder(string OrderNo)
        {
            try
            {
                string TransactionCode = OrderNo; //"17/0860";

                // Execute a transaction
                var transaction = new TransactionBuilder(Client, Company.companyCode, DocumentType.SalesInvoice, "C000451")
                    .WithAddress(TransactionAddressType.SingleLocation, "521 S Weller St", null, null, "Seattle", "WA",
                        "98104", "US")
                    .WithLine(100.0m, 1, "P0000000", "LA1T1570A20IM8RH") //"P0000000" - taxCode. "P0000000" is the default taxCode
                    .WithLineTaxOverride(TaxOverrideType.TaxAmount, "Test taxt override", 50m)
                    .WithLine(200m, 1, "P0000000")
                    .WithExemptLine(50m, "NT") //"NT" - exeptionCode
                    .WithTransactionCode(TransactionCode) //"17/0860"
                    .Create();

                // Ensure this transaction was created, and has three lines, and has some tax
                Assert.NotNull(transaction, "Transaction should have been created");
                Assert.True(transaction.totalTax > 0.0m, "Transaction should have had some tax");
                Assert.True(transaction.lines.Count == 3, "Transaction should have three lines");

                // Now commit that transaction
                var commitResult = Client.CommitTransaction(Company.companyCode, transaction.code, new CommitTransactionModel() { commit = true });

                // Ensure that this transaction was committed
                Assert.NotNull(commitResult, "Should have been able to call CommitTransaction");
                Assert.True(commitResult.status == DocumentStatus.Committed, "Transaction should have been committed");

                // Now void the transaction
                var voidResult = Client.VoidTransaction(Company.companyCode, transaction.code, new VoidTransactionModel()
                {
                    code = VoidReasonCode.DocVoided
                });

                // Ensure that the transaction was voided
                Assert.NotNull(voidResult, "Should have been able to call VoidTransactoin");
                Assert.True(voidResult.status == DocumentStatus.Cancelled, "Transaction should have been voided");

            }
            catch (AvaTaxError AvaEx)
            {

                Console.WriteLine("AvaError: " + AvaEx.error.error.message);
                Console.ReadLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message.ToString());
                Console.ReadLine();
            }
        }

        public void TransactionWorkflow()
        {

            try
            {

                string TransactionCode = "YYYYYYYY";

                // Now commit that transaction
                //var commitResultPrev = Client.CommitTransaction(TestCompany.companyCode, TransactionCode, new CommitTransactionModel() { commit = true });

                // Execute a transaction
                var transactionBuild = new TransactionBuilder(Client, Company.companyCode, DocumentType.SalesInvoice, "C000451")
                    .WithAddress(TransactionAddressType.SingleLocation, "521 S Weller St", null, null, "Seattle", "WA",
                        "98104", "US")
                    .WithLine(100.0m, 1, "P0000000", "LA1T1570A20IM8RH") //"P0000000" - taxCode. "P0000000" is the default taxCode
                    .WithLineTaxOverride(TaxOverrideType.TaxAmount, "Test taxt override", 50m)
                    .WithLine(200m, 1, "P0000000")
                    .WithExemptLine(50m, "NT") //"NT" - exeptionCode
                    .WithTransactionCode(TransactionCode); //"17/0860"
                    
                var transaction = transactionBuild.Create();
                //var updateResult = Client.UpdateItem(TestCompany.id, int.Parse(transaction.lines[0].id.ToString()), new ItemModel()
                //{
                //    itemCode = "LA1T1570A20IM8RH"
                //});


                // Ensure this transaction was created, and has three lines, and has some tax
                Assert.NotNull(transaction, "Transaction should have been created");
                Assert.True(transaction.totalTax > 0.0m, "Transaction should have had some tax");
                Assert.True(transaction.lines.Count == 3, "Transaction should have three lines");

                // Now commit that transaction
                var commitResult = Client.CommitTransaction(Company.companyCode, transaction.code, new CommitTransactionModel() { commit = true });

                // Ensure that this transaction was committed
                Assert.NotNull(commitResult, "Should have been able to call CommitTransaction");
                Assert.True(commitResult.status == DocumentStatus.Committed, "Transaction should have been committed");

                // Now void the transaction
                var voidResult = Client.VoidTransaction(Company.companyCode, transaction.code, new VoidTransactionModel()
                {
                    code = VoidReasonCode.DocVoided
                });

                // Ensure that the transaction was voided
                Assert.NotNull(voidResult, "Should have been able to call VoidTransactoin");
                Assert.True(voidResult.status == DocumentStatus.Cancelled, "Transaction should have been voided");

            }
            catch (AvaTaxError AvaEx)
            {

                Console.WriteLine("AvaError: " + AvaEx.error.error.message);
                Console.ReadLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message.ToString());
                Console.ReadLine();
            }
        }

        public void TransactionTests()
        {
            // Create a simple transaction for $100 using the fluent transaction builder
            //var transaction = new TransactionBuilder(Client, "DEFAULT", DocumentType.SalesInvoice, "ABC")
            //    .WithAddress(TransactionAddressType.SingleLocation, "123 Main Street", null, null, "Irvine", "CA", "92615", "US")
            //    .WithLine(100.0m, 1, "P0000000")
            //    .Create();

            /*
             * Example of:
             * 
             *  Sales Order SALTO USA - 173286 -    Total tax = 6,30
             *  Sales Invoice SALTO USA - 17/0860 - Total Tax = 6,30     
             * 
            */

            //var t2 = new TransactionBuilder(Client, CompanyCode, DocumentType.SalesOrder, "C000451")
            //    .WithAddress(TransactionAddressType.ShipFrom, "1780 Corporate Drive, Suite 400", "", "", "Norcross", "GA", "30093", "US") //SALTO USA
            //    .WithAddress(TransactionAddressType.ShipTo, "329 NE Tradewind Lane", "", "", "Stuart", "FL", "34996", "US")
            //    .WithLine(80.0m, 1, "")
            //    .WithLine(25.0m, 1, "") // Each line is added as a separate item on the invoice!
            //    .Create();

            var t2builder = new TransactionBuilder(Client, CompanyCode, DocumentType.SalesOrder, "CNUEVO")
                .WithAddress(TransactionAddressType.ShipFrom, "1780 Corporate Drive, Suite 400", "", "", "Norcross", "GA", "30093", "US") //SALTO USA
                .WithAddress(TransactionAddressType.ShipTo, "329 NE Tradewind Lane", "", "", "Stuart", "FL", "34996", "US")
                .WithLine(80.0m, 1, "")
                .WithLine(25.0m, 1, ""); // Each line is added as a separate item on the invoice!
                //.Create();
            var t2 = t2builder.Create();

            if (transactionLog) WriteLogTransaction(t2);

            Console.WriteLine("Your calculated tax was {0}", t2.totalTax);

            Console.ReadKey();

        }

        public void WriteLogTransaction(TransactionModel trans) {
            General.CreateFileAddLine(Program + " (" + this.produccion + ") sending transaction " + trans.code, this.LogPath, true);
            foreach (AvaTaxMessage line in trans.messages)
            {
                General.CreateFileAddLine(line.summary + ": " + line.details, this.LogPath, true);
            }     
            
        }

        //static void Main(string[] args)
        //{

        //    string username = "g.remirez@saltosystems.com";
        //    string password = "Avalara2017!";
        //    CompanyCode = "SALTOTEST";

        //    //TestCompany = new CompanyModel();

        //    //string combined = String.Format("{0}:{1}", username, password);
        //    //string base64 = System.Convert.ToBase64String(bytes);
        //    //byte[] bytes = Encoding.UTF8.GetBytes(combined);
        //    //HttpClient cli = new HttpClient();
        //    //cli.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64);
        //    ////https://sandbox-rest.avatax.com/swagger/ui/index.html#!/Companies/ApiV2CompaniesGet

        //    //Test of integration

        //    // Create a client and set up authentication
        //    Client = new AvaTaxClient("MyApp", "1.0", Environment.MachineName, AvaTaxEnvironment.Sandbox)
        //        .WithSecurity(username, password);

        //    TestCompany = Client.GetCompany(491437, "");

        //    // Verify that we can ping successfully
        //    var pingResult = Client.Ping();
        //    if (pingResult.authenticated == true)
        //    {
        //        Console.WriteLine("Success!");
        //    }

        //    SendOrder();

        //}
    }
}
