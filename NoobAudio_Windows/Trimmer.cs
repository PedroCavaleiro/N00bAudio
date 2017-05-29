using System;
using System.Windows.Forms;
using NoobAudioLib.Format.Wave;

namespace NoobAudio_Windows
{
    public partial class Trimmer : Form
    {
        public GlobalVars.DynamicVars DynamicVars;
        private WaveControl graph;

        public Trimmer()
        {
            InitializeComponent();
        }

        private void Trimmer_Load(object sender, EventArgs e)
        {
            graph = new WaveControl();
            graph.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(graph, 1, 0);
            graph.Read(DynamicVars.wavOps.GetWaveFile, true);
            numericUpDown1.Maximum = DynamicVars.wavOps.GetWaveFile.GetTrackLength * 1000;
            numericUpDown2.Maximum = DynamicVars.wavOps.GetWaveFile.GetTrackLength * 1000;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NoobAudioLib.FX.Trimmer trm = new NoobAudioLib.FX.Trimmer((int)DynamicVars.wavOps.GetWaveFile.Format.SamplesPerSec, DynamicVars.wavOps.GetWaveFile);
            trm.CutStart((int)numericUpDown1.Value);
            graph.Dispose();
            graph = new WaveControl();
            graph.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(graph, 1, 0);
            graph.Read(DynamicVars.wavOps.GetWaveFile, true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NoobAudioLib.FX.Trimmer trm = new NoobAudioLib.FX.Trimmer((int)DynamicVars.wavOps.GetWaveFile.Format.SamplesPerSec, DynamicVars.wavOps.GetWaveFile);
            trm.CutEnd((int)numericUpDown2.Value);
            graph.Dispose();
            graph = new WaveControl();
            graph.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(graph, 1, 0);
            graph.Read(DynamicVars.wavOps.GetWaveFile, true);
        }
    }
}
