using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WF_PruebaEtiquetasIndusal
{
    static class Program
    {
        // MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {
                cPrintDocument oPrinter = new cPrintDocument("");
                int nCopias=1;
                string separador="#";

                foreach (string param in args)
                {
                    string[] dupla = param.Split(';');
                    switch (dupla[0].ToUpper())
                    {
                        case "SP":
                            separador = dupla[1];
                            break;
                        case "TE":
                            switch (dupla[1])
                            {
                                case "1":
                                    oPrinter.TipoDocumento = cPrintDocument.eTipoDocumento.carroIncompleto;
                                    break;
                                case "2":
                                    oPrinter.TipoDocumento = cPrintDocument.eTipoDocumento.carroLavado;
                                    break;
                                case "3":
                                    oPrinter.TipoDocumento = cPrintDocument.eTipoDocumento.carroOxido;
                                    break;
                                case "4":
                                    oPrinter.TipoDocumento = cPrintDocument.eTipoDocumento.carroTransporte;
                                    break;
                                case "5":
                                    oPrinter.TipoDocumento = cPrintDocument.eTipoDocumento.paqueteProducto;
                                    break;
                            }                            
                            break;
                        case "CC":
                            oPrinter.CodCliente = dupla[1];
                            break;
                        case "NC":
                            oPrinter.NomCliente = dupla[1].Replace(separador," ");
                            break;
                        case "NP":
                            oPrinter.NumPedido= dupla[1];
                            break;
                        case "CP":
                            oPrinter.CodProducto = dupla[1];
                            break;
                        case "DP":
                            oPrinter.NomProducto = dupla[1].Replace(separador, " ");
                            break;
                        case "NR":
                            oPrinter.NCarro = dupla[1];
                            break;
                        case "LT":
                            oPrinter.Mensaje = dupla[1].Replace(separador, " ");
                            break;
                        case "NN":
                            nCopias=int.Parse(dupla[1]);
                            break;
// INI *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA
                        case "TM":
                            oPrinter.Dimensiones = dupla[1];
                            break;
// FIN *** MOD0001, JCA, 18/04/12; MODIFICACION PARA QUE SE IMPRIMA EN 120x80 o 80x80, SEGUN SE PIDA DESDE LA LLAMADA
                    }
                }

                oPrinter.Print(nCopias);
            }
        }
    }
}
