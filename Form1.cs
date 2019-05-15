using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



//NE PEUX PAS FERMER LES BOTS QUAND LES FENETRRES SONT INVISIBLES
//https://stackoverflow.com/questions/25961231/unhide-process-by-its-process-name

namespace DevHelper
{
    public partial class Form1 : Form
    {
        [DllImport("User32")]
        private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        [DllImport("User32.dll")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string strClassName, string strWindowName);

        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int ProcessId);

        private const int SW_RESTORE = 9;
        int botsCount = 0;
        int botsCount2 = 0;
        int botsCount3 = 0;

        public Form1()
        {
            InitializeComponent();
            listBox1.SetSelected(0, true);
            if (Properties.Settings.Default.botFile == "NULL")
            {
                textBox2.Text = "Please select a file";
            } else
            {
                textBox2.Text = Properties.Settings.Default.botFile;
            }

            if (Properties.Settings.Default.serverFile == "NULL")
            {
                textBox1.Text = "Please select a file";
            } else
            {
                textBox1.Text = Properties.Settings.Default.serverFile;
            }

            if (Properties.Settings.Default.unityFile == "NULL")
            {
                textBox3.Text = "Please select a file";
            } else
            {
                textBox3.Text = Properties.Settings.Default.unityFile;
            }

            if (Properties.Settings.Default.botFile2 == "NULL")
            {
                textBox6.Text = "Please select a file";
            }
            else
            {
                textBox6.Text = Properties.Settings.Default.botFile2;
            }

            if (Properties.Settings.Default.botFile3 == "NULL")
            {
                textBox7.Text = "Please select a file";
            }
            else
            {
                textBox7.Text = Properties.Settings.Default.botFile3;
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Please select a file")
            {
                MessageBox.Show("Aucun chemin valide spécifié", "Cette action ne peut être effectuée", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (Process proc in Process.GetProcesses())
            {
                if ((proc.MainWindowTitle == "SAMARITAIN --- Server "))
                {
                    MessageBox.Show("Serveur déjà ouvert veuillez le fermer avant d'en ouvrir un autre", "Cette action ne peut être effectuée",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }

            //Edit directory name
            string tempDir = Properties.Settings.Default.serverFile;
            int index = tempDir.LastIndexOf(@"\");
            if (index > 0)
                tempDir = tempDir.Substring(0, index+1);

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c TITLE SAMARITAIN --- Server && color 0c && dotnet " +"\""+ Properties.Settings.Default.serverFile + "\"" + " "+ listBox1.SelectedItem,
                    UseShellExecute = true,
                    WorkingDirectory = tempDir,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    CreateNoWindow = true
                    
                }
                

            };
            process.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "Please select a file")
            {
                MessageBox.Show("Aucun chemin valide spécifié", "Cette action ne peut être effectuée", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for (int i = 0; i < trackBar1.Value; i++)
            {
                botsCount++;
                button7.Text = "KILL BOT 1 (" + botsCount.ToString()+")";
                //Edit directory name
                string tempDir = Properties.Settings.Default.botFile;
                int index = tempDir.LastIndexOf(@"\");
                if (index > 0)
                    tempDir = tempDir.Substring(0, index + 1);

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = "/c TITLE SAMARITAIN --- BOT1 && color 74 && dotnet "+"\"" + Properties.Settings.Default.botFile + "\""+ " " + listBox1.SelectedItem,
                        UseShellExecute = true,
                        WorkingDirectory = tempDir,
                        RedirectStandardOutput = false,
                        RedirectStandardError = false,
                        CreateNoWindow = true

                    }


                };

                if (trackBar1.Value > 5)
                {
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                }

                if(checkBox1.Checked == true)
                {
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                }

                if((string)listBox1.SelectedItem == "Autre")
                {
                    process.StartInfo.Arguments = "/c TITLE SAMARITAIN --- Bot && color 74 && dotnet " + "\"" + Properties.Settings.Default.botFile + "\"" + " " + textBox5.Text;
                }

                process.Start();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "DLL Files (*.dll)|*dll|All Files (*.*)|*.*";


            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    System.IO.StreamReader sr = new
                    System.IO.StreamReader(openFileDialog1.FileName);
                    sr.Close();
                }
            }
            String filedata = openFileDialog1.FileName;

            openFileDialog1.Title = ("Choose a file");
            openFileDialog1.InitialDirectory = "\\";

            Properties.Settings.Default.serverFile = openFileDialog1.FileName;
            textBox1.Text = openFileDialog1.FileName;
            Properties.Settings.Default.Save();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "DLL Files (*.dll)|*dll|All Files (*.*)|*.*";


            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    System.IO.StreamReader sr = new
                    System.IO.StreamReader(openFileDialog1.FileName);
                    sr.Close();
                }
            }
            String filedata = openFileDialog1.FileName;

            openFileDialog1.Title = ("Choose a file");
            openFileDialog1.InitialDirectory = "\\";

            Properties.Settings.Default.botFile = openFileDialog1.FileName;
            textBox2.Text = openFileDialog1.FileName;
            Properties.Settings.Default.Save();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "EXE Files (*.exe)|*exe|All Files (*.*)|*.*";


            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    System.IO.StreamReader sr = new
                    System.IO.StreamReader(openFileDialog1.FileName);
                    sr.Close();
                }
            }
            String filedata = openFileDialog1.FileName;

            openFileDialog1.Title = ("Choose a file");
            openFileDialog1.InitialDirectory = "\\";

            Properties.Settings.Default.unityFile = openFileDialog1.FileName;
            textBox3.Text = openFileDialog1.FileName;
            Properties.Settings.Default.Save();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox4.Text = trackBar1.Value.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(closeAllBots);
            t.Start();
            
        }

        void closeAllBots()
        {
           for (int i = 0; i < 2; i++)
            {
                foreach (Process proc in Process.GetProcesses())
                {
                    if ((proc.MainWindowTitle == "SAMARITAIN --- BOT1 "))
                    {
                        proc.Kill();; 
                    }

                }
            }
            botsCount = 0;
            button7.Invoke(new MethodInvoker(delegate
            {
                button7.Text = "KILL BOT 1 (" + botsCount.ToString() + ")";
            }));

        }

        void closeAllBots2()
        {
            for (int i = 0; i < 2; i++)
            {
                foreach (Process proc in Process.GetProcesses())
                {
                    if ((proc.MainWindowTitle == "SAMARITAIN --- BOT2 "))
                    {
                        proc.Kill(); ;
                    }

                }
            }
            botsCount2 = 0;
            button11.Invoke(new MethodInvoker(delegate
            {
                button11.Text = "KILL BOT 2 (" + botsCount2.ToString() + ")";
            }));

        }

        void closeAllBots3()
        {
            for (int i = 0; i < 2; i++)
            {
                foreach (Process proc in Process.GetProcesses())
                {
                    if ((proc.MainWindowTitle == "SAMARITAIN --- BOT3 "))
                    {
                        proc.Kill(); ;
                    }

                }
            }
            botsCount3 = 0;
            button13.Invoke(new MethodInvoker(delegate
            {
                button13.Text = "KILL BOT 3 (" + botsCount3.ToString() + ")";
            }));

        }

        void closeServer()
        {
            for (int i = 0; i < 2; i++)
            {
                foreach (Process proc in Process.GetProcesses())
                {
                    if ((proc.MainWindowTitle == "SAMARITAIN --- Server "))
                    {
                        proc.Kill();
                    }

                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Thread t2 = new Thread(closeServer);
            t2.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "Please select a file")
            {
                MessageBox.Show("Aucun chemin valide spécifié", "Cette action ne peut être effectuée", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Edit directory name
            string tempDir = Properties.Settings.Default.unityFile;
            int index = tempDir.LastIndexOf(@"\");
            if (index > 0)
                tempDir = tempDir.Substring(0, index + 1);

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c start \"\" \"Battle IA.exe\" ",
                    UseShellExecute = true,
                    WorkingDirectory = tempDir,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    CreateNoWindow = true

                }


            };

            process.Start();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex == 2)
            {
                textBox5.Enabled = true;
            } else
            {
                textBox5.Enabled = false;
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            botsCount = 0;
            foreach (Process proc in Process.GetProcesses())
            {
                if ((proc.MainWindowTitle == "SAMARITAIN --- BOT1 "))
                {
                    botsCount++;
                }

            }
            botsCount2 = 0;
            foreach (Process proc in Process.GetProcesses())
            {
                if ((proc.MainWindowTitle == "SAMARITAIN --- BOT2 "))
                {
                    botsCount2++;
                }

            }
            botsCount3 = 0;
            foreach (Process proc in Process.GetProcesses())
            {
                if ((proc.MainWindowTitle == "SAMARITAIN --- BOT3 "))
                {
                    botsCount3++;
                }

            }

            button7.Invoke(new MethodInvoker(delegate
            {
                button7.Text = "KILL BOT 1 (" + botsCount.ToString() + ")";
            }));

            button11.Invoke(new MethodInvoker(delegate
            {
                button11.Text = "KILL BOT 2 (" + botsCount2.ToString() + ")";
            }));

            button13.Invoke(new MethodInvoker(delegate
            {
                button13.Text = "KILL BOT 3 (" + botsCount3.ToString() + ")";
            }));
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "DLL Files (*.dll)|*dll|All Files (*.*)|*.*";


            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    System.IO.StreamReader sr = new
                    System.IO.StreamReader(openFileDialog1.FileName);
                    sr.Close();
                }
            }
            String filedata = openFileDialog1.FileName;

            openFileDialog1.Title = ("Choose a file");
            openFileDialog1.InitialDirectory = "\\";

            Properties.Settings.Default.botFile2 = openFileDialog1.FileName;
            textBox6.Text = openFileDialog1.FileName;
            Properties.Settings.Default.Save();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "DLL Files (*.dll)|*dll|All Files (*.*)|*.*";


            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    System.IO.StreamReader sr = new
                    System.IO.StreamReader(openFileDialog1.FileName);
                    sr.Close();
                }
            }
            String filedata = openFileDialog1.FileName;

            openFileDialog1.Title = ("Choose a file");
            openFileDialog1.InitialDirectory = "\\";

            Properties.Settings.Default.botFile3 = openFileDialog1.FileName;
            textBox7.Text = openFileDialog1.FileName;
            Properties.Settings.Default.Save();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "Please select a file")
            {
                MessageBox.Show("Aucun chemin valide spécifié", "Cette action ne peut être effectuée", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for (int i = 0; i < trackBar1.Value; i++)
            {
                botsCount2++;
                button11.Text = "KILL BOT 1 (" + botsCount2.ToString() + ")";
                //Edit directory name
                string tempDir = Properties.Settings.Default.botFile2;
                int index = tempDir.LastIndexOf(@"\");
                if (index > 0)
                    tempDir = tempDir.Substring(0, index + 1);

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = "/c TITLE SAMARITAIN --- BOT2 && color 74 && dotnet " + "\"" + Properties.Settings.Default.botFile2 + "\"" + " " + listBox1.SelectedItem,
                        UseShellExecute = true,
                        WorkingDirectory = tempDir,
                        RedirectStandardOutput = false,
                        RedirectStandardError = false,
                        CreateNoWindow = true

                    }


                };

                if (trackBar1.Value > 5)
                {
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                }

                if (checkBox1.Checked == true)
                {
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                }

                if ((string)listBox1.SelectedItem == "Autre")
                {
                    process.StartInfo.Arguments = "/c TITLE SAMARITAIN --- BOT1 && color 74 && dotnet " + "\"" + Properties.Settings.Default.botFile + "\"" + " " + textBox5.Text;
                }

                process.Start();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == "Please select a file")
            {
                MessageBox.Show("Aucun chemin valide spécifié", "Cette action ne peut être effectuée", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for (int i = 0; i < trackBar1.Value; i++)
            {
                botsCount3++;
                button13.Text = "KILL BOT 1 (" + botsCount3.ToString() + ")";
                //Edit directory name
                string tempDir = Properties.Settings.Default.botFile3;
                int index = tempDir.LastIndexOf(@"\");
                if (index > 0)
                    tempDir = tempDir.Substring(0, index + 1);

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = "/c TITLE SAMARITAIN --- BOT3 && color 74 && dotnet " + "\"" + Properties.Settings.Default.botFile + "\"" + " " + listBox1.SelectedItem,
                        UseShellExecute = true,
                        WorkingDirectory = tempDir,
                        RedirectStandardOutput = false,
                        RedirectStandardError = false,
                        CreateNoWindow = true

                    }


                };

                if (trackBar1.Value > 5)
                {
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                }

                if (checkBox1.Checked == true)
                {
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                }

                if ((string)listBox1.SelectedItem == "Autre")
                {
                    process.StartInfo.Arguments = "/c TITLE SAMARITAIN --- BOT3 && color 74 && dotnet " + "\"" + Properties.Settings.Default.botFile + "\"" + " " + textBox5.Text;
                }

                process.Start();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Thread tBot2 = new Thread(closeAllBots2);
            tBot2.Start();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Thread tBot3 = new Thread(closeAllBots3);
            tBot3.Start();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:christophe.hugueny@isen.yncrea.fr");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string tempString;
            string tempDir = Properties.Settings.Default.unityFile;
            int index = tempDir.LastIndexOf(@"\");
            if (index > 0)
                tempDir = tempDir.Substring(0, index + 1);
            tempDir = tempDir + "settings.txt";

            if ((string)listBox1.SelectedItem == "Autre")
            {
                tempString = textBox5.Text;
                tempString.Replace("/bot", "/display");
                System.IO.File.WriteAllText(@tempDir, textBox5.Text);
            } else
            {
                tempString = (string)listBox1.SelectedItem;
                tempString.Replace("/bot", "/display");
                System.IO.File.WriteAllText(@tempDir, (string)listBox1.SelectedItem);
            }

            
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            button16.Text = "Set Server Delay : "+trackBar2.Value+" ms";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string tempDir = Properties.Settings.Default.serverFile;
            int index = tempDir.LastIndexOf(@"\");
            if (index > 0)
                tempDir = tempDir.Substring(0, index + 1);
            tempDir = tempDir + "settings.json";


            string json = File.ReadAllText(tempDir);
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            jsonObj["DelayBetweenEachBotTurn"] = trackBar2.Value;
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(tempDir, output);
        }
    }
}
