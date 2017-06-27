using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GestSportEntidades.GestSport
{
    public class ExpresionesReg
    {
        public bool ValidarPasswordUser(string textoPass)
        {
            bool correcto = false;
            Regex Val = new Regex(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,12})$");
            if (Val.IsMatch(textoPass))
                correcto = true;
            return correcto;
        }

        public bool ValidarEmail(string textoEmail)
        {
            bool correcto = false;
            Regex Val = new Regex(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$");
            if (Val.IsMatch(textoEmail))
                correcto = true;
            return correcto;
        }
    }
}
