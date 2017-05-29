using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NoobAudioLib;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using System.Numerics;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;

namespace NoobAudio_Windows
{
	public partial class FileDetails : Form
	{
		public GlobalVars.DynamicVars DynamicVars;
		private WaveControl wc;
		public string FileName;
		public FileDetails()
		{
			InitializeComponent();
		}

		private void FileDetails_Load(object sender, EventArgs e)
		{
			lblBPS.Text = DynamicVars.wavOps.GetWaveFile.Format.BitsPerSample.ToString() + " bits/amostra";
			lblChannels.Text = DynamicVars.wavOps.GetWaveFile.Format.Channels.ToString();
			lblFileName.Text = FileName;
			lblMBps.Text = DynamicVars.wavOps.GetWaveFile.Format.AverageBytesPerSec.ToString() + " B/s";
			lblSampleRate.Text = DynamicVars.wavOps.GetWaveFile.Format.SamplesPerSec.ToString() + " Hz";
			lblSamplesCount.Text = DynamicVars.wavOps.GetWaveFile.Data.NumSamples.ToString();
			lblSize.Text = Utils.SizeSuffix((long)DynamicVars.wavOps.GetWaveFile.Data.DataSize);

			TimeSpan time = TimeSpan.FromSeconds(DynamicVars.wavOps.GetWaveFile.GetTrackLength);
			string str = time.ToString(@"hh\:mm\:ss");
			lblTime.Text = str;

			wc = new WaveControl();
			wc.Dock = DockStyle.Fill;
			splitContainer2.Panel1.Controls.Add(wc);
			wc.Read(DynamicVars.wavOps.GetWaveFile);
			wc.WaveformDrawError += Wc_WaveformDrawError;
			ThreadStart plotFFT = new ThreadStart(PlotFFT);
			Thread childThread = new Thread(plotFFT);
			childThread.Start();
		}

		private void PlotFFT()
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

            for (int i = 0; i < complexArray.Length / 10; i = i + jmp)
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

		private void Wc_WaveformDrawError(object source, Exception ex)
		{
			wc.Dispose();
			wc = new WaveControl();
			wc.Dock = DockStyle.Fill;
			splitContainer2.Panel1.Controls.Add(wc);
			wc.Read(DynamicVars.wavOps.GetWaveFile);
			wc.WaveformDrawError += Wc_WaveformDrawError;
		}

		private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
		{

		}
	}
	public static class ISynchronizationInvokeExtensions
	{
		public static void InvokeEx<T>(this T @this, Action<T> action) where T : ISynchronizeInvoke
		{
			if (@this.InvokeRequired)
				@this.Invoke(action, new object[] { @this });
			else
				action(@this);
		}
	}
}