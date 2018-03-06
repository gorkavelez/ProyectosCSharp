using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de clPermisos
/// </summary>
public class clPermisos
{
	public clPermisos()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    public bool PermisoUsuarioMenu(string menuItem, string usuario)
    { 
        // Función que accede a base de datos para comprobar si el usuario que ha hecho
        // logín en la aplicación tiene permisos para ver la opción de menú
        dataAccess datos = new dataAccess();

        return (datos.PermisoUsuarioMenu(usuario,menuItem).Rows.Count>0);
    }

    public bool AutenticarUsuario(string usuario)
    {
        return (string.Compare(usuario, "JCA") == 0);        
    }
}
