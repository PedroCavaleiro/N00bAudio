using System;
using System.Windows.Forms;
using NoobAudioLib;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;

namespace NoobAudio_Windows
{
    public partial class NoobAudio : Form
    {
        [Serializable]
        public class ProjectData
        {
            public byte[] projectSettings;
            public byte[] projectData;
        }
        private int childFormNumber = 0;

        private FileDetails childForm;
        private GlobalVars.DynamicVars dynamicVars;
        private bool OriginalPlaying = false;
        private bool ModifiedPlaying = false;
        private SPlayer spo;
        private SPlayer spm;
        private Trimmer trimForm;

        public NoobAudio()
        {
            InitializeComponent();
            dynamicVars = new GlobalVars.DynamicVars();
            dynamicVars.mainWindow = this;
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            //FXEditor childForm = new FXEditor();
            //childForm.DynamicVars = dynamicVars;
            //childForm.MdiParent = this;
            //childForm.Text = "Janela " + childFormNumber++;
            //childForm.Show();
        }

        private void loadFile(string FileName, string SafeFileName, bool loadProject)
        {
            if(!loadProject)
                dynamicVars.projectSettings = new GlobalVars.DynamicVars.ProjectSettings();
            if (dynamicVars.wavOps != null)
                dynamicVars.wavOps.Dispose();
            if (childForm != null)
            {
                childForm.Dispose();
                childForm.Close();
            }
            if(trimForm != null)
            {
                trimForm.Dispose();
                trimForm.Close();
            }
            dynamicVars.wavOps = new StdOps(FileName);
            dynamicVars.projectSettings.fileLocation = FileName;
            tslFileLoaded.Text = FileName;
            childForm = new FileDetails();
            childForm.MdiParent = this;
            childForm.DynamicVars = dynamicVars;
            childForm.FileName = SafeFileName;
            childForm.Show();
            editMenu.Enabled = true;
            if (dynamicVars.fxEditor != null)
                dynamicVars.fxEditor.Close();
            firstLoad();
        }

        private void saveSettings(string saveFile = null)
        {

            MemoryStream ms = new MemoryStream();
            XmlSerializer formatter = new XmlSerializer(dynamicVars.projectSettings.GetType());
            formatter.Serialize(ms, dynamicVars.projectSettings);
            ProjectData pd = new ProjectData();
            pd.projectSettings = ms.ToArray();
            ms.Close();
            ms = new MemoryStream();
            BinaryFormatter binFormatter = new BinaryFormatter();
            binFormatter.Serialize(ms, dynamicVars.wavOps.GetWaveFile.Data.ProcessedSamples);
            pd.projectData = ms.ToArray();
            ms.Close();
            if(saveFile == null)
                saveFile = dynamicVars.projectSettings.fileLocation.Split('.')[dynamicVars.projectSettings.fileLocation.Split('.').Length - 2] + ".N00bAudioProject";
            FileStream outFile = File.Open(saveFile, FileMode.OpenOrCreate);
            binFormatter = new BinaryFormatter();
            binFormatter.Serialize(outFile, pd);
            outFile.Close();
            lblSaved.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void loadSettings()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Abrir projeto";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Projectos N00bAudio |*.N00bAudioProject";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                FileStream inFile = File.Open(openFileDialog.FileName, FileMode.Open);
                BinaryFormatter binFormatter = new BinaryFormatter();
                ProjectData pd = new ProjectData();
                pd = (ProjectData)binFormatter.Deserialize(inFile);
                inFile.Close();
                MemoryStream ms = new MemoryStream(pd.projectSettings);
                XmlSerializer formatter = new XmlSerializer(dynamicVars.projectSettings.GetType());
                dynamicVars.projectSettings = (GlobalVars.DynamicVars.ProjectSettings)formatter.Deserialize(ms);
                ms.Close();
                ms = new MemoryStream(pd.projectData);
                binFormatter = new BinaryFormatter();
                if (File.Exists(dynamicVars.projectSettings.fileLocation))
                    loadFile(dynamicVars.projectSettings.fileLocation, dynamicVars.projectSettings.fileLocation.Split('\\')[dynamicVars.projectSettings.fileLocation.Split('\\').Length - 1], true);
                else
                {
                    MessageBox.Show("Ficheiro original não encontrado! Procure o ficheiro");
                    OpenFile(null, null);
                }
                dynamicVars.wavOps.GetWaveFile.Data.ProcessedSamples = (Int16[])binFormatter.Deserialize(ms);
                lblLastEdit.Text = dynamicVars.projectSettings.lastEdit;
                ms.Close();
            }
        }

        private void ExportAudio(string location)
        {
            FileStream file = new FileStream(location, FileMode.OpenOrCreate, FileAccess.Write);
            MemoryStream ms = new MemoryStream();
            ms = dynamicVars.wavOps.GetWaveFile.Write();
            ms.WriteTo(file);
            ms.Close();
            file.Close();
        }

        #region WindowControls
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
#endregion

        #region UI

        private void firstLoad()
        {
            saveToolStripMenuItem.Enabled = true;
            toolStripMenuItem1.Enabled = true;
            toolStripMenuItem3.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
            saveToolStripButton.Enabled = true;
            exportToolStripButton2.Enabled = true;
            viewMenu.Enabled = true;
            toolStripButton2.Enabled = true;
            toolStripButton1.Enabled = true;
            toolStripButton3.Enabled = true;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            loadSettings();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveSettings();
        }

        private void efeitosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dynamicVars.fxEditor = new FXEditor();
            dynamicVars.fxEditor.DynamicVars = dynamicVars;
            dynamicVars.fxEditor.MdiParent = this;
            dynamicVars.fxEditor.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void indexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Docs docs = new Docs();
            docs.MdiParent = this;
            docs.Show();
        }

        private void NoobAudio_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!OriginalPlaying)
            {
                spo = new SPlayer();
                spo.Play(dynamicVars.wavOps.GetWaveStream);
                toolStripButton1.Image = new Bitmap(Properties.Resources.Media_Controls_Stop_icon);
                OriginalPlaying = true;
            }
            else
            {
                spo.Stop();
                toolStripButton1.Image = new Bitmap(Properties.Resources.Media_Controls_Play_icon);
                OriginalPlaying = false;
            }                
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Exportar wave";
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            sfd.Filter = "N00bAudio Project |*.N00bAudioProject";
            sfd.FileName = dynamicVars.projectSettings.fileLocation.Split('\\')[dynamicVars.projectSettings.fileLocation.Split('\\').Length - 1].Split('.')[0] + "_edited";
            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                saveSettings(sfd.FileName);
            }
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Abrir ficheiro wave";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Ficheiros de Audio Wave (*.wav)|*.wav";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {

                WaveHeaderCheck waveHeaderCheck = new WaveHeaderCheck(openFileDialog.FileName,
                                                                        GlobalVars.ConstantVars.SupportedFormats.Wave.BitsPerSample,
                                                                        GlobalVars.ConstantVars.SupportedFormats.Wave.Channels,
                                                                        GlobalVars.ConstantVars.SupportedFormats.Wave.AudioFormat
                                                                      );
                if (waveHeaderCheck.ValidBits)
                    if (waveHeaderCheck.ValidChannels)
                        if (waveHeaderCheck.ValidAudioFormat)
                        {
                            loadFile(openFileDialog.FileName, openFileDialog.SafeFileName, false);
                        }
                        else
                            MessageBox.Show("Opps! Este software não suporta ficheiros wave com compressão\nPara mais informações leia a documentação\n",
                                "Formato de audio Inválido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else
                        MessageBox.Show("Opps! Este software não suporta ficheiros wave com mais de um canal\nPara mais informações leia a documentação", "Número de canais Inválido",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    MessageBox.Show("Opps! Este software só suporta ficheiros wave de 16 bits\nPara mais informações leia a documentação",
                        "Bits por Amostragem Inválidos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void audioProcessingOcurred()
        {
            string dt = DateTime.Now.ToString("HH:mm:ss");
            lblLastEdit.Text = dt;
            dynamicVars.projectSettings.lastEdit = dt;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string saveFile = dynamicVars.projectSettings.fileLocation.Split('.')[dynamicVars.projectSettings.fileLocation.Split('.').Length - 2] + "_edited.wav";
            ExportAudio(saveFile);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Exportar wave";
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            sfd.Filter = "Ficheiro de Audio Wave |*.wav";
            sfd.FileName = dynamicVars.projectSettings.fileLocation.Split('\\')[dynamicVars.projectSettings.fileLocation.Split('\\').Length - 1].Split('.')[0] + "_edited";
            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                ExportAudio(sfd.FileName);
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            saveSettings();
        }

        private void exportToolStripButton2_Click(object sender, EventArgs e)
        {
            string saveFile = dynamicVars.projectSettings.fileLocation.Split('.')[dynamicVars.projectSettings.fileLocation.Split('.').Length - 2] + "_edited.wav";
            ExportAudio(saveFile);
        }

        private void openProjectToolStripButton2_Click(object sender, EventArgs e)
        {
            loadSettings();
        }

        #endregion

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (childForm == null || childForm.IsDisposed)
            {
                childForm = new FileDetails();
                childForm.MdiParent = this;
                childForm.DynamicVars = dynamicVars;
                childForm.FileName = dynamicVars.projectSettings.fileLocation.Split('\\')[dynamicVars.projectSettings.fileLocation.Split('\\').Length - 1];
                childForm.Show();
            }
            else
                childForm.BringToFront();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dynamicVars.fxEditor == null || dynamicVars.fxEditor.IsDisposed)
            {
                dynamicVars.fxEditor = new FXEditor();
                dynamicVars.fxEditor.DynamicVars = dynamicVars;
                dynamicVars.fxEditor.MdiParent = this;
                dynamicVars.fxEditor.Show();
            }
            else
                dynamicVars.fxEditor.BringToFront();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (!OriginalPlaying)
            {
                MemoryStream ms = new MemoryStream();
                ms = dynamicVars.wavOps.GetWaveFile.Write();
                spm = new SPlayer();
                spm.Play(ms);
                toolStripButton3.Image = new Bitmap(Properties.Resources.Media_Controls_Stop_icon);
                OriginalPlaying = true;
            }
            else
            {
                spm.Stop();
                toolStripButton3.Image = new Bitmap(Properties.Resources.Media_Controls_Play_icon);
                OriginalPlaying = false;
            }
                
        }

        private void mediaPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Preview prv = new Preview();
            prv.DynamicVars = dynamicVars;
            prv.MdiParent = this;
            prv.Show();
        }

        private void cortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (trimForm == null || trimForm.IsDisposed)
            {
                trimForm = new Trimmer();
                trimForm.DynamicVars = dynamicVars;
                trimForm.MdiParent = this;
                trimForm.Show();
            }
            else
                trimForm.BringToFront();
        }

        private void reverseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Deseja inverter a reprodução de audio?", "Reverter audio", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                NoobAudioLib.FX.Reverse reverse = new NoobAudioLib.FX.Reverse(dynamicVars.wavOps.GetWaveFile);
                reverse.Invert();
                MessageBox.Show("Audio Invertido", "Processamento concluído",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
