using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using Microsoft.Win32;
namespace Web
{
    public partial class Form1 : Form
    {
        public static int TotalBytes = 0;
        string strWindowsState;
        string strPath;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            strPath = "Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\ny3l4m\\Settings";
            RegistryKey regKeyAppRoot = Registry.CurrentUser.CreateSubKey(strPath);

            strWindowsState = (string)regKeyAppRoot.GetValue("WindowState");

            if (strPath != null)
             {

                 string url = "http://nyelam-com-application-desktop.rembon.co.id/v100";
                 if (WebRequestTest() == true)
                 {
                     toolStripProgressBar1.Visible = true;
                     toolStripLabel1.Visible = false;
                     TotalBytes = 0;
                     toolStripProgressBar1.Value = 0;

                     webBrowser1.IsWebBrowserContextMenuEnabled = false;
                     webBrowser1.AllowWebBrowserDrop = false;
                     webBrowser1.Url = new Uri(url);
                 }
                 else
                 {
                     Form1 frm = new Form1();
                     frm.Show();
                     this.Hide();
                     webBrowser1.Url = null;

                 }
             }
               

            else {

                FormNoInternetConnection frm = new FormNoInternetConnection();
                frm.Show();

            }
               

  

           
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

        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            if(isConnected())
                 {
                        TotalBytes++;
                    if (TotalBytes > 75)
                    {
                        TotalBytes = TotalBytes - 50;
                        toolStripLabel1.Visible = true;
                        toolStripLabel1.Text = "Wait";

                    }
                    toolStripProgressBar1.Value = TotalBytes;
                }
                else
                {
                    FormNoInternetConnection frm = new FormNoInternetConnection();
                    frm.Show();
                    this.Hide();
                    webBrowser1.Url = null;
                    webBrowser1.Stop();
                    return ;
                }

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //int TotalCount = 75 - toolStripProgressBar1.Value;
            int TotalCount = 100 - toolStripProgressBar1.Value;
            toolStripProgressBar1.Value += TotalCount;
            //toolStripProgressBar1.Visible = false;
            toolStripLabel1.Visible = true;
            toolStripLabel1.Text = "done";
        }

        public static bool isConnected()
    {
        try
        {
            string myAddress = "www.google.com";
            IPAddress[] addresslist = Dns.GetHostAddresses(myAddress);

            if (addresslist[0].ToString().Length > 6)
            {
                return true;
            }
            else
                return false;

        }
        catch
        {
            return false;
        }

    }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Exit Application ?",
                       "Nyelam.com",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
       }



}

