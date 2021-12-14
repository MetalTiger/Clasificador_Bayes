namespace Clasificador
{
    partial class FRM_Clasificador
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnRuta = new System.Windows.Forms.Button();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPorcentajeEnt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAnalisis = new System.Windows.Forms.Button();
            this.txtColClase = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCategorias = new System.Windows.Forms.TextBox();
            this.dgvConfusion = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dgvMetricas = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAccuracy = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlResultados = new System.Windows.Forms.Panel();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.progresoBarra = new System.Windows.Forms.ProgressBar();
            this.lblEstado = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfusion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMetricas)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.pnlResultados.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // btnRuta
            // 
            this.btnRuta.Location = new System.Drawing.Point(570, 19);
            this.btnRuta.Margin = new System.Windows.Forms.Padding(4);
            this.btnRuta.Name = "btnRuta";
            this.btnRuta.Size = new System.Drawing.Size(96, 32);
            this.btnRuta.TabIndex = 2;
            this.btnRuta.Text = "Ruta...";
            this.btnRuta.UseVisualStyleBackColor = true;
            this.btnRuta.Click += new System.EventHandler(this.btnRuta_Click);
            // 
            // txtRuta
            // 
            this.txtRuta.Location = new System.Drawing.Point(157, 19);
            this.txtRuta.Margin = new System.Windows.Forms.Padding(4);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(392, 29);
            this.txtRuta.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cargar Dataset:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 37);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(212, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "Porcentaje de entrenamiento:";
            // 
            // txtPorcentajeEnt
            // 
            this.txtPorcentajeEnt.Location = new System.Drawing.Point(224, 34);
            this.txtPorcentajeEnt.Margin = new System.Windows.Forms.Padding(4);
            this.txtPorcentajeEnt.Name = "txtPorcentajeEnt";
            this.txtPorcentajeEnt.Size = new System.Drawing.Size(70, 29);
            this.txtPorcentajeEnt.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(302, 37);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "%";
            // 
            // btnAnalisis
            // 
            this.btnAnalisis.Location = new System.Drawing.Point(435, 153);
            this.btnAnalisis.Margin = new System.Windows.Forms.Padding(4);
            this.btnAnalisis.Name = "btnAnalisis";
            this.btnAnalisis.Size = new System.Drawing.Size(135, 55);
            this.btnAnalisis.TabIndex = 7;
            this.btnAnalisis.Text = "Análisis";
            this.btnAnalisis.UseVisualStyleBackColor = true;
            this.btnAnalisis.Click += new System.EventHandler(this.btnAnalisis_Click);
            // 
            // txtColClase
            // 
            this.txtColClase.Location = new System.Drawing.Point(157, 67);
            this.txtColClase.Margin = new System.Windows.Forms.Padding(4);
            this.txtColClase.Name = "txtColClase";
            this.txtColClase.Size = new System.Drawing.Size(54, 29);
            this.txtColClase.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 71);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 21);
            this.label5.TabIndex = 10;
            this.label5.Text = "Columna de Clase:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 94);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(193, 21);
            this.label6.TabIndex = 11;
            this.label6.Text = "Intervalo de discretización:";
            // 
            // txtCategorias
            // 
            this.txtCategorias.Location = new System.Drawing.Point(224, 91);
            this.txtCategorias.Margin = new System.Windows.Forms.Padding(4);
            this.txtCategorias.Name = "txtCategorias";
            this.txtCategorias.Size = new System.Drawing.Size(70, 29);
            this.txtCategorias.TabIndex = 6;
            // 
            // dgvConfusion
            // 
            this.dgvConfusion.AllowUserToDeleteRows = false;
            this.dgvConfusion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConfusion.Location = new System.Drawing.Point(7, 36);
            this.dgvConfusion.Margin = new System.Windows.Forms.Padding(4);
            this.dgvConfusion.Name = "dgvConfusion";
            this.dgvConfusion.ReadOnly = true;
            this.dgvConfusion.RowTemplate.Height = 25;
            this.dgvConfusion.Size = new System.Drawing.Size(425, 218);
            this.dgvConfusion.TabIndex = 13;
            this.dgvConfusion.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 11);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 21);
            this.label7.TabIndex = 14;
            this.label7.Text = "Matriz de Confusión:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 288);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(171, 21);
            this.label8.TabIndex = 15;
            this.label8.Text = "Métricas de Evaluación:";
            // 
            // dgvMetricas
            // 
            this.dgvMetricas.AllowUserToDeleteRows = false;
            this.dgvMetricas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMetricas.Location = new System.Drawing.Point(7, 313);
            this.dgvMetricas.Margin = new System.Windows.Forms.Padding(4);
            this.dgvMetricas.Name = "dgvMetricas";
            this.dgvMetricas.ReadOnly = true;
            this.dgvMetricas.RowTemplate.Height = 25;
            this.dgvMetricas.Size = new System.Drawing.Size(454, 213);
            this.dgvMetricas.TabIndex = 16;
            this.dgvMetricas.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 530);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 21);
            this.label9.TabIndex = 17;
            this.label9.Text = "Accuracy:";
            // 
            // txtAccuracy
            // 
            this.txtAccuracy.Location = new System.Drawing.Point(7, 555);
            this.txtAccuracy.Margin = new System.Windows.Forms.Padding(4);
            this.txtAccuracy.Name = "txtAccuracy";
            this.txtAccuracy.ReadOnly = true;
            this.txtAccuracy.Size = new System.Drawing.Size(127, 29);
            this.txtAccuracy.TabIndex = 18;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCategorias);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPorcentajeEnt);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(12, 103);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 144);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Validación Simple";
            // 
            // pnlResultados
            // 
            this.pnlResultados.Controls.Add(this.btnRegresar);
            this.pnlResultados.Controls.Add(this.dgvConfusion);
            this.pnlResultados.Controls.Add(this.label7);
            this.pnlResultados.Controls.Add(this.txtAccuracy);
            this.pnlResultados.Controls.Add(this.label9);
            this.pnlResultados.Controls.Add(this.dgvMetricas);
            this.pnlResultados.Controls.Add(this.label8);
            this.pnlResultados.Location = new System.Drawing.Point(12, 3);
            this.pnlResultados.Name = "pnlResultados";
            this.pnlResultados.Size = new System.Drawing.Size(654, 597);
            this.pnlResultados.TabIndex = 19;
            this.pnlResultados.Visible = false;
            // 
            // btnRegresar
            // 
            this.btnRegresar.Location = new System.Drawing.Point(497, 108);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(135, 55);
            this.btnRegresar.TabIndex = 19;
            this.btnRegresar.Text = "Regresar";
            this.btnRegresar.UseVisualStyleBackColor = true;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // progresoBarra
            // 
            this.progresoBarra.Location = new System.Drawing.Point(12, 628);
            this.progresoBarra.Name = "progresoBarra";
            this.progresoBarra.Size = new System.Drawing.Size(649, 23);
            this.progresoBarra.TabIndex = 20;
            this.progresoBarra.Visible = false;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(13, 603);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(59, 21);
            this.lblEstado.TabIndex = 21;
            this.lblEstado.Text = "Estado:";
            this.lblEstado.Visible = false;
            // 
            // FRM_Clasificador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 606);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.progresoBarra);
            this.Controls.Add(this.pnlResultados);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtColClase);
            this.Controls.Add(this.btnAnalisis);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRuta);
            this.Controls.Add(this.btnRuta);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FRM_Clasificador";
            this.Text = "Clasificador";
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfusion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMetricas)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlResultados.ResumeLayout(false);
            this.pnlResultados.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenFileDialog openFileDialog;
        private Button btnRuta;
        private TextBox txtRuta;
        private Label label1;
        private Label label3;
        private TextBox txtPorcentajeEnt;
        private Label label4;
        private Button btnAnalisis;
        private TextBox txtColClase;
        private Label label5;
        private Label label6;
        private TextBox txtCategorias;
        private DataGridView dgvConfusion;
        private Label label7;
        private Label label8;
        private DataGridView dgvMetricas;
        private Label label9;
        private TextBox txtAccuracy;
        private GroupBox groupBox1;
        private Panel pnlResultados;
        private Button btnRegresar;
        private ProgressBar progresoBarra;
        private Label lblEstado;
    }
}