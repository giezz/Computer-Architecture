using System;
using System.Drawing;
using System.Windows.Forms;
using WinApi;

namespace task3
{
    public partial class Form1 : Form
    {
        private static IntPtr dc;

        public Form1()
        {
            InitializeComponent();
            dc = PinvokeDlls.GetDC(Handle);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PinvokeDlls.SetPixel(dc, e.X, e.Y, (uint) ColorToRGB(colorDialog1.Color));
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                colorDialog1.ShowDialog();
            }
        }

        private int ColorToRGB(Color crColor)
        {
            return crColor.B << 16 | crColor.G << 8 | crColor.R;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            PinvokeDlls.ReleaseDC(Handle, dc);
        }
    }
}