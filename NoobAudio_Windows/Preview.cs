using System;
using System.Windows.Forms;

namespace NoobAudio_Windows
{
    public partial class Preview : Form
    {

        public GlobalVars.DynamicVars DynamicVars;

        public Preview()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = this.Size.Height / 2;
        }
    }
}
