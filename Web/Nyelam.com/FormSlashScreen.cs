using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nyelam.com
{
    public partial class FormSlashScreen : Form
    {
        public FormSlashScreen()
        {
            InitializeComponent();
        }

        private void FormSlashScreen_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            //this.Width = BackgroundImage.Width;
            //this.Height = BackgroundImage.Height;
            this.Width = 300;
            this.Height = 300;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            this.Close();
            MainForm main = new MainForm();
            main.Show();
        }
    }
}
