using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> exponentList = new List<int>();
            int[] message = new int[textBox1.Text.Length];
            int counter = 0;
            int gcd;
            foreach(char c in textBox1.Text)
            {
                message[counter] = c;
                counter++;
            }
            int n = int.Parse(textBox2.Text) * int.Parse(textBox3.Text);
            int fn = (int.Parse(textBox2.Text) - 1) * (int.Parse(textBox2.Text) - 1);
            for (int i = 0; i < fn; i++)
            {
                gcd = GreatestCommonDivisor(i, fn);
                if ( gcd == 1)
                {
                    exponentList.Add(gcd);
                }

            }

        }

        public int GreatestCommonDivisor(int a, int b)
        {
            int Remainder;

            while (b != 0)
            {
                Remainder = a % b;
                a = b;
                b = Remainder;
            }

            return a;
        }
    }
}
