using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using WinApi;

namespace task3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            Thread thread = new Thread(CheckMouseState);
            thread.Start();
            
        }

        private void CheckMouseState()
        {
            IntPtr activeWindowPtr = PinvokeDlls.GetActiveWindow();
            var dc = PinvokeDlls.GetDC(new IntPtr(0));
            POINT p;
            while (true)
            {
                short lmbKeySate = PinvokeDlls.GetKeyState(PinvokeEnums.VirtualKeyStates.VK_LBUTTON);
                PinvokeDlls.GetCursorPos(out p);
                if (lmbKeySate == -127 || lmbKeySate == -128)
                {
                    PinvokeDlls.SetPixel(dc, p.X, p.Y, 1686397);
                }
            }
        }

        private int ColorToRGB(Color crColor)
        {
            return (crColor.B << 16 | crColor.G << 8 | crColor.R);
        }
    }
}