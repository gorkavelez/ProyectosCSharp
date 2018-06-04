using System;
using System.IO;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using MS.WindowsAPICodePack.Internal;
using System.Diagnostics;

namespace CambiarAttFicheros
{
   class Program
   {
       private static string GetValue(IShellProperty value)
        {
            if (value == null || value.ValueAsObject == null)
            {
                return String.Empty;
            }
            return value.ValueAsObject.ToString();
        }

        private static bool SetValue(string filePath)
        {
            try
            {
                var shellFile = ShellFile.FromParsingName(filePath);
                ShellPropertyWriter MetadataProperties = shellFile.Properties.GetPropertyWriter();            
                MetadataProperties.WriteProperty(SystemProperties.System.Title,"titulo nuevo");   
                MetadataProperties.WriteProperty(SystemProperties.System.Author,"Autor nuevo");
                MetadataProperties.WriteProperty(SystemProperties.System.Photo.DateTaken,DateTime.Now);
                MetadataProperties.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //value.PropertyKey,object
            return true;
        }
      

        static void Main(string[] args)
        {                  
            // if (args.Length != 1)                      
            //     return;            
            // string filename = args[0];
            DirectoryInfo directory = new DirectoryInfo(@"C:\PDF\");
            FileInfo[] ficheros = directory.GetFiles("*.*");            
            foreach(FileInfo fichero in ficheros)
            {            
                string filename =fichero.FullName;
                if (!System.IO.File.Exists(filename))
                    return;
                ShellObject picture = ShellObject.FromParsingName(filename);
                if (picture != null)                
                {                   
                    var camera = GetValue(picture.Properties.
                    GetProperty(SystemProperties.System.Photo.CameraManufacturer));
                    var cameraModel = GetValue(picture.Properties.
                    GetProperty(SystemProperties.System.Photo.CameraModel));
                    var otracosa = GetValue(picture.Properties.GetProperty(SystemProperties.System.ItemDate));
                    var titulo = GetValue(picture.Properties.GetProperty(SystemProperties.System.Title));
                    var formattedString = String.Format("File {0} has Manufacturer {1} and Model {2} fecha captura {3} titulo {4}",
                                                        filename, camera, cameraModel,otracosa,titulo);
                    Trace.WriteLine(formattedString);
                    SetValue(filename);
                }        
                
            }
        }        
    }
}
