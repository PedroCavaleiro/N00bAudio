using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoobAudio_Windows.FXPanels
{
    public partial class SpeedShift : UserControl
    {
        public GlobalVars.DynamicVars DynamicVars;

        public SpeedShift()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ApplySpeedShift((float)numericUpDown1.Value);
        }

        private void ApplySpeedShift(float multiplier)
        {
            Working(true);
            NoobAudioLib.FX.SpeedShift speedShiftFX = new NoobAudioLib.FX.SpeedShift(DynamicVars.wavOps.GetWaveFile);
            speedShiftFX.ShiftSpeed(multiplier);
            DynamicVars.projectSettings.fxEditor.speedShift.multiplier = multiplier;
            DynamicVars.fxEditor.on_fxChange();
            Working(false);
        }

        void ApplySpeedShiftBPM(int currBPM, int newBPM)
        {
			Working(true);
			NoobAudioLib.FX.SpeedShift speedShiftFX = new NoobAudioLib.FX.SpeedShift(DynamicVars.wavOps.GetWaveFile);
            speedShiftFX.SpeedShiftBPM(currBPM, newBPM);
            DynamicVars.projectSettings.fxEditor.speedShift.currBPM = currBPM;
            DynamicVars.projectSettings.fxEditor.speedShift.newBPM = newBPM;
			DynamicVars.fxEditor.on_fxChange();
			Working(false);
        }

        public override void Refresh()
        {
			if (DynamicVars.projectSettings.fxEditor.speedShift.Enabled)
			{
                if (DynamicVars.projectSettings.fxEditor.speedShift.bpm)
                    ApplySpeedShiftBPM(DynamicVars.projectSettings.fxEditor.speedShift.currBPM,
                                        DynamicVars.projectSettings.fxEditor.speedShift.newBPM);
                else
                    ApplySpeedShift(DynamicVars.projectSettings.fxEditor.speedShift.multiplier);
			}
        }

        public void Working(bool status)
        {
            if (status)
                this.Enabled = false;
            else
                this.Enabled = true;
        }

        public override void ResetText()
        {
            DynamicVars.projectSettings.fxEditor.speedShift.Enabled = false;
            DynamicVars.projectSettings.fxEditor.speedShift.multiplier = -1;
            DynamicVars.projectSettings.fxEditor.speedShift.currBPM = -1;
            DynamicVars.projectSettings.fxEditor.speedShift.newBPM = -1;
            DynamicVars.projectSettings.fxEditor.speedShift.bpm = false;
        }

        public void loadSettings()
        {
            checkBox1.Checked = DynamicVars.projectSettings.fxEditor.speedShift.bpm;
            if (DynamicVars.projectSettings.fxEditor.speedShift.Enabled)
                try{
                    numericUpDown1.Value = (decimal)DynamicVars.projectSettings.fxEditor.speedShift.multiplier;
            }
            catch(Exception ex) {
                    numericUpDown1.Value = 1;
            }
            try
            {
                numericUpDown2.Value = (decimal)DynamicVars.projectSettings.fxEditor.speedShift.currBPM;
            }
            catch (Exception ex)
            {
                numericUpDown2.Value = 1;
            }
            try
            {
                numericUpDown3.Value = (decimal)DynamicVars.projectSettings.fxEditor.speedShift.newBPM;
            }
            catch (Exception ex)
            {
                numericUpDown3.Value = 1;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            DynamicVars.projectSettings.fxEditor.speedShift.bpm = checkBox1.Checked;
            numericUpDown1.Enabled = !checkBox1.Checked;
            button1.Enabled = !checkBox1.Checked;
            button2.Enabled = checkBox1.Checked;
            numericUpDown2.Enabled = checkBox1.Checked;
            numericUpDown3.Enabled = checkBox1.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ApplySpeedShiftBPM((int)numericUpDown2.Value, (int)numericUpDown3.Value);
        }
    }
}
