using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Drawing.Printing;
using System.Reflection;

namespace HardwareID
{
    public partial class FormActivationID : Form
    {
        static Regex _rWord = new Regex(@"R\w*");
        public FormActivationID()
        {
            InitializeComponent();
        }

        private void btnGen_Click(object sender, EventArgs e)
        {


            string code;
            code = MD5Hash(TxtProduct.Text).ToUpper();
            txtCode.Text = Regex.Replace(code, ".{4}", "$0-");
          //  string result = Regex.Replace(txtCode.Text, @"[^0-9]", "");

            var val = txtCode.Text.Replace("-", "");

            textBox1.Text = val;




        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCode.Text = "";
            TxtProduct.Text = "";
        }

       

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
