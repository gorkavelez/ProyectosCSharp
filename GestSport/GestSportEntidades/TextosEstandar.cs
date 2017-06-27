using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestSportEntidades.GestSport
{
    public class TextosEstandar
    {
        string LiteralError = string.Empty;

#region Texto para los Errores

        public string textoError1
        {
            get { LiteralError = "La contraseña antigua no es correcta"; return LiteralError; }
        }

        public string textoError2
        {
            get { LiteralError = "la contraseña debe tener al menos un número"; return LiteralError; }
        }

        public string textoError3
        {
            get { LiteralError = "La contraseña debe tener al menos un caracter en mayusculas"; return LiteralError; }            
        }
        
        public string textoError4
        {
            get { LiteralError = "la contraseña debe tener al menos 8 posiciones"; return LiteralError; }
        }

        public string textoError5
        {
            get { LiteralError = "Los campos deben estar completados"; return LiteralError; }
        }

        public string textoErrorLogin
        {
            get { LiteralError = "Usuario o contraseña incorrecta"; return LiteralError; }
        }

        public string textoNoExisteEmail
        {
            get { LiteralError = "No hay ningún usuario con ese eMail"; return LiteralError; }
        }

        public string textoEmailErroneo
        {
            get { LiteralError = "El email introducido no es correcto"; return LiteralError; }
        }

        public string textoEmailNoCoincide
        {
            get { LiteralError = "La confirmación del email no coincide"; return LiteralError; }
        }

        public string textoContraseñaNoEsCorrecta
        {
            get { LiteralError = "La contraseña no es correcta"; return LiteralError; }
        }

        public string textoFaltaNombre
        {
            get { LiteralError = "Falta introducir el nombre"; return LiteralError; }
        }

        public string textoFaltaApellido
        {
            get { LiteralError = "Falta introducir el apellido"; return LiteralError; }
        }

        public string textoUsuario
        {
            get { LiteralError = "Falta introducir el usuario"; return LiteralError; }
        }

        public string textoErrorUsuario
        {
            get { LiteralError = "Ya existe el usuario"; return LiteralError; }
        }


#endregion

    }
}
