namespace NoobAudio_Windows.FXPanels
{
    partial class Flanger
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.NUDtime = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.NUDrate = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.NUDamp = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NUDtime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDrate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDamp)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "Efeito de Flanger";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // NUDtime
            // 
            this.NUDtime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NUDtime.Location = new System.Drawing.Point(57, 42);
            this.NUDtime.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.NUDtime.Name = "NUDtime";
            this.NUDtime.Size = new System.Drawing.Size(152, 20);
            this.NUDtime.TabIndex = 5;
            this.NUDtime.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Duração";
            // 
            // NUDrate
            // 
            this.NUDrate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NUDrate.DecimalPlaces = 1;
            this.NUDrate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.NUDrate.Location = new System.Drawing.Point(57, 68);
            this.NUDrate.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.NUDrate.Name = "NUDrate";
            this.NUDrate.Size = new System.Drawing.Size(152, 20);
            this.NUDrate.TabIndex = 7;
            this.NUDrate.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Taxa";
            // 
            // NUDamp
            // 
            this.NUDamp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NUDamp.DecimalPlaces = 2;
            this.NUDamp.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.NUDamp.Location = new System.Drawing.Point(57, 94);
            this.NUDamp.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.NUDamp.Name = "NUDamp";
            this.NUDamp.Size = new System.Drawing.Size(152, 20);
            this.NUDamp.TabIndex = 9;
            this.NUDamp.Value = new decimal(new int[] {
            7,
            0,
            0,
            65536});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Amp";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(134, 120);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Aplicar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 39);
            this.label6.TabIndex = 12;
            this.label6.Text = "NOTA:\r\nDuração em ms (milisegundos)\r\nTaxa em Hz (Hertz)";
            // 
            // Flanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.NUDamp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.NUDrate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NUDtime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "Flanger";
            this.Size = new System.Drawing.Size(212, 233);
            ((System.ComponentModel.ISupportInitialize)(this.NUDtime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDrate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDamp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown NUDtime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown NUDrate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown NUDamp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label6;
    }
}
