namespace NoobAudio_Windows.FXPanels
{
    partial class Reverb
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
            this.NUDrate = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.NUDtime = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NUDrate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDtime)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 27);
            this.label1.TabIndex = 3;
            this.label1.Text = "Efeito de Reverb";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            this.NUDrate.Location = new System.Drawing.Point(57, 66);
            this.NUDrate.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.NUDrate.Name = "NUDrate";
            this.NUDrate.Size = new System.Drawing.Size(152, 20);
            this.NUDrate.TabIndex = 11;
            this.NUDrate.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Decay";
            // 
            // NUDtime
            // 
            this.NUDtime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NUDtime.Location = new System.Drawing.Point(57, 40);
            this.NUDtime.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.NUDtime.Name = "NUDtime";
            this.NUDtime.Size = new System.Drawing.Size(152, 20);
            this.NUDtime.TabIndex = 9;
            this.NUDtime.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Atraso";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(134, 92);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Aplicar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 26);
            this.label6.TabIndex = 13;
            this.label6.Text = "NOTA:\r\nAtraso em ms (milisegundos)";
            // 
            // Reverb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.NUDrate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NUDtime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "Reverb";
            this.Size = new System.Drawing.Size(212, 233);
            ((System.ComponentModel.ISupportInitialize)(this.NUDrate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDtime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown NUDrate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown NUDtime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label6;
    }
}
