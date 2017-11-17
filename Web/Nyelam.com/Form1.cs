

using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Management; //Need to manually add to the References
using System.IO;
using System.Security.Cryptography;
using System.Drawing.Printing;
using System.Reflection;
using Microsoft.Win32;

namespace Nyelam.com
{
    public partial class Form1 : Form
    {
        string strWindowsState;
        string strPath;
        public Form1()
        {
            InitializeComponent();
        }

        private string getUniqueID(string drive)
        {
            if (drive == string.Empty)
            {
                //Find first drive
                foreach (DriveInfo compDrive in DriveInfo.GetDrives())
                {
                    if (compDrive.IsReady)
                    {
                        drive = compDrive.RootDirectory.ToString();
                        break;
                    }
                }
            }

            if (drive.EndsWith(":\\"))
            {
                //C:\ -> C
                drive = drive.Substring(0, drive.Length - 2);
            }

            string volumeSerial = getVolumeSerial(drive);
            string cpuID = getCPUID();

            //Mix them up and remove some useless 0's
            return cpuID.Substring(13) + cpuID.Substring(1, 4) + volumeSerial + cpuID.Substring(4, 4);
        }

        private string getVolumeSerial(string drive)
        {       
            ManagementObject disk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
            disk.Get();
            
            string volumeSerial = disk["VolumeSerialNumber"].ToString();
            disk.Dispose();

            return volumeSerial;
        }

        private string getCPUID()
        {
            string cpuInfo = "";
            ManagementClass managClass = new ManagementClass("win32_processor");
            ManagementObjectCollection managCollec = managClass.GetInstances();

            foreach (ManagementObject managObj in managCollec)
            {
                if (cpuInfo == "")
                {
                    //Get only the first CPU's ID
                    cpuInfo = managObj.Properties["processorID"].Value.ToString();
                    break;
                }
            }

            return cpuInfo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            var val = TxtActivation.Text.Replace("-", "");
            if (val == lblvalidasi.Text)
            {
                MessageBox.Show("Acktivate");
                strPath = "Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\ny3l4m\\Settings";
                strWindowsState = "";
                RegistryKey regKeyAppRoot = Registry.CurrentUser.CreateSubKey(strPath);

                if (WindowState == FormWindowState.Maximized)
                    strWindowsState = "Maximized";

                else if (WindowState == FormWindowState.Maximized)
                    strWindowsState = "Minimized";

                else
                    strWindowsState = val ;

                regKeyAppRoot.SetValue("WindowState", strWindowsState);

                this.Close();
                MainForm mf = new MainForm();
                mf.Show();

            }
            else {

                MessageBox.Show("Invalid","Warning!");
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.nyelam.com/");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtID.Text = getUniqueID("C");



            string code;
            code = MD5Hash(txtID.Text).ToUpper();
           // txtCode.Text = Regex.Replace(code, ".{4}", "$0-");
           // string result = Regex.Replace(txtID.Text, @"[^0-9]", "");

            lblvalidasi.Text = code;
            strPath = "Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\"+ code+"\\Settings";

            //Random rn = new Random();
            //string charsToUse = txtID.Text;

            //MatchEvaluator RandomChar = delegate(Match m)
            //{
            //    return charsToUse[rn.Next(charsToUse.Length)].ToString();
            //};

            //TxtActivation.Text = Regex.Replace("XXXX-XXXX-XXXX-XXXX-XXXX", "X", RandomChar);
            //label2.Text = Regex.Replace("XXXX", "X", RandomChar);
        
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
            Application.Exit();
        }


        //md5 code

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}