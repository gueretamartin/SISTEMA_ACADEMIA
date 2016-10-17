﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Negocio;

namespace Escritorio.Materia
{
    public partial class MateriaABM : Base.FormularioBase
    {
        #region VARIABLES
        private Entidades.Materia materiaActual;
        // bool est = true;
        #endregion

        #region PROPIEDADES

        public Entidades.Materia MateriaActual
        {
            get { return materiaActual; }
            set { materiaActual = value; }
        }
        #endregion

        #region CONSTRUCTORES
        public MateriaABM()
        {
            InitializeComponent();
            this.lstBoxPlan.DataSource = (new ControladorPlanes()).dameTodos();
            this.lstBoxPlan.ValueMember = "Id";
            this.lstBoxPlan.DisplayMember = "DescripcionPlan";
        }

        //Recibe el modo del formulario. Internamete debe setear a ModoForm en el modo enviado, este constructor
        //servirá para las altas. Y no recibe ID porque será un nuevo usuario.
        public MateriaABM(ModoForm modo)
            : this()
        {
            this.Modo = modo;
        }

        //Recibe un entero que representa el ID del usuario y el Modo en que estará el Formulario
        public MateriaABM(int ID, ModoForm modo)
            : this()
        {
            this.Modo = modo;
            MateriaActual = (new ControladorMaterias()).dameUno(ID);
            MapearDeDatos();
            switch (modo)
            { //Dependiendo el modo, la ventana de carga como se setea
                case (ModoForm.Alta):
                    this.btnAceptar.Text = "Guardar";
                    break;
                case (ModoForm.Modificacion):
                    this.btnAceptar.Text = "Guardar";
                    break;
                case (ModoForm.Baja):
                    this.btnAceptar.Text = "Eliminar";
                    break;
                case (ModoForm.Consulta):
                    this.btnAceptar.Text = "Aceptar";
                    break;
            }
        }


        #endregion

        #region METODOS


        public override void MapearDeDatos()
        {
            this.txtID.Text = this.MateriaActual.Id.ToString();
            this.txtHsSem.Text = this.MateriaActual.HorasSemanales.ToString();
            this.txtHsTot.Text = this.MateriaActual.HorasTotales.ToString();
            this.txtDescMateria.Text = this.MateriaActual.DescripcionMateria;
            this.lstBoxPlan.SelectedIndex = this.lstBoxPlan.FindString(MateriaActual.Plan.DescripcionPlan);
        }

        public override void GuardarCambios()
        {
            MapearADatos();
            new ControladorMaterias().save(MateriaActual);
        }

        public override void MapearADatos()
        {
            //La propiedad State se setea dependiendo el Modo del Formulario
            switch (this.Modo)
            {
                case (ModoForm.Alta):
                    {
                        MateriaActual = new Entidades.Materia();
                        Personas p = new Personas();
                        this.MateriaActual.HorasSemanales = Convert.ToInt32(this.txtHsSem.Text);
                        this.MateriaActual.HorasTotales = Convert.ToInt32(this.txtHsTot.Text);
                        this.MateriaActual.DescripcionMateria = this.txtDescMateria.Text;
                        this.MateriaActual.Plan = (new ControladorPlanes()).dameUno(Convert.ToInt32(this.lstBoxPlan.SelectedValue));
                        this.MateriaActual.State = Entidades.EntidadBase.States.New;
                        break;
                    }
                case (ModoForm.Modificacion):
                    {
                        this.MateriaActual.HorasSemanales = Convert.ToInt32(this.txtHsSem.Text);
                        this.MateriaActual.HorasTotales = Convert.ToInt32(this.txtHsTot.Text);
                        this.MateriaActual.DescripcionMateria = this.txtDescMateria.Text;
                        this.MateriaActual.Plan = (new ControladorPlanes()).dameUno(Convert.ToInt32(this.lstBoxPlan.SelectedValue));
                        this.MateriaActual.State = Entidades.EntidadBase.States.Modified;
                        break;
                    }
                case (ModoForm.Baja):
                    {
                        this.MateriaActual.State = Entidades.EntidadBase.States.Deleted;

                        break;
                    }
                case (ModoForm.Consulta):
                    {
                        this.MateriaActual.State = Entidades.EntidadBase.States.Unmodified;
                        break;
                    }
            }
        }
        public new void Notificar(string titulo, string mensaje, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            MessageBox.Show(mensaje, titulo, botones, icono);
        }


        public new void Notificar(string mensaje, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            this.Notificar(this.Text, mensaje, botones, icono);
        }

        #endregion



        #region Eventos

        private void btnAceptar_Click(object sender, EventArgs e)
        {
           
                GuardarCambios();
                Close();
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

      


        #endregion

      

        private void MateriaABM_Load(object sender, EventArgs e)
        {
           
            if (ModoForm.Baja == this.Modo)
            {
                this.lstBoxPlan.Visible = false;
                
            }


        }
         
}
}
