using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace CapturaDatosPlantaWindowsForm.Funciones
{
    class ResolucionControles
    {
        float f_HeightRatio = new float();
        float f_WidthRatio = new float();
        public void ResizeForm(Form ObjForm, int DesignerHeight, int DesignerWidth)
        {
            #region Code for Resizing and Font Change According to Resolution            
            //se especifica aqui la resolucion del fomulario en el que este está diseñado
            //Si por ejemplo el formulario está diseñado en 800x600 poner DesignerHeight=600
            int i_StandardHeight = DesignerHeight;
            //Especificar la resolucion por compenente 
            //Si por ejemplo el formulario está diseñado en 800x600 poner DesignerWidth=800
            int i_StandardWidth = DesignerWidth;
            int i_PresentHeight = Screen.PrimaryScreen.Bounds.Height;//alto actual
            int i_PresentWidth = Screen.PrimaryScreen.Bounds.Width;//ancho actual
            f_HeightRatio = (float)((float)i_PresentHeight / (float)i_StandardHeight);
            f_WidthRatio = (float)((float)i_PresentWidth / (float)i_StandardWidth);
            ObjForm.AutoScaleMode = AutoScaleMode.None;//poner el modo de autoescalado a no, para que respete esta funcionalidad
            ObjForm.Scale(new SizeF(f_WidthRatio, f_HeightRatio));
            foreach (Control c in ObjForm.Controls)
            {
                if (c.HasChildren)
                {
                    ResizeControlStore(c);
                }
                else
                {
                    c.Font = new Font(c.Font.FontFamily, c.Font.Size * f_HeightRatio, c.Font.Style, c.Font.Unit, ((byte)(0)));
                }
            }
            ObjForm.Font = new Font(ObjForm.Font.FontFamily, ObjForm.Font.Size * f_HeightRatio, ObjForm.Font.Style, ObjForm.Font.Unit, ((byte)(0)));
            #endregion
        }

        private void ResizeControlStore(Control objCtl)
        {
            if (objCtl.HasChildren)
            {
                foreach (Control cChildren in objCtl.Controls)
                {
                    if (cChildren.HasChildren)
                    {
                        ResizeControlStore(cChildren);
                    }
                    else
                    {
                        cChildren.Font = new Font(cChildren.Font.FontFamily, cChildren.Font.Size * f_HeightRatio, cChildren.Font.Style, cChildren.Font.Unit, ((byte)(0)));
                    }
                }
                objCtl.Font = new Font(objCtl.Font.FontFamily, objCtl.Font.Size * f_HeightRatio, objCtl.Font.Style, objCtl.Font.Unit, ((byte)(0)));
            }
            else
            {
                objCtl.Font = new Font(objCtl.Font.FontFamily, objCtl.Font.Size * f_HeightRatio, objCtl.Font.Style, objCtl.Font.Unit, ((byte)(0)));
            }

        }

    }
}
