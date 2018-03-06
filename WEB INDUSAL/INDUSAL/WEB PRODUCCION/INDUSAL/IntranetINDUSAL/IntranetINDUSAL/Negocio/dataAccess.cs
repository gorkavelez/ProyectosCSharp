using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Descripción breve de dataAccess
/// </summary>
public class dataAccess
{
    // cadena de conexión principal
    const string _strCnx = "Data Source=JCA\\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";
    string _strCnxAux = "";      // cadena de conexión opcional, para pruebas

    public string SqlCnxAux
    {
        get { return _strCnxAux; }
        set { _strCnxAux = value; }
    }

    public dataAccess()
    {
        // Constructor de la clase
    }

    private DataTable GetData(string strCmd)
    {
        // declaración de variables        
        SqlConnection sqlCnx;
        SqlDataAdapter adaptador;
        DataTable productos = new DataTable("datos");

        // creación de instancias de objetos        
        sqlCnx = new SqlConnection();
        // se asignan valores a la cadena de conexión y al comando        
        sqlCnx.ConnectionString = CadenaConexion();
        //strCmd = "SELECT * FROM [Products by Category] ORDER BY CategoryName,ProductName";
        // se crea la instancia de objeto adapter
        adaptador = new SqlDataAdapter(strCmd, sqlCnx);
        // se llena la tabla con los datos devueltos por el comando        
        adaptador.Fill(productos);

        return productos;
    }

    public DataTable Clientes()
    {
        string strCmd;
        strCmd = "SELECT CustomerID,CompanyName FROM Customers ORDER BY CustomerID";
        return GetData(strCmd);
    }

    public DataTable Productos()
    {
        // declaración de variables
        string strCmd;
        strCmd = "SELECT * FROM [Products by Category] ORDER BY CategoryName,ProductName";
        return GetData(strCmd);
    }

    public DataTable Productos(string customerID)
    {
        // declaración de variables
        StringBuilder strCmd = new StringBuilder("SELECT [Products by Category].*");
        strCmd.Append(" FROM [Products by Category] INNER JOIN [Customer Categories]");
        strCmd.Append(" ON [Products by Category].CategoryID = [Customer Categories].CategoryID");
        strCmd.Append(" WHERE [Customer Categories].CustomerID = '");
        strCmd.Append(customerID);
        strCmd.Append("'");
        return GetData(strCmd.ToString());
    }

    public DataTable Transportistas()
    {
        // declaración de variables
        string strCmd;
        strCmd = "SELECT codigo,nombre FROM [Transportistas] ORDER BY codigo";
        return GetData(strCmd);
    }

    public DataTable RutasTransportista(string codTransportista)
    {
        // declaración de variables
        StringBuilder strCmd = new StringBuilder();
        strCmd.Append("SELECT ruta,nombre,distancia FROM [vRutasTransportistas]");
        strCmd.Append(" WHERE transportista=");
        strCmd.Append(codTransportista);

        return GetData(strCmd.ToString());
    }

    public DataTable ClientesRuta(string codRuta)
    {
        // declaración de variables
        StringBuilder strCmd = new StringBuilder();
        strCmd.Append("SELECT cliente,nombre FROM [vClientesRutas]");
        strCmd.Append(" WHERE ruta=");
        strCmd.Append(codRuta);

        return GetData(strCmd.ToString());
    }

    public DataTable PermisoUsuarioMenu(string userID, string mItem)
    {
        // declaración de variables
        string strCmd;
        strCmd = "SELECT userID,menuItem FROM [MenuUsers]";
        strCmd += " WHERE userID='" + userID + "' AND menuItem='" + mItem + "'";
        return GetData(strCmd);
    }

    private string CadenaConexion()
    {
        // sólo si no se ha especificado una cadena de conexión alternativa, se
        // usa la principal
        if (string.Compare(_strCnxAux, "") == 0)
        {
            return (_strCnx);
        }
        else
        {
            return (_strCnxAux);
        }
    }
}
