namespace NoobAudio_Windows
{
    partial class FXEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FXEditor));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAddFX = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbAvailableFX = new System.Windows.Forms.ComboBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.btnRemoveEffect = new System.Windows.Forms.Button();
            this.lbEnabledFX = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1MinSize = 420;
            this.splitContainer1.Size = new System.Drawing.Size(1810, 719);
            this.splitContainer1.SplitterDistance = 629;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.button1);
            this.splitContainer2.Panel1.Controls.Add(this.btnAddFX);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.cbAvailableFX);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(629, 719);
            this.splitContainer2.SplitterDistance = 60;
            this.splitContainer2.SplitterWidth = 6;
            this.splitContainer2.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(494, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 35);
            this.button1.TabIndex = 3;
            this.button1.Text = "Desenhar FFT";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAddFX
            // 
            this.btnAddFX.Location = new System.Drawing.Point(352, 11);
            this.btnAddFX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddFX.Name = "btnAddFX";
            this.btnAddFX.Size = new System.Drawing.Size(135, 35);
            this.btnAddFX.TabIndex = 2;
            this.btnAddFX.Text = "Adicionar Efeito";
            this.btnAddFX.UseVisualStyleBackColor = true;
            this.btnAddFX.Click += new System.EventHandler(this.btnAddFX_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Efeitos:";
            // 
            // cbAvailableFX
            // 
            this.cbAvailableFX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAvailableFX.FormattingEnabled = true;
            this.cbAvailableFX.Items.AddRange(new object[] {
            "Fade",
            "Flanger",
            "Controlo de Volume",
            "Reverb",
            "Speed Shift"});
            this.cbAvailableFX.Location = new System.Drawing.Point(89, 14);
            this.cbAvailableFX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbAvailableFX.Name = "cbAvailableFX";
            this.cbAvailableFX.Size = new System.Drawing.Size(255, 28);
            this.cbAvailableFX.TabIndex = 0;
            this.cbAvailableFX.SelectedIndexChanged += new System.EventHandler(this.cbAvailableFX_SelectedIndexChanged);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.lbEnabledFX);
            this.splitContainer3.Panel1.Controls.Add(this.btnRemoveEffect);
            this.splitContainer3.Panel2MinSize = 250;
            this.splitContainer3.Size = new System.Drawing.Size(629, 653);
            this.splitContainer3.SplitterDistance = 221;
            this.splitContainer3.SplitterWidth = 6;
            this.splitContainer3.TabIndex = 0;
            // 
            // btnRemoveEffect
            // 
            this.btnRemoveEffect.Enabled = false;
            this.btnRemoveEffect.Location = new System.Drawing.Point(4, 559);
            this.btnRemoveEffect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRemoveEffect.Name = "btnRemoveEffect";
            this.btnRemoveEffect.Size = new System.Drawing.Size(213, 34);
            this.btnRemoveEffect.TabIndex = 1;
            this.btnRemoveEffect.Text = "Remover Efeito";
            this.btnRemoveEffect.UseVisualStyleBackColor = true;
            this.btnRemoveEffect.Click += new System.EventHandler(this.btnRemoveEffect_Click);
            // 
            // lbEnabledFX
            // 
            this.lbEnabledFX.FormattingEnabled = true;
            this.lbEnabledFX.ItemHeight = 20;
            this.lbEnabledFX.Location = new System.Drawing.Point(4, 5);
            this.lbEnabledFX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbEnabledFX.Name = "lbEnabledFX";
            this.lbEnabledFX.Size = new System.Drawing.Size(213, 544);
            this.lbEnabledFX.TabIndex = 0;
            this.lbEnabledFX.SelectedIndexChanged += new System.EventHandler(this.lbEnabledFX_SelectedIndexChanged);
            // 
            // FXEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1810, 719);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1832, 775);
            this.Name = "FXEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FXEditor";
            this.Load += new System.EventHandler(this.FXEditor_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ListBox lbEnabledFX;
        private System.Windows.Forms.Button btnAddFX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbAvailableFX;
        private System.Windows.Forms.Button btnRemoveEffect;
        private System.Windows.Forms.Button button1;
    }
}