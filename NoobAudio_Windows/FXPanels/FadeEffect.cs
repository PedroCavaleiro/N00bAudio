using NoobAudioLib.Format.Wave;
using NoobAudioLib.FX;
using System;
using System.Windows.Forms;

namespace NoobAudio_Windows.FXPanels
{
    public partial class FadeEffect : UserControl
    {
        public GlobalVars.DynamicVars DynamicVars;
        private FadeInFadeOut fadein;
        private FadeInFadeOut fadeout;

        public FadeEffect()
        {
            InitializeComponent();           
        }
        

        public void ApplyFadeIn(int fadeInMs)
        {
            Working(true);
            DynamicVars.projectSettings.fxEditor.fadeSettings.FadeIn = fadeInMs;
            bool valid = false;
            fadein = new FadeInFadeOut((int)DynamicVars.wavOps.GetWaveFile.Format.SamplesPerSec,
                                                    DynamicVars.wavOps.GetWaveFile, fadeInMs, out valid);
            if (!valid)
                MessageBox.Show("Opps! Parece que não é possível aplicar um fade de " + fadeInMs + "ms neste ficheiro", "Fade demasiado longo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            else
            {
                fadein.FadeIn();
                DynamicVars.fxEditor.on_fxChange();
            }
            Working(false);
        }

        public void ApplyFadeOut(int fadeOutMs)
        {
            Working(true);
            DynamicVars.projectSettings.fxEditor.fadeSettings.FadeOut = fadeOutMs;
            bool valid = false;
            fadeout = new FadeInFadeOut((int)DynamicVars.wavOps.GetWaveFile.Format.SamplesPerSec,
                                                    DynamicVars.wavOps.GetWaveFile, fadeOutMs, out valid);
            if (!valid)
                MessageBox.Show("Opps! Parece que não é possível aplicar um fade de " + fadeOutMs + "ms neste ficheiro", "Fade demasiado longo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            else
            {
                fadeout.FadeOut();
                DynamicVars.fxEditor.on_fxChange();
            }
            Working(false);
        }

        #region UI
        private void button4_Click(object sender, EventArgs e)
        {
            Working(true);
            DynamicVars.projectSettings.fxEditor.fadeSettings.FadeOut = -1;
            DynamicVars.fxEditor.ReaplyAllFX();
            Working(false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Working(true);
            DynamicVars.projectSettings.fxEditor.fadeSettings.FadeIn = -1;
            DynamicVars.fxEditor.ReaplyAllFX();
            Working(false);
        }

        public void Working(bool status)
        {
            if (status)
                this.Enabled = false;
            else
                this.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ApplyFadeOut((int)numericUpDown2.Value);
        }

        public override void Refresh()
        {            
            if (DynamicVars.projectSettings.fxEditor.fadeSettings.FadeIn != -1)
                ApplyFadeIn(DynamicVars.projectSettings.fxEditor.fadeSettings.FadeIn);
            if (DynamicVars.projectSettings.fxEditor.fadeSettings.FadeOut != -1)
                ApplyFadeOut(DynamicVars.projectSettings.fxEditor.fadeSettings.FadeOut);
        }

        public void loadSettings()
        {
            if (DynamicVars.projectSettings.fxEditor.fadeSettings.Enabled)
            {
                if (DynamicVars.projectSettings.fxEditor.fadeSettings.FadeIn != -1)
                    numericUpDown1.Value = DynamicVars.projectSettings.fxEditor.fadeSettings.FadeIn;
                if (DynamicVars.projectSettings.fxEditor.fadeSettings.FadeOut != -1)
                    numericUpDown2.Value = DynamicVars.projectSettings.fxEditor.fadeSettings.FadeOut;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ApplyFadeIn((int)numericUpDown1.Value);
        }

        public override void ResetText()
        {
            DynamicVars.projectSettings.fxEditor.fadeSettings.Enabled = false;
            DynamicVars.projectSettings.fxEditor.fadeSettings.FadeIn = -1;
            DynamicVars.projectSettings.fxEditor.fadeSettings.FadeOut = -1;
        }
        #endregion
    }
}
