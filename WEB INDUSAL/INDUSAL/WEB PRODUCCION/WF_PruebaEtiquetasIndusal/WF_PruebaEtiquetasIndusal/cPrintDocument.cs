using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;

namespace WF_PruebaEtiquetasIndusal
{
    public class cPrintDocument
    {
        // MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA

        // Dimensiones A4 para usar de referencia
        //  Width=826
        //  Height=1169

        public enum eTipoDocumento
        {
            carroTransporte,
            carroLavado,
            carroIncompleto,
            carroOxido,
            paqueteProducto
        }


        private int iAlturaPq = 90;
        private int iAlturaGr = 100;

// INI *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA
        private int iAnchuraPq = 280; //  80 mm
// FIN *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA

        private int iAnchuraGr = 420; // 120 mm

        private Size sizeRectPq;        
        private Size sizeRectGr;
        
        private Point posRectCliente;
        private Point posRectPedido;
        private Point posRectProducto;

        private Rectangle rectCliente;
        private Rectangle rectPedido;
        private Rectangle rectProducto;

        Font textFontXSmall;
        Font textFontSmall;
        Font textFontMedium;
        Font textFontLarge; // MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA
        Font codeBarrFontSmall;
        Font codeBarrFontMedium;
        Font codeBarrFontLarge;
        Font textFontXXLarge;
        Font textFontXLarge;
        Font textFontXXXLarge;

        private Pen pen;

        private const Single mmInch=(Single)25.4;
        //private PrintDocument _printer; // MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA
        private int dotsPerInchX;
        private int dotsPerInchY;
        private string _documentName;

        private int _iCopia;
        private int _nCopias;        

        private eTipoDocumento _tipoDocumento;

        private string _codCliente;
        private string _nomCliente;
        private string _numPedido;
        private string _codProducto;
        private string _nomProducto;
        private string _mensaje;
        private string _nCarro;
        private string _deviceName;

// INI *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA
        private string _dimensiones;        
// FIN *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA

        public string DeviceName
        {
            get { return _deviceName; }
            set { _deviceName = value; }
        }

        public eTipoDocumento TipoDocumento
        {
            get { return _tipoDocumento; }
            set { _tipoDocumento = value; }
        }

        public string CodCliente
        {
            get { return _codCliente; }
            set { _codCliente = "*"+value+"*"; }
        }

        public string NomCliente
        {
            get { return _nomCliente; }
            set { _nomCliente = value; }
        }

        public string NumPedido
        {
            get { return _numPedido; }
            set { _numPedido = "*" + value + "*"; }
        }

        public string CodProducto
        {
            get { return _codProducto; }
            set { _codProducto = "*" + value + "*"; }
        }

        public string NomProducto
        {
            get { return _nomProducto; }
            set { _nomProducto = value; }
        }

        public string Mensaje
        {
            get { return _mensaje; }
            set { _mensaje = value; }
        }

        public string NCarro
        {
            get { return _nCarro; }
            set { _nCarro = value; }
        }

// INI *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA
        public string Dimensiones
        {
            get { return _dimensiones; }
            set 
            { 
                _dimensiones = value;
                GenerarDimensiones();
            }
        }
// FIN *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA

        public cPrintDocument()
        {
            // *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA   

            textFontXSmall = new Font("Arial", 7);
            textFontSmall = new Font("Arial", 10);
            textFontMedium = new Font("Arial", 12);
            textFontLarge = new Font("Arial", 16); ; // MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA
            codeBarrFontSmall = new Font("Code 39", 12);
            codeBarrFontMedium = new Font("Code 39", 18);
            codeBarrFontLarge = new Font("Code 39", 24);
            textFontXXLarge = new Font("Arial", 36);
            textFontXLarge = new Font("Arial", 24);
            textFontXXXLarge = new Font("Arial", 48);

            pen = new Pen(Brushes.Black);
        }

        public cPrintDocument(string _pDimensiones)
        {
// INI *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA
            //sizeRectPq = new Size(iAnchuraGr, iAlturaPq);            
            //sizeRectGr = new Size(iAnchuraGr, iAlturaGr);

            //posRectCliente = new Point(25, 17);
            //posRectPedido = new Point(posRectCliente.X, posRectCliente.Y + sizeRectPq.Height);
            //posRectProducto = new Point(posRectCliente.X, posRectPedido.Y + sizeRectPq.Height);

            //rectCliente = new Rectangle(posRectCliente, sizeRectPq);
            //rectPedido = new Rectangle(posRectPedido, sizeRectPq);
            //rectProducto = new Rectangle(posRectProducto, sizeRectGr);
            
            _dimensiones = _pDimensiones;
            GenerarDimensiones();

// FIN *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA   

            textFontXSmall = new Font("Arial", 7);
            textFontSmall = new Font("Arial", 10);
            textFontMedium = new Font("Arial", 12);
            textFontLarge = new Font("Arial", 16); ; // MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA
            codeBarrFontSmall = new Font("Code 39", 12);
            codeBarrFontMedium = new Font("Code 39", 18);
            codeBarrFontLarge = new Font("Code 39", 24);
            textFontXXLarge = new Font("Arial", 36);
            textFontXLarge = new Font("Arial", 24);
            textFontXXXLarge = new Font("Arial", 48);

            pen = new Pen(Brushes.Black);

        }

        public Exception Print(int nCopias)
        {
            _nCopias = nCopias;
            _iCopia = 1;
            try
            {
                PrintDocument oPD = CreatePrintDocument();                
                oPD.DocumentName = _documentName;
                oPD.Print();                
                return (null);
            }
            catch (Exception ex)
            {
                return (ex);
            }
        }

        private void GenerarDimensiones()
        {
            // *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA            
            switch (_dimensiones)
            {
                case "120x80":
                    sizeRectPq = new Size(iAnchuraGr, iAlturaPq);
                    sizeRectGr = new Size(iAnchuraGr, iAlturaGr);
                    break;
                case "80x80":
                    sizeRectPq = new Size(iAnchuraPq, iAlturaPq);
                    sizeRectGr = new Size(iAnchuraPq, iAlturaGr);
                    break;
            }            

            posRectCliente = new Point(25, 17);
            posRectPedido = new Point(posRectCliente.X, posRectCliente.Y + sizeRectPq.Height);
            posRectProducto = new Point(posRectCliente.X, posRectPedido.Y + sizeRectPq.Height);

            rectCliente = new Rectangle(posRectCliente, sizeRectPq);
            rectPedido = new Rectangle(posRectPedido, sizeRectPq);
            rectProducto = new Rectangle(posRectProducto, sizeRectGr);
        }

        private PrintDocument CreatePrintDocument()
        {
            PrintDocument newPD = new PrintDocument();            

            switch (_tipoDocumento)
            {
                case eTipoDocumento.carroIncompleto:
                    newPD.PrintPage += new PrintPageEventHandler(carroIncompleto_PrintPage);
                    _documentName = "carroIncompleto";
                    break;
                case eTipoDocumento.carroLavado:
                    newPD.PrintPage += new PrintPageEventHandler(carroLavado_PrintPage);
                    _documentName = "carroLavado";
                    break;
                case eTipoDocumento.carroOxido:
// INI *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA
                    //newPD.PrintPage += new PrintPageEventHandler(carroOxido_PrintPage);
                    switch (_dimensiones)
                    {
                        case "120x80":
                            newPD.PrintPage += new PrintPageEventHandler(carroOxido120x80_PrintPage);
                            break;
                        case "80x80":
                            newPD.PrintPage += new PrintPageEventHandler(carroOxido80x80_PrintPage);
                            break;
                    }
// FIN *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA
                    _documentName = "carroOxido";
                    break;
                case eTipoDocumento.carroTransporte:
// INI *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA
                    //newPD.PrintPage += new PrintPageEventHandler(carroTransporte_PrintPage);
                    switch (_dimensiones)
                    {
                        case "120x80":
                            newPD.PrintPage += new PrintPageEventHandler(carroTransporte120x80_PrintPage);
                            break;
                        case "80x80":
                            newPD.PrintPage += new PrintPageEventHandler(carroTransporte80x80_PrintPage);
                            break;
                    }
// FIN *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA
                    _documentName = "carroTransporte";
                    break;
                case eTipoDocumento.paqueteProducto:
                    newPD.PrintPage += new PrintPageEventHandler(paqueteProducto_PrintPage);
                    _documentName = "paqueteProducto";
                    break;
            }
                        
            //newPD.PrinterSettings.PrinterName = "Microsoft XPS Document Writer";

            newPD.DefaultPageSettings.Landscape = true;
// INI *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA
            //newPD.DefaultPageSettings.PaperSize = new PaperSize("Etiqueta120x80", 315, 472);
            switch (_dimensiones)
            {
                case "120x80":
                    newPD.DefaultPageSettings.PaperSize = new PaperSize("Etiqueta120x80", 315, 472);
                    break;
                case "80x80":
                    newPD.DefaultPageSettings.PaperSize = new PaperSize("Etiqueta80x80", 315, 315);                    
                    break;
            }
// FIN *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA            

            PrinterResolution pr = newPD.PrinterSettings.DefaultPageSettings.PrinterResolution;
            dotsPerInchX = pr.X;
            dotsPerInchY = pr.Y;

            return (newPD);
        }              

        private void carroIncompleto_PrintPage(object sender, PrintPageEventArgs e)
        {
            // INI *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA
            //e.Graphics.DrawRectangle(pen, rectPedido);
            //e.Graphics.DrawRectangle(pen, rectCliente);
            //e.Graphics.DrawRectangle(pen, rectProducto);
            if (_dimensiones == "120x80")
            {
                e.Graphics.DrawRectangle(pen, rectPedido);
                e.Graphics.DrawRectangle(pen, rectCliente);
                e.Graphics.DrawRectangle(pen, rectProducto);                                 
                
            }
            // FIN *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA
            

            e.Graphics.DrawString(_nomCliente, textFontSmall, Brushes.Black, posRectCliente.X + 10, posRectCliente.Y + 10);
            e.Graphics.DrawString(_codCliente, codeBarrFontMedium, Brushes.Black, posRectCliente.X + 10, posRectCliente.Y + 30);

            e.Graphics.DrawString(_numPedido.Replace("*", ""), textFontSmall, Brushes.Black, posRectPedido.X + 10, posRectPedido.Y + 10);
            e.Graphics.DrawString(_numPedido, codeBarrFontMedium, Brushes.Black, posRectPedido.X + 10, posRectPedido.Y + 30);

            e.Graphics.DrawString("CARRO:", textFontMedium, Brushes.Black, posRectProducto.X + 10, posRectProducto.Y + 10);
            e.Graphics.DrawString(_nCarro, textFontXXXLarge, Brushes.Black, posRectProducto.X + 20, posRectProducto.Y + 25);

            // INI *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA
            //e.Graphics.DrawString("INCOMPLETO", textFontXLarge, Brushes.Black, posRectProducto.X + 180, posRectProducto.Y + 20);
            switch (_dimensiones)
            {
                case "120x80":
                    e.Graphics.DrawString("INCOMPLETO", textFontXLarge, Brushes.Black, posRectProducto.X + 180, posRectProducto.Y + 20);
                    break;
                case "80x80":
                    e.Graphics.DrawString("INCOMPLETO", textFontLarge, Brushes.Black, posRectProducto.X + 120, posRectProducto.Y + 20);
                    break;
            }
            // FIN *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA
            

            if (_mensaje!="")
                e.Graphics.DrawString(_mensaje, textFontXSmall, Brushes.Black, posRectProducto.X + 10, posRectProducto.Y+ 90);

            e.HasMorePages = (_iCopia < _nCopias);
            _iCopia++;
        }

        private void carroLavado_PrintPage(object sender, PrintPageEventArgs e)
        {
            // INI *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA
            //e.Graphics.DrawRectangle(pen, rectPedido);
            //e.Graphics.DrawRectangle(pen, rectCliente);
            //e.Graphics.DrawRectangle(pen, rectProducto);
            if (_dimensiones == "120x80")
            {
                e.Graphics.DrawRectangle(pen, rectPedido);
                e.Graphics.DrawRectangle(pen, rectCliente);
                e.Graphics.DrawRectangle(pen, rectProducto);

            }
            // FIN *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA

            e.Graphics.DrawString(_nomCliente, textFontSmall, Brushes.Black, posRectCliente.X + 10, posRectCliente.Y + 10);
            e.Graphics.DrawString(_codCliente, codeBarrFontMedium, Brushes.Black, posRectCliente.X + 10, posRectCliente.Y + 30);

            e.Graphics.DrawString(_numPedido.Replace("*", ""), textFontSmall, Brushes.Black, posRectPedido.X + 10, posRectPedido.Y + 10);
            e.Graphics.DrawString(_numPedido, codeBarrFontMedium, Brushes.Black, posRectPedido.X + 10, posRectPedido.Y + 30);

            // INI *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA
            //e.Graphics.DrawString("LAVADO", textFontXXLarge, Brushes.Black, posRectProducto.X + 100, posRectProducto.Y + 30);
            switch (_dimensiones)
            {
                case "120x80":
                    e.Graphics.DrawString("LAVADO", textFontXXLarge, Brushes.Black, posRectProducto.X + 100, posRectProducto.Y + 30);
                    break;
                case "80x80":
                    e.Graphics.DrawString("LAVADO", textFontXXLarge, Brushes.Black, posRectProducto.X + 30, posRectProducto.Y + 30);
                    break;
            }
            // FIN *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA

            if (_mensaje != "")
                e.Graphics.DrawString(_mensaje, textFontXSmall, Brushes.Black, posRectProducto.X + 10, posRectProducto.Y + 90);

            e.HasMorePages = (_iCopia < _nCopias);
            _iCopia++;
        }

        private void carroOxido80x80_PrintPage(object sender, PrintPageEventArgs e)
        {
            // *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA            

            Int32 longitud = 0;
            string nombre1 = "";
            string nombre2 = "";

            Point posNomCliente1 = new Point(20, 50);
            Point posNomCliente2 = new Point(20, 80);

            Point posCodCliente = new Point(posNomCliente2.X + 50, posNomCliente2.X + 120);

            Font textFont = new Font("Arial", 18);
            Font codeBarrFont = new Font("Code 39", 24);

            longitud = _nomCliente.Length;            

            if (longitud > 15)
            {
                nombre1 = _nomCliente.Substring(0, 18);
                nombre2 = _nomCliente.Substring(18, longitud - 18);
            }
            else
                nombre1 = _nomCliente;

            e.Graphics.DrawString(nombre1, textFont, Brushes.Black, posNomCliente1);
            e.Graphics.DrawString(nombre2, textFont, Brushes.Black, posNomCliente2);                        
            e.Graphics.DrawString(_codCliente, codeBarrFont, Brushes.Black, posCodCliente);

            e.Graphics.DrawString("OXIDO / GRASA", textFontXLarge, Brushes.Black, posRectProducto.X + 10, posRectProducto.Y + 30);            

            if (_mensaje != "")
                e.Graphics.DrawString(_mensaje, textFontXSmall, Brushes.Black, posRectProducto.X + 10, posRectProducto.Y + 90);

            e.HasMorePages = (_iCopia < _nCopias);
            _iCopia++;
        }

        private void carroOxido120x80_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawRectangle(pen, rectPedido);
            e.Graphics.DrawRectangle(pen, rectCliente);
            e.Graphics.DrawRectangle(pen, rectProducto);

            e.Graphics.DrawString(_nomCliente, textFontSmall, Brushes.Black, posRectCliente.X + 10, posRectCliente.Y + 10);
            e.Graphics.DrawString(_codCliente, codeBarrFontMedium, Brushes.Black, posRectCliente.X + 10, posRectCliente.Y + 30);

            e.Graphics.DrawString("OXIDO / GRASA", textFontXXLarge, Brushes.Black, posRectProducto.X + 10, posRectProducto.Y + 30);

            if (_mensaje != "")
                e.Graphics.DrawString(_mensaje, textFontXSmall, Brushes.Black, posRectProducto.X + 10, posRectProducto.Y + 90);

            e.HasMorePages = (_iCopia < _nCopias);
            _iCopia++;
        }

        private void carroTransporte80x80_PrintPage(object sender, PrintPageEventArgs e)
        {
            Point posNomCliente1 = new Point(20, 50);
            Point posNomCliente2 = new Point(20, 80);

            Point posCodCliente = new Point(posNomCliente2.X + 50, posNomCliente2.X + 120);

            Font textFont = new Font("Arial", 18);
            Font codeBarrFont = new Font("Code 39", 24);

            Int32 longitud = _nomCliente.Length;
            string nombre1="";
            string nombre2 = "";

            if (longitud > 18)
            {
                nombre1 = _nomCliente.Substring(0, 18);
                nombre2 = _nomCliente.Substring(18, longitud - 18);
            }
            else
                nombre1 = _nomCliente;
            
            e.Graphics.DrawString(nombre1, textFont, Brushes.Black, posNomCliente1);
            e.Graphics.DrawString(nombre2, textFont, Brushes.Black, posNomCliente2);

            e.Graphics.DrawString(_codCliente, codeBarrFont, Brushes.Black,posCodCliente);

            e.HasMorePages = (_iCopia < _nCopias);
            _iCopia++;
        }

        private void carroTransporte120x80_PrintPage(object sender, PrintPageEventArgs e)
        {
            Point posNomCliente = new Point(20, 50);
            Point posCodCliente = new Point(posNomCliente.X + 70, posNomCliente.X + 120);

            Font textFont = new Font("Arial", 18);
            Font codeBarrFont = new Font("Code 39", 24);

            e.Graphics.DrawString(_nomCliente, textFont, Brushes.Black, posNomCliente);
            e.Graphics.DrawString(_codCliente, codeBarrFont, Brushes.Black, posCodCliente);

            e.HasMorePages = (_iCopia < _nCopias);
            _iCopia++;
        }

        private void paqueteProducto_PrintPage(object sender, PrintPageEventArgs e)
        {
            // INI *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA
            //e.Graphics.DrawRectangle(pen, rectPedido);
            //e.Graphics.DrawRectangle(pen, rectCliente);
            //e.Graphics.DrawRectangle(pen, rectProducto);
            if (_dimensiones == "120x80")
            {
                e.Graphics.DrawRectangle(pen, rectPedido);
                e.Graphics.DrawRectangle(pen, rectCliente);
                e.Graphics.DrawRectangle(pen, rectProducto);

            }
            // FIN *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA

            e.Graphics.DrawString(_nomCliente, textFontSmall, Brushes.Black, posRectCliente.X + 10, posRectCliente.Y + 10);
            e.Graphics.DrawString(_codCliente, codeBarrFontMedium, Brushes.Black, posRectCliente.X + 10, posRectCliente.Y + 30);

            e.Graphics.DrawString(_numPedido.Replace("*", ""), textFontSmall, Brushes.Black, posRectPedido.X + 10, posRectPedido.Y + 10);
            e.Graphics.DrawString(_numPedido, codeBarrFontMedium, Brushes.Black, posRectPedido.X + 10, posRectPedido.Y + 30);

            e.Graphics.DrawString(_nomProducto, textFontMedium, Brushes.Black, posRectProducto.X + 10, posRectProducto.Y + 10);
            e.Graphics.DrawString(_codProducto, codeBarrFontLarge, Brushes.Black, posRectProducto.X + 10, posRectProducto.Y + 30);

            if (_mensaje != "")
                e.Graphics.DrawString(_mensaje, textFontXSmall, Brushes.Black, posRectProducto.X + 10, posRectProducto.Y + 90);

            e.HasMorePages = (_iCopia < _nCopias);
            _iCopia++;
        }

    }
}
