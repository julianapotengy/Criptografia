using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace senhabolada
{
    public partial class Senhas : Form
    {
        string senha, senhacrip, senhatestar;
       
        public Senhas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                senha = textBox1.Text;
                senhacrip = GetMd5Hash(md5Hash, senha);
                textBox1.Clear();
            }
        }
        
        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            senhatestar = textBox2.Text;
            textBox2.Clear();

            using (MD5 md5Hash = MD5.Create())
            {
                if (VerifyMd5Hash(md5Hash, senhatestar, senhacrip))
                {
                    MessageBox.Show("As senhas coincidem :)", "Certo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("As senhas não coincidem :(", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            string hashOfInput = GetMd5Hash(md5Hash, input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


}
