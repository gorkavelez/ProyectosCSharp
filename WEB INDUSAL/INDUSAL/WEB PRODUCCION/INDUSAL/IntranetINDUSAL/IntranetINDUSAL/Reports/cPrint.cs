using System;
using System.IO;
using System.Data;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Web;
using Microsoft.Reporting.WebForms;

public class cPrint : IDisposable
{
    private int m_currentPageIndex;
    private IList<Stream> m_streams;    

    //private DataTable LoadSalesData()
    //{
    //    // Create a new DataSet and read sales data file 
    //    //    data.xml into the first DataTable.
    //    DataSet dataSet = new DataSet();
    //    dataSet.ReadXml(@"..\..\data.xml");
    //    return dataSet.Tables[0];
    //}

    // Routine to provide to the report renderer, in order to
    //    save an image for each page of the report.

    private Stream CreateStream(string name,
      string fileNameExtension, Encoding encoding,
      string mimeType, bool willSeek)
    {
        Stream stream = new FileStream(@"..\..\" + name +
           "." + fileNameExtension, FileMode.Create);
        //Stream stream = new FileStream(@"C:\PDFs\Prueba.jpg", FileMode.Create);
        m_streams.Add(stream);
        return stream;
    }

    // Export the given report as an EMF (Enhanced Metafile) file.
    
    private void Export(LocalReport report)
    {
        string deviceInfo =
          "<DeviceInfo>" +
          "  <OutputFormat>EMF</OutputFormat>" +
          "  <PageWidth>4.72in</PageWidth>" +
          "  <PageHeight>3.15in</PageHeight>" +
          "  <MarginTop>0.25in</MarginTop>" +
          "  <MarginLeft>0.25in</MarginLeft>" +
          "  <MarginRight>0.25in</MarginRight>" +
          "  <MarginBottom>0.25in</MarginBottom>" +
          "</DeviceInfo>";
        
        
        Warning[] warnings;
        m_streams = new List<Stream>();
        report.Render("Image", deviceInfo, CreateStream,
           out warnings);        
        foreach (Stream stream in m_streams)
            stream.Position = 0;
    }
    // Handler for PrintPageEvents
    
    private void PrintPage(object sender, PrintPageEventArgs ev)
    {
        Metafile pageImage = new
           Metafile(m_streams[m_currentPageIndex]);
        ev.Graphics.DrawImage(pageImage, ev.PageBounds);
        m_currentPageIndex++;
        ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
    }

    private void Print()
    {
        const string printerName =
           "Acrobat Distiller";
        if (m_streams == null || m_streams.Count == 0)
            return;
        PrintDocument printDoc = new PrintDocument();
        printDoc.PrinterSettings.PrinterName = printerName;
        if (!printDoc.PrinterSettings.IsValid)
        {
            string msg = String.Format(
               "Can't find printer \"{0}\".", printerName);
            //MessageBox.Show(msg, "Print Error");
            return;
        }
        printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
        printDoc.Print();
    }
    
    // Create a local report for Report.rdlc, load the data,
    //    export the report to an .emf file, and print it.
    
    public void Run(string _rptPath,DataTable _dt)
    {
        LocalReport report = new LocalReport();
        //report.ReportPath = @"..\..\Report.rdlc";
        //report.ReportPath = "rptReporting2.rdlc";
        report.ReportPath = @"Reports\"+_rptPath;
        report.DataSources.Add(new ReportDataSource("dsEtiquetas_dtClientePedidoProducto", _dt));
        //report.DataSources.Add(
        //   new ReportDataSource("Sales", LoadSalesData()));
        Export(report);
        m_currentPageIndex = 0;
        Print();
    }

    public void Dispose()
    {
        if (m_streams != null)
        {
            foreach (Stream stream in m_streams)
                stream.Close();
            m_streams = null;
        }
    }

    //public static void Main(string[] args)
    //{
    //    using (Demo demo = new Demo())
    //    {
    //        demo.Run();
    //    }
    //}
}
