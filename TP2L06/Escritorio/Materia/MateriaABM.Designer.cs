using System;
using System.Windows.Forms;

namespace Escritorio.Materia
{
    partial class MateriaABM
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
            this.lblPlanes = new System.Windows.Forms.Label();
            this.txtHsTot = new System.Windows.Forms.TextBox();
            this.txtHsSem = new System.Windows.Forms.TextBox();
            this.txtDescMateria = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblHsTot = new System.Windows.Forms.Label();
            this.lblHsSem = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbBoxPlanes = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAceptar.Location = new System.Drawing.Point(278, 201);
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
            this.btnCancelar.Location = new System.Drawing.Point(382, 201);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(88, 25);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblPlanes
            // 
            this.lblPlanes.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPlanes.AutoSize = true;
            this.lblPlanes.Location = new System.Drawing.Point(41, 147);
            this.lblPlanes.Name = "lblPlanes";
            this.lblPlanes.Size = new System.Drawing.Size(28, 13);
            this.lblPlanes.TabIndex = 101;
            this.lblPlanes.Text = "Plan";
            // 
            // txtHsTot
            // 
            this.txtHsTot.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtHsTot.Location = new System.Drawing.Point(113, 113);
            this.txtHsTot.Name = "txtHsTot";
            this.txtHsTot.Size = new System.Drawing.Size(277, 20);
            this.txtHsTot.TabIndex = 5;
            // 
            // txtHsSem
            // 
            this.txtHsSem.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtHsSem.Location = new System.Drawing.Point(113, 77);
            this.txtHsSem.Name = "txtHsSem";
            this.txtHsSem.Size = new System.Drawing.Size(277, 20);
            this.txtHsSem.TabIndex = 3;
            // 
            // txtDescMateria
            // 
            this.txtDescMateria.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtDescMateria.Location = new System.Drawing.Point(113, 41);
            this.txtDescMateria.Name = "txtDescMateria";
            this.txtDescMateria.Size = new System.Drawing.Size(277, 20);
            this.txtDescMateria.TabIndex = 1;
            // 
            // txtID
            // 
            this.txtID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtID.Location = new System.Drawing.Point(113, 6);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(277, 20);
            this.txtID.TabIndex = 10;
            // 
            // lblHsTot
            // 
            this.lblHsTot.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblHsTot.AutoSize = true;
            this.lblHsTot.Location = new System.Drawing.Point(18, 116);
            this.lblHsTot.Name = "lblHsTot";
            this.lblHsTot.Size = new System.Drawing.Size(73, 13);
            this.lblHsTot.TabIndex = 3;
            this.lblHsTot.Text = "Horas Totales";
            // 
            // lblHsSem
            // 
            this.lblHsSem.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblHsSem.AutoSize = true;
            this.lblHsSem.Location = new System.Drawing.Point(10, 81);
            this.lblHsSem.Name = "lblHsSem";
            this.lblHsSem.Size = new System.Drawing.Size(90, 13);
            this.lblHsSem.TabIndex = 2;
            this.lblHsSem.Text = "Horas Semanales";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(4, 45);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(101, 13);
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Text = "Descripcion Materia";
            // 
            // lblID
            // 
            this.lblID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(46, 10);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(18, 13);
            this.lblID.TabIndex = 0;
            this.lblID.Text = "ID";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.06767F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.93233F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 260F));
            this.tableLayoutPanel1.Controls.Add(this.lblID, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblHsSem, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblHsTot, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblPlanes, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtID, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtDescMateria, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtHsSem, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtHsTot, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.cmbBoxPlanes, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblDescripcion, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(60, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.14286F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.85714F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(410, 167);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // cmbBoxPlanes
            // 
            this.cmbBoxPlanes.FormattingEnabled = true;
            this.cmbBoxPlanes.Location = new System.Drawing.Point(113, 144);
            this.cmbBoxPlanes.Name = "cmbBoxPlanes";
            this.cmbBoxPlanes.Size = new System.Drawing.Size(277, 21);
            this.cmbBoxPlanes.TabIndex = 102;
            // 
            // MateriaABM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 238);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MateriaABM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Materias";
            this.Load += new System.EventHandler(this.MateriaABM_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        #endregion
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblPlanes;
        private System.Windows.Forms.TextBox txtHsTot;
        private System.Windows.Forms.TextBox txtHsSem;
        private System.Windows.Forms.TextBox txtDescMateria;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblHsTot;
        private System.Windows.Forms.Label lblHsSem;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ComboBox cmbBoxPlanes;
    }
}