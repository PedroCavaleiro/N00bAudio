using System;
using System.Windows.Forms;

namespace NoobAudio_Windows.FXPanels
{
    public partial class Reverb : UserControl
    {
        public GlobalVars.DynamicVars DynamicVars;
        private NoobAudioLib.FX.Reverb reverbFX;
        public Reverb()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ApplyReverb();
        }

        public void loadSettings()
        {
            if (DynamicVars.projectSettings.fxEditor.reverbSettings.Enabled)
            {
                try
                {
                    NUDtime.Value = DynamicVars.projectSettings.fxEditor.reverbSettings.Delay;
                    NUDrate.Value = (decimal)DynamicVars.projectSettings.fxEditor.reverbSettings.Decay;
                }
                catch (Exception ex)
                {
                    NUDtime.Value = 500;
                    NUDrate.Value = (decimal)0.5;
                }
            }
        }

        public void ApplyReverb()
        {
            Working(true);
            DynamicVars.projectSettings.fxEditor.reverbSettings.Decay = (float)NUDrate.Value;
            DynamicVars.projectSettings.fxEditor.reverbSettings.Delay = (int)NUDtime.Value;
            reverbFX = new NoobAudioLib.FX.Reverb(DynamicVars.wavOps.GetWaveFile, (int)NUDtime.Value, (float)NUDrate.Value);
            reverbFX.ProcessReverb();
            DynamicVars.fxEditor.on_fxChange();
            Working(false);
        }

        public override void Refresh()
        {
            ApplyReverb();
        }

        public override void ResetText()
        {
            DynamicVars.projectSettings.fxEditor.reverbSettings.Enabled = false;
            DynamicVars.projectSettings.fxEditor.reverbSettings.Decay = -1;
            DynamicVars.projectSettings.fxEditor.reverbSettings.Delay = -1;
        }

        public void Working(bool status)
        {
            if (status)
                this.Enabled = false;
            else
                this.Enabled = true;
        }

    }
}
