using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebTest
{
    abstract partial class WebForm : System.Web.UI.Page
    {
        protected enum FormModes
        {
            Alta,
            Baja,
            Modificacion
        }
        protected FormModes formMode
        {
            get { return (FormModes)this.ViewState["FormMode"]; }
            set { this.ViewState["FormMode"] = value; }
        }
        protected int IdSeleccionado
        {
            get
            {
                if (this.ViewState["idSeleccionado"] != null)
                    return (int)this.ViewState["idSeleccionado"];
                else
                    return 0;
            }
            set
            {
                this.ViewState["idSeleccionado"] = value;
            }
        }
       
        protected bool entidadSeleccionada
        {
            get
            {
                return this.IdSeleccionado != 0;
            }
        }
        public virtual void cargarForm(int id)
        {


        }
        public virtual void borrarEntidad(int id)
        {
        }
        public virtual void habilitarForm(bool enabled)
        {

        }
        public virtual void renovarForm()
        {
        }
        public virtual List<TextBox> obtenerTextBoxesAValidar()
        {
            return null;
        }
    }
}