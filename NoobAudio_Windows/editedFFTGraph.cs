using MathNet.Numerics.IntegralTransforms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoobAudio_Windows
{
    public partial class editedFFTGraph : Form
    {
        public GlobalVars.DynamicVars DynamicVars;
        public editedFFTGraph()
        {
            InitializeComponent();
        }

        public void PlotFFT()
        {
            int jmp = 200;
            this.InvokeEx(f => this.DoubleBuffered = true);
            this.InvokeEx(f => f.pg.Value = 0);
            this.InvokeEx(f => f.chart1.Series["Frequência"].Points.Clear());
            this.InvokeEx(f => f.label2.Text = "A processar a transformada de Fourier");
            Complex[] complexArray = new Complex[DynamicVars.wavOps.GetWaveFile.Data.ProcessedSamples.Length];
            this.InvokeEx(f => f.pg.Maximum = (int)(complexArray.Length / 10));
            for (int i = 0; i < DynamicVars.wavOps.GetWaveFile.Data.ProcessedSamples.Length; i++)
                complexArray[i] = new Complex(DynamicVars.wavOps.GetWaveFile.Data.ProcessedSamples[i], 0);

            Fourier.Forward(complexArray);
            //Complex[] normalized = NoobAudioLib.Utils.fftNormalization(complexArray);
            this.InvokeEx(f => f.pg.Style = ProgressBarStyle.Blocks);
            this.InvokeEx(f => f.pg.Value = 0);
            this.InvokeEx(f => f.label2.Text = "A desenhar grafico");
            
            for(int i = 0; i < complexArray.Length / 10; i = i + jmp)
            {
                this.InvokeEx(f => f.chart1.ChartAreas["ChartArea1"].AxisX.Title = "Hz");
                this.InvokeEx(f => f.chart1.ChartAreas["ChartArea1"].AxisX.TitleFont = new Font("Arial", 14.0f));
                this.InvokeEx(f => f.chart1.ChartAreas["ChartArea1"].AxisX.MinorTickMark.Enabled = true);
                double mag = (2.0 / DynamicVars.wavOps.GetWaveFile.Data.ProcessedSamples.Length) * (Math.Abs(Math.Sqrt(Math.Pow(complexArray[i].Real, 2) +
                             Math.Pow(complexArray[i].Imaginary, 2))));
                double hzPerSample = DynamicVars.wavOps.GetWaveFile.Format.SamplesPerSec / complexArray.Length;
                this.InvokeEx(f => f.chart1.Series["Frequência"].Points.AddXY(hzPerSample * i, mag));
                this.InvokeEx(f => f.pg.Value = i);
            }
            this.InvokeEx(f => f.pg.Visible = false);
            this.InvokeEx(f => f.label2.Visible = false);
        }
    }
}
