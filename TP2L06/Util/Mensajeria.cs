using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Util
{
    public static class Mensajeria
    {
        public static void MostrarErrores(List<string> errores)
        {
            string mensaje = "";
            foreach(string msj in errores)
            {
                mensaje += msj + "\n";
            }
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static String MostrarErroresTexto(List<string> errores)
        {
            string mensaje = "";
            foreach (string msj in errores)
            {
                mensaje += msj + "\n";
            }
            return (mensaje);
        }


        public static void MostrarExito(string exito)
        {
            MessageBox.Show(exito, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static String MostrarExitoTexto(string exito)
        {
            return (exito);
        }

        public static void MostrarAlerta(string alerta)
        {
            MessageBox.Show(alerta, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static String MostrarAlertaTexto(string alerta)
        {
            return (alerta);
        }



    }
}
