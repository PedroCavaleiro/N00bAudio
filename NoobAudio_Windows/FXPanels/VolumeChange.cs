using System;
using System.Windows.Forms;
using NoobAudioLib.FX;

namespace NoobAudio_Windows.FXPanels
{
    public partial class VolumeChange : UserControl
    {
        public GlobalVars.DynamicVars DynamicVars;

        public VolumeChange()
        {
            InitializeComponent();
        }

        public override void ResetText()
        {
            DynamicVars.projectSettings.fxEditor.volumeSettings.Enabled = false;
            DynamicVars.projectSettings.fxEditor.volumeSettings.Vol = -1;
            DynamicVars.projectSettings.fxEditor.volumeSettings.Override = false;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            decimal val = decimal.Round(trackBar1.Value / (decimal)10, 1);
            label2.Text = String.Format("Volume: {0}", val);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (MessageBox.Show("Ao sobrepor o nível máximo de volume poderá distorcer o som! Pertende prosseguir?", "Maximum Volume Override", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    checkBox1.Checked = false;
                else
                    trackBar1.Maximum = 100;
            }
            else
            {
                trackBar1.Maximum = 20;
                if (trackBar1.Value > 20)
                    trackBar1.Value = 10;
            }
            decimal val = decimal.Round(trackBar1.Value / (decimal)10, 1);
            label2.Text = String.Format("Volume: {0}", val);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ApplyVolumeChange();
        }

        public void loadSettings()
        {
            if (DynamicVars.projectSettings.fxEditor.volumeSettings.Enabled)
            {
                if (DynamicVars.projectSettings.fxEditor.volumeSettings.Override)
                    checkBox1.Checked = true;
                try{
                    trackBar1.Value = (int)DynamicVars.projectSettings.fxEditor.volumeSettings.Vol * 10;
                }
                catch (Exception ex)
                {
                    trackBar1.Value = 1;
                }
                label2.Text = String.Format("Volume: {0}", DynamicVars.projectSettings.fxEditor.volumeSettings.Vol);
            }
        }

        private void ApplyVolumeChange()
        {
            Working(true);
            DynamicVars.projectSettings.fxEditor.volumeSettings.Override= checkBox1.Checked;
            decimal val = decimal.Round(trackBar1.Value / (decimal)10, 1);
            DynamicVars.projectSettings.fxEditor.volumeSettings.Vol = (double)val;
            Volume volControl = new Volume(DynamicVars.wavOps.GetWaveFile);
            volControl.ProcessVolumeChange((double)val);
            DynamicVars.fxEditor.on_fxChange();
            Working(false);
        }

        public void Working(bool status)
        {
            if (status)
                this.Enabled = false;
            else
                this.Enabled = true;
        }

        public override void Refresh()
        {
            ApplyVolumeChange();
        }
    }
}
