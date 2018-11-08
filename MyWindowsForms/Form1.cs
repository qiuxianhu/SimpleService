using MyWindowsServiceHost;
using System;
using System.Windows.Forms;

namespace MyWindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
            this.btnServiceStart.Click += new EventHandler(btnServiceStart_Click);
            this.btnServiceStop.Click += new EventHandler(btnServiceStop_Click);
            this.btnTestService.Click += btnTestService_Click;
        }
        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.btnServiceStop_Click(null, null);
        }

        void Form1_Load(object sender, EventArgs e)
        {
            this.btnServiceStart.Enabled = this.btnServiceStop.Enabled = true;
        }

        private void btnServiceStart_Click(object sender, EventArgs e)
        {
            Global.Start();
            this.btnServiceStart.Enabled = false;
            this.btnServiceStop.Enabled = true;
        }

        private void btnServiceStop_Click(object sender, EventArgs e)
        {
            Global.Stop();
            this.btnServiceStart.Enabled = true;
            this.btnServiceStop.Enabled = false;
        }

        private void btnTestService_Click(object sender, EventArgs e)
        {
            this.btnTestService.Enabled = false;
            MessageBox.Show("ok");
            this.btnTestService.Enabled = true;
        }
    }
}
