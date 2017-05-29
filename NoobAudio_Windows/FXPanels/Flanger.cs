using System;
using System.Windows.Forms;

namespace NoobAudio_Windows.FXPanels
{
    public partial class Flanger : UserControl
    {
        public GlobalVars.DynamicVars DynamicVars;
        private NoobAudioLib.FX.Flanger flangerFX;

        public Flanger()
        {
            InitializeComponent();
        }

        public override void ResetText()
        {
            DynamicVars.projectSettings.fxEditor.flangerSettings.Enabled = false;
            DynamicVars.projectSettings.fxEditor.flangerSettings.Amp= -1;
            DynamicVars.projectSettings.fxEditor.flangerSettings.DelayTime = -1;
            DynamicVars.projectSettings.fxEditor.flangerSettings.Rate = -1;
        }

        public void loadSettings()
        {
            if (DynamicVars.projectSettings.fxEditor.flangerSettings.Enabled)
            {
                try
                {
                    NUDtime.Value = DynamicVars.projectSettings.fxEditor.flangerSettings.DelayTime;
                    NUDrate.Value = DynamicVars.projectSettings.fxEditor.flangerSettings.Rate;
                    NUDamp.Value = (decimal)DynamicVars.projectSettings.fxEditor.flangerSettings.Amp;
                }
                catch (Exception ex)
                {
                    NUDtime.Value = 15;
                    NUDrate.Value = 1;
                    NUDamp.Value = (decimal)0.7;
                }                
            }
        }

        public void Working(bool status)
        {
            if (status)
                this.Enabled = false;
            else
                this.Enabled = true;
        }

        public void ApplyFlanger()
        {
            Working(true);
            DynamicVars.projectSettings.fxEditor.flangerSettings.DelayTime = (int)NUDtime.Value;
            DynamicVars.projectSettings.fxEditor.flangerSettings.Rate = (int)NUDrate.Value;
            DynamicVars.projectSettings.fxEditor.flangerSettings.Amp = (double)NUDamp.Value;
            flangerFX = new NoobAudioLib.FX.Flanger(DynamicVars.wavOps.GetWaveFile, (int)NUDtime.Value, (int)NUDrate.Value, (double)NUDamp.Value);
            flangerFX.ProcessFlanger();
            DynamicVars.fxEditor.on_fxChange();
            Working(false);
        }

        public override void Refresh()
        {
            ApplyFlanger();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ApplyFlanger();
        }
    }
}
