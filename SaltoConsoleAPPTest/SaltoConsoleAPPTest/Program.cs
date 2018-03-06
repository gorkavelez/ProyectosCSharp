using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaltoConsoleAPPTest.PageItem;
using System.Net;
using System.Web.Services;
using System.Web;


namespace SaltoConsoleAPPTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Program ClasPrograma = new Program();
            ClasPrograma.leerProductos();
        }

        public void leerProductos()
        {
            Item_Service ServItem = new Item_Service();
            
            ServItem.UseDefaultCredentials=true;
            Item_Filter vfiltroNo = new Item_Filter();
            vfiltroNo.Field = Item_Fields.No;
            vfiltroNo.Criteria = " ";           

            Item_Filter[] vfiltros = new Item_Filter[] { vfiltroNo };//,vfiltroFecha};                       
            
            PageItem.Item[] vItem = ServItem.ReadMultiple(vfiltros, "", 0);

            foreach(Item producto in vItem)
            {
                if (producto.Description != null)
                    Console.WriteLine(producto.No + " - " + producto.Description.ToString());
            }
            Console.Write("\nPress any key to continue... ");
            Console.ReadLine();           
            
        }
    }
}
