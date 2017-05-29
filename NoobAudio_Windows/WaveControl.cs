using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using NoobAudioLib.Format.Wave;

namespace NoobAudio_Windows
{

    public delegate void DrawError(object source, Exception ex);

    public class WaveControl : UserControl
	{

        public event DrawError WaveformDrawError;

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        private WaveFile _WaveFile;

		private bool _DrawWave = false;

		private string _Filename;

		private double _SamplesPerPixel = 0.0;

		private double _ZoomFactor;
 
		private int _StartX = 0;
 
		private int _EndX = 0;
 
		private int	_PrevX = 0;

		private bool _ResetRegion;

		private bool _AltKeyDown = false;
 
		private int _OffsetInSamples = 0;

        private MemoryStream _WaveStream;

		public string Filename
		{
			set { _Filename = value; }
			get { return _Filename; }
		}

        public MemoryStream WaveStream
        {
            set { _WaveStream = value; }
            get { return _WaveStream; }
        }

		private double SamplesPerPixel
		{
			set
			{
				_SamplesPerPixel = value;
				_ZoomFactor = _SamplesPerPixel / 25;
			}
		}

        private bool ProcessedAudio;

		public WaveControl()
		{
			InitializeComponent();

			// sets up double buffering
			SetStyle(ControlStyles.UserPaint|ControlStyles.AllPaintingInWmPaint|ControlStyles.DoubleBuffer, true);
		}

        public void Read(WaveFile waveFile, bool RenderProcessedAudio = false)
		{
            _WaveFile = waveFile; //new WaveFile(_Filename, _WaveStream);

            //_WaveFile.Read();

            ProcessedAudio = RenderProcessedAudio;
			
			_DrawWave = true;

			Refresh();
		}

		protected override void Dispose(bool disposing)
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code

		private void InitializeComponent()
		{
			// 
			// WaveControl
			// 
			this.BackColor = System.Drawing.Color.SkyBlue;
			this.Name = "WaveControl";
			this.Size = new System.Drawing.Size(616, 328);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WaveControl_MouseUp);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.WaveControl_Paint);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.WaveControl_KeyUp);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WaveControl_KeyDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WaveControl_MouseMove);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WaveControl_MouseDown);

		}
		#endregion

		private void WaveControl_Paint(object sender, PaintEventArgs e)
		{
			Pen pen = new Pen( ForeColor );

			if (_DrawWave)
			{
				Draw( e, pen );
			}
			
			int regionStartX = Math.Min(_StartX, _EndX);
			int regionEndX = Math.Max(_StartX, _EndX);
			SolidBrush brush = new SolidBrush(Color.FromArgb(128, 0, 0, 0));
			e.Graphics.FillRectangle(brush, regionStartX, 0, regionEndX - regionStartX, e.Graphics.VisibleClipBounds.Height);
		}

		protected override void OnMouseWheel(MouseEventArgs mea)
		{
			if (mea.Delta * SystemInformation.MouseWheelScrollLines / 120 > 0)
				ZoomIn();
			else
				ZoomOut();

			Refresh();
		}

		private void Draw(PaintEventArgs pea, Pen pen)
		{
			Graphics grfx = pea.Graphics;

			RectangleF visBounds = grfx.VisibleClipBounds;

			if (_SamplesPerPixel == 0.0)
			{
				this.SamplesPerPixel = (_WaveFile.Data.NumSamples / visBounds.Width);
			}

			grfx.DrawLine(pen, 0, visBounds.Height / 2, visBounds.Width, visBounds.Height / 2);

			grfx.TranslateTransform(0, visBounds.Height);
			grfx.ScaleTransform(1, -1);

			if (_WaveFile.Format.BitsPerSample == 16)
				Draw16Bit(grfx, pen, visBounds);
		}

		private void Draw16Bit(Graphics grfx, Pen pen, RectangleF visBounds)
		{
			int prevX = 0;
			int prevY = 0;

			int i = 0;

			int index = _OffsetInSamples;
			int maxSampleToShow = (int) ((_SamplesPerPixel * visBounds.Width ) + _OffsetInSamples);

			maxSampleToShow = Math.Min(maxSampleToShow, _WaveFile.Data.NumSamples);
            bool err = false;
			while (index < maxSampleToShow)
			{
				short maxVal = -32767;
				short minVal = 32767;

				for ( int x = 0; x < _SamplesPerPixel; x++ )
				{
                    if(ProcessedAudio)
                    {               
                        try
                        {
                            maxVal = Math.Max(maxVal, _WaveFile.Data.ProcessedSamples[x + index]);
                            minVal = Math.Min(minVal, _WaveFile.Data.ProcessedSamples[x + index]);
                        }
                        catch(Exception ex)
                        {                            
                            WaveformDrawError(this, ex);
                            err = true;
                            break;
                        }                            
                    }
                    else
                    {
                        maxVal = Math.Max(maxVal, _WaveFile.Data[x + index]);
                        minVal = Math.Min(minVal, _WaveFile.Data[x + index]);
                    }
					
				}
                if (err)
                    break;

				// scales based on height of window
				int scaledMinVal = (int) (( (minVal + 32768) * visBounds.Height ) / 65536 );
				int scaledMaxVal = (int) (( (maxVal + 32768) * visBounds.Height ) / 65536 );

				if ( _SamplesPerPixel > 0.0000000001 )
				{
					if ( scaledMinVal == scaledMaxVal )
					{
						if ( prevY != 0 )
							grfx.DrawLine( pen, prevX, prevY, i, scaledMaxVal );
					}
					else
					{
						grfx.DrawLine( pen, i, scaledMinVal, i, scaledMaxVal );
					}
				}
				else
					return;

				prevX = i;
				prevY = scaledMaxVal;
				
				i++;
				index = (int) ( i * _SamplesPerPixel) + _OffsetInSamples;
			}
		}

		private void ZoomIn()
		{
			_SamplesPerPixel -= _ZoomFactor;
		}

		private void ZoomOut()
		{
			_SamplesPerPixel += _ZoomFactor;
		}

		private void ZoomToRegion()
		{
			int regionStartX = Math.Min(_StartX, _EndX);
			int regionEndX = Math.Max(_StartX, _EndX);

			regionStartX = Math.Max(0, regionStartX);
			regionEndX = Math.Max(0, regionEndX);

			_OffsetInSamples += (int) (regionStartX * _SamplesPerPixel);

			int numSamplesToShow = (int) ((regionEndX - regionStartX) * _SamplesPerPixel);

			if (numSamplesToShow > 0)
			{
				this.SamplesPerPixel = (double) numSamplesToShow / this.Width;

				_ResetRegion = true;
			}
		}

		private void ZoomOutFull()
		{
			this.SamplesPerPixel = (_WaveFile.Data.NumSamples / this.Width);
			_OffsetInSamples = 0;

			_ResetRegion = true;
		}

		private void Scroll(int newXValue)
		{
			_OffsetInSamples -= (int) ((newXValue - _PrevX) * _SamplesPerPixel);

			if (_OffsetInSamples < 0)
				_OffsetInSamples = 0;
		}

		private void WaveControl_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (_AltKeyDown)
				{
					_PrevX = e.X;
				}
				else
				{
					_StartX = e.X;
					_ResetRegion = true;
				}
			}
			else if (e.Button == MouseButtons.Right)
			{
				if (e.Clicks == 2)
					ZoomOutFull();
				else
					ZoomToRegion();
			}
		}

		private void WaveControl_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (_AltKeyDown)
				{
					Scroll(e.X);
				}
				else
				{
					_EndX = e.X;
					_ResetRegion = false;
				}

				_PrevX = e.X;

				Refresh( );
			}
		}

		private void WaveControl_MouseUp(object sender, MouseEventArgs e)
		{
			if (_AltKeyDown)
				return;

			if (_ResetRegion)
			{
				_StartX = 0;
				_EndX = 0;

				Refresh();
			}
			else
			{
				_EndX = e.X;
			}
		}

		private void WaveControl_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Alt)
			{
				_AltKeyDown = true;
			}
		}

		private void WaveControl_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Menu)
			{
				_AltKeyDown = false;
			}
		}
	}
}
