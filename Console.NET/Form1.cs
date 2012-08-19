using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime;
using System.Diagnostics;
namespace Console.NET
{
    public partial class Form1 : Form
    {
        public delegate void DataReceived(String data);
        public Form1()
        {
            InitializeComponent();
            p = new Process();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String app = textBox1.Text.Split(' ')[0];
            String command = textBox1.Text.Split(' ').Length > 1 ? textBox1.Text.Split(new char[]{' '}, 2)[1] : "";

           string cmdexePath = @"C:\Windows\System32\cmd.exe";
            //notice the quotes around the below string...
            string myApplication = command;
            //the /K keeps the CMD window open - even if your windows app closes
            //string cmdArguments = String.Format("/K {0} {1}", app, command);
            ProcessStartInfo psi = new ProcessStartInfo(cmdexePath);
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardError = true;
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
           
            
            p.StartInfo = psi;
            p.Start();
            try
            {
                p.BeginOutputReadLine();
            }
            catch (Exception ex)
            {
            }
        }
        Process p;
        private void button1_Click(object sender, EventArgs e)
        {
            p.StandardInput.WriteLine(textBox1.Text);
         
           
        }
        void DataReceivedMethod(String data)
        {
            richTextBox1.HideSelection = false;

            richTextBox1.AppendText( data + "\n");
            
        }
        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            richTextBox1.Invoke(new Form1.DataReceived(DataReceivedMethod),  e.Data);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = checkBox1.Checked;
        }
    }
}
