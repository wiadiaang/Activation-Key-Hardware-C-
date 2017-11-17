using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Web
{
    public partial class FormNoInternetConnection : Form
    {
        public FormNoInternetConnection()
        {
            InitializeComponent();
        }

        private void FormNoInternetConnection_Load(object sender, EventArgs e)
        {
            try
            {
                timer1.Start();
                timer1.Interval = 2000;

            }
            catch { 
            
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (
                 ; ((WebRequestTest() == true)
                 == false);
                     )
            {
            }

            this.Close();
            this.Dispose();
            Form1 frm = new Form1();
            frm.Show();
           // Application.Restart();
        }

        public static bool WebRequestTest()
        {
            string url = "http://nyelam-com-application-desktop.rembon.co.id/v100";
            try
            {
                System.Net.WebRequest myRequest = System.Net.WebRequest.Create(url);
                System.Net.WebResponse myResponse = myRequest.GetResponse();
            }
            catch (System.Net.WebException)
            {
                return false;
            }
            return true;
        }
    }
}
