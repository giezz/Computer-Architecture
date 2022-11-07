using System;
using System.Windows.Forms;
using WinApi;

namespace task3
{
    public partial class Form1 : Form
    {
        private static IntPtr handle;
        private static IntPtr dc;
        
        public Form1()
        {
            InitializeComponent();
            handle = Handle;
            dc = PinvokeDlls.GetDC(handle);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PinvokeDlls.SetPixel(dc, e.X, e.Y, 1686397);
                // Console.WriteLine("test mouse move event");
            }
        }
    }
}