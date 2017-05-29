using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace NoobAudio_Windows
{
    public partial class FXEditor : Form
    {
        public GlobalVars.DynamicVars DynamicVars;
        private List<UserControl> fxPanels;
        private WaveControl wc;

        public FXEditor()
        {
            InitializeComponent();
            fxPanels = new List<UserControl>();
        }

        private void loadFX(int fx, bool LoadSettings = false)
        {
            switch(fx)
            {
                case 0:
                    FXPanels.FadeEffect fadeFX = new FXPanels.FadeEffect();
                    fadeFX.DynamicVars = DynamicVars;
                    if (LoadSettings)
                        fadeFX.loadSettings();
                    DynamicVars.projectSettings.fxEditor.fadeSettings.Enabled = true;
                    fxPanels.Add(fadeFX);
                    lbEnabledFX.Items.Add("Fade");
                    loadFXPanel(0);
                    break;
                case 1:
                    
                    FXPanels.Flanger flangerFX = new FXPanels.Flanger();
                    flangerFX.DynamicVars = DynamicVars;
                    if (LoadSettings)
                        flangerFX.loadSettings();
                    DynamicVars.projectSettings.fxEditor.flangerSettings.Enabled = true;
                    fxPanels.Add(flangerFX);
                    lbEnabledFX.Items.Add("Flanger");
                    loadFXPanel(1);
                    break;
                case 2:
                    FXPanels.VolumeChange volFX = new FXPanels.VolumeChange();
                    volFX.DynamicVars = DynamicVars;
                    if (LoadSettings)
                        volFX.loadSettings();
                    DynamicVars.projectSettings.fxEditor.volumeSettings.Enabled = true;
                    fxPanels.Add(volFX);
                    lbEnabledFX.Items.Add("Controlo de Volume");
                    loadFXPanel(2);
                    break;
                case 3:
                    FXPanels.Reverb reverbFX = new FXPanels.Reverb();
                    reverbFX.DynamicVars = DynamicVars;
                    if (LoadSettings)
                        reverbFX.loadSettings();
                    DynamicVars.projectSettings.fxEditor.reverbSettings.Enabled = true;
                    fxPanels.Add(reverbFX);
                    lbEnabledFX.Items.Add("Reverb");
                    loadFXPanel(3);
                    break;
                case 4:
                    FXPanels.SpeedShift speedShiftFX = new FXPanels.SpeedShift();
                    speedShiftFX.DynamicVars = DynamicVars;
                    if (LoadSettings)
                        speedShiftFX.loadSettings();
                    DynamicVars.projectSettings.fxEditor.speedShift.Enabled = true;
                    fxPanels.Add(speedShiftFX);
                    lbEnabledFX.Items.Add("Speed Shift");
                    loadFXPanel(4);
                    break;
            }
        }

        public void ReaplyAllFX()
        {
            for(int i = 0; i < DynamicVars.wavOps.GetWaveFile.Data.ProcessedSamples.Length; i++)
                DynamicVars.wavOps.GetWaveFile.Data.ProcessedSamples[i] = DynamicVars.wavOps.GetWaveFile.Data[i];
            foreach (UserControl fxPanel in fxPanels)
                fxPanel.Refresh();
            wc.Dispose();
            wc = new WaveControl();
            wc.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(wc);
            wc.WaveformDrawError += Wc_WaveformDrawError;
            wc.Read(DynamicVars.wavOps.GetWaveFile, true);
        }

        private void loadSettings()
        {
            if (DynamicVars.projectSettings.fxEditor.fadeSettings.Enabled)
                loadFX(0, true);
            if (DynamicVars.projectSettings.fxEditor.flangerSettings.Enabled)
                loadFX(1, true);
            if (DynamicVars.projectSettings.fxEditor.volumeSettings.Enabled)
                loadFX(2, true);
            if (DynamicVars.projectSettings.fxEditor.reverbSettings.Enabled)
                loadFX(3, true);
            if (DynamicVars.projectSettings.fxEditor.speedShift.Enabled)
                loadFX(4, true);
        }

        #region UI
        public void on_fxChange()
        {
            wc.Dispose();
            wc = new WaveControl();
            wc.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(wc);
            wc.WaveformDrawError += Wc_WaveformDrawError; ;
            wc.Read(DynamicVars.wavOps.GetWaveFile, true);
            DynamicVars.mainWindow.audioProcessingOcurred();
        }

        private void lbEnabledFX_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadFXPanel(lbEnabledFX.SelectedIndex);
            btnRemoveEffect.Enabled = true;
        }

        private void btnAddFX_Click(object sender, EventArgs e)
        {
            loadFX(cbAvailableFX.SelectedIndex);
        }

        private void Wc_WaveformDrawError(object source, Exception ex)
        {
            //Poderá ocorrer um erro ao desenhar sobre a waveform existente. Reiniciar o controlo por complete corrige esse problema
            //Este erro era produzido tendo a janela FXEditor aberta e efetuar cortes ao ficheiro de audio
            wc.Dispose();
            wc = new WaveControl();
            wc.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(wc);
            wc.Read(DynamicVars.wavOps.GetWaveFile, true);
            wc.WaveformDrawError += Wc_WaveformDrawError;
        }

        private void loadFXPanel(int fx)
        {
            try
            {
                splitContainer3.Panel2.Controls.RemoveAt(0);
            }
            catch(Exception ex)
            { }
            try
            {
                splitContainer3.Panel2.Controls.Add(fxPanels[fx]);
            }
            catch (Exception ex)
            { /*um erro estranho esporádico*/ }
        }
        #endregion

        private void btnRemoveEffect_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Deseja remover o efeito " + lbEnabledFX.Items[lbEnabledFX.SelectedIndex].ToString().ToLower() + "?", "Remover efeito", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                fxPanels[lbEnabledFX.SelectedIndex].ResetText();
                splitContainer3.Panel2.Controls.Remove(fxPanels[lbEnabledFX.SelectedIndex]);
                fxPanels.RemoveAt(lbEnabledFX.SelectedIndex);
                lbEnabledFX.Items.RemoveAt(lbEnabledFX.SelectedIndex);                
                ReaplyAllFX();
            }
        }

        private void FXEditor_Load(object sender, EventArgs e)
        {
            loadSettings();
            wc = new WaveControl();
            wc.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(wc);
            wc.WaveformDrawError += Wc_WaveformDrawError;
            wc.Read(DynamicVars.wavOps.GetWaveFile, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            editedFFTGraph EditedFFTGraph = new editedFFTGraph();
            EditedFFTGraph.MdiParent = DynamicVars.mainWindow;
            EditedFFTGraph.DynamicVars = DynamicVars;
            EditedFFTGraph.Show();
            ThreadStart plotFFT = new ThreadStart(EditedFFTGraph.PlotFFT);
            Thread childThread = new Thread(plotFFT);
            childThread.Start();
        }

        private void cbAvailableFX_SelectedIndexChanged(object sender, EventArgs e)
        {
            splitContainer3.Panel2Collapsed = false;
        }
    }
}
