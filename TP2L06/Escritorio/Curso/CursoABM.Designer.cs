using System;

namespace Escritorio.Curso
{
    partial class CursoABM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblComisiones = new System.Windows.Forms.Label();
            this.lstBoxComisiones = new System.Windows.Forms.ListBox();
            this.txtCupo = new System.Windows.Forms.TextBox();
            this.txtAño = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblCupo = new System.Windows.Forms.Label();
            this.lblAño = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblMaterias = new System.Windows.Forms.Label();
            this.lstBoxMaterias = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAceptar.Location = new System.Drawing.Point(528, 380);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(88, 25);
            this.btnAceptar.TabIndex = 7;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCancelar.Location = new System.Drawing.Point(631, 380);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(88, 25);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblComisiones
            // 
            this.lblComisiones.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblComisiones.AutoSize = true;
            this.lblComisiones.Location = new System.Drawing.Point(76, 109);
            this.lblComisiones.Name = "lblComisiones";
            this.lblComisiones.Size = new System.Drawing.Size(119, 13);
            this.lblComisiones.TabIndex = 101;
            this.lblComisiones.Text = "Seleccione la Comision:";
            // 
            // lstBoxComisiones
            // 
            this.lstBoxComisiones.FormattingEnabled = true;
            this.lstBoxComisiones.Location = new System.Drawing.Point(79, 125);
            this.lstBoxComisiones.Name = "lstBoxComisiones";
            this.lstBoxComisiones.Size = new System.Drawing.Size(594, 108);
            this.lstBoxComisiones.TabIndex = 103;
            // 
            // txtCupo
            // 
            this.txtCupo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtCupo.Location = new System.Drawing.Point(336, 47);
            this.txtCupo.Name = "txtCupo";
            this.txtCupo.Size = new System.Drawing.Size(201, 20);
            this.txtCupo.TabIndex = 2;
            // 
            // txtAño
            // 
            this.txtAño.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtAño.Location = new System.Drawing.Point(51, 47);
            this.txtAño.Name = "txtAño";
            this.txtAño.Size = new System.Drawing.Size(153, 20);
            this.txtAño.TabIndex = 1;
            // 
            // txtID
            // 
            this.txtID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtID.Location = new System.Drawing.Point(51, 8);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(153, 20);
            this.txtID.TabIndex = 10;
            // 
            // lblCupo
            // 
            this.lblCupo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCupo.AutoSize = true;
            this.lblCupo.Location = new System.Drawing.Point(240, 50);
            this.lblCupo.Name = "lblCupo";
            this.lblCupo.Size = new System.Drawing.Size(32, 13);
            this.lblCupo.TabIndex = 5;
            this.lblCupo.Text = "Cupo";
            // 
            // lblAño
            // 
            this.lblAño.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAño.AutoSize = true;
            this.lblAño.Location = new System.Drawing.Point(11, 50);
            this.lblAño.Name = "lblAño";
            this.lblAño.Size = new System.Drawing.Size(26, 13);
            this.lblAño.TabIndex = 1;
            this.lblAño.Text = "Año";
            // 
            // lblID
            // 
            this.lblID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(15, 11);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(18, 13);
            this.lblID.TabIndex = 0;
            this.lblID.Text = "ID";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.32155F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.67844F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 262F));
            this.tableLayoutPanel1.Controls.Add(this.lblID, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblAño, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCupo, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtID, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtAño, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtCupo, 3, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(79, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.14286F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.85714F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(568, 78);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // lblMaterias
            // 
            this.lblMaterias.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMaterias.AutoSize = true;
            this.lblMaterias.Location = new System.Drawing.Point(76, 245);
            this.lblMaterias.Name = "lblMaterias";
            this.lblMaterias.Size = new System.Drawing.Size(109, 13);
            this.lblMaterias.TabIndex = 104;
            this.lblMaterias.Text = "Seleccione la Materia";
            // 
            // lstBoxMaterias
            // 
            this.lstBoxMaterias.FormattingEnabled = true;
            this.lstBoxMaterias.Location = new System.Drawing.Point(79, 261);
            this.lstBoxMaterias.Name = "lstBoxMaterias";
            this.lstBoxMaterias.Size = new System.Drawing.Size(594, 108);
            this.lstBoxMaterias.TabIndex = 105;
            // 
            // CursoABM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 417);
            this.Controls.Add(this.lstBoxMaterias);
            this.Controls.Add(this.lblMaterias);
            this.Controls.Add(this.lstBoxComisiones);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.lblComisiones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CursoABM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Usuario";
            this.Load += new System.EventHandler(this.CursoABM_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void CursoABM_Load(object sender, EventArgs e)
        {
            
        }

        #endregion
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblComisiones;
        private System.Windows.Forms.ListBox lstBoxComisiones;
        private System.Windows.Forms.TextBox txtCupo;
        private System.Windows.Forms.TextBox txtAño;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblCupo;
        private System.Windows.Forms.Label lblAño;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblMaterias;
        private System.Windows.Forms.ListBox lstBoxMaterias;
    }
}