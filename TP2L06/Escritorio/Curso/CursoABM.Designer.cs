﻿using System;

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
            this.txtCupo = new System.Windows.Forms.TextBox();
            this.txtAño = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblCupo = new System.Windows.Forms.Label();
            this.lblAño = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblMaterias = new System.Windows.Forms.Label();
            this.cmbBoxMaterias = new System.Windows.Forms.ComboBox();
            this.lblDenominacion = new System.Windows.Forms.Label();
            this.txtDenominacion = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAceptar.Location = new System.Drawing.Point(462, 165);
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
            this.btnCancelar.Location = new System.Drawing.Point(556, 165);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(88, 25);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtCupo
            // 
            this.txtCupo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCupo.Location = new System.Drawing.Point(348, 14);
            this.txtCupo.Name = "txtCupo";
            this.txtCupo.Size = new System.Drawing.Size(31, 20);
            this.txtCupo.TabIndex = 2;
            // 
            // txtAño
            // 
            this.txtAño.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtAño.Location = new System.Drawing.Point(71, 64);
            this.txtAño.Name = "txtAño";
            this.txtAño.Size = new System.Drawing.Size(153, 20);
            this.txtAño.TabIndex = 1;
            // 
            // txtID
            // 
            this.txtID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtID.Location = new System.Drawing.Point(71, 14);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(153, 20);
            this.txtID.TabIndex = 10;
            // 
            // lblCupo
            // 
            this.lblCupo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCupo.AutoSize = true;
            this.lblCupo.Location = new System.Drawing.Point(288, 17);
            this.lblCupo.Name = "lblCupo";
            this.lblCupo.Size = new System.Drawing.Size(32, 13);
            this.lblCupo.TabIndex = 5;
            this.lblCupo.Text = "Cupo";
            // 
            // lblAño
            // 
            this.lblAño.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAño.AutoSize = true;
            this.lblAño.Location = new System.Drawing.Point(21, 68);
            this.lblAño.Name = "lblAño";
            this.lblAño.Size = new System.Drawing.Size(26, 13);
            this.lblAño.TabIndex = 1;
            this.lblAño.Text = "Año";
            // 
            // lblID
            // 
            this.lblID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(25, 17);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(18, 13);
            this.lblID.TabIndex = 0;
            this.lblID.Text = "ID";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.86207F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.13793F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 254F));
            this.tableLayoutPanel1.Controls.Add(this.lblID, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblAño, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtID, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtAño, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCupo, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtCupo, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblMaterias, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmbBoxMaterias, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblDenominacion, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtDenominacion, 3, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(44, 18);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.14286F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.85714F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(600, 141);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // lblMaterias
            // 
            this.lblMaterias.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMaterias.AutoSize = true;
            this.lblMaterias.Location = new System.Drawing.Point(13, 114);
            this.lblMaterias.Name = "lblMaterias";
            this.lblMaterias.Size = new System.Drawing.Size(42, 13);
            this.lblMaterias.TabIndex = 104;
            this.lblMaterias.Text = "Materia";
            // 
            // cmbBoxMaterias
            // 
            this.cmbBoxMaterias.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbBoxMaterias.FormattingEnabled = true;
            this.cmbBoxMaterias.Location = new System.Drawing.Point(71, 110);
            this.cmbBoxMaterias.Name = "cmbBoxMaterias";
            this.cmbBoxMaterias.Size = new System.Drawing.Size(153, 21);
            this.cmbBoxMaterias.TabIndex = 106;
            // 
            // lblDenominacion
            // 
            this.lblDenominacion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDenominacion.AutoSize = true;
            this.lblDenominacion.Location = new System.Drawing.Point(266, 68);
            this.lblDenominacion.Name = "lblDenominacion";
            this.lblDenominacion.Size = new System.Drawing.Size(75, 13);
            this.lblDenominacion.TabIndex = 107;
            this.lblDenominacion.Text = "Denominacion";
            // 
            // txtDenominacion
            // 
            this.txtDenominacion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtDenominacion.Location = new System.Drawing.Point(348, 64);
            this.txtDenominacion.Name = "txtDenominacion";
            this.txtDenominacion.Size = new System.Drawing.Size(249, 20);
            this.txtDenominacion.TabIndex = 108;
            // 
            // CursoABM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 199);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CursoABM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Usuario";
            this.Load += new System.EventHandler(this.CursoABM_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

      

        #endregion
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtCupo;
        private System.Windows.Forms.TextBox txtAño;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblCupo;
        private System.Windows.Forms.Label lblAño;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblMaterias;
        private System.Windows.Forms.ComboBox cmbBoxMaterias;
        private System.Windows.Forms.Label lblDenominacion;
        private System.Windows.Forms.TextBox txtDenominacion;
    }
}