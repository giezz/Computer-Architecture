using System.Windows.Forms;
using WinApi;

namespace task2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SwitchSpaceToEnter()
        {
            if (IsSpaceOn())
            {
                SendKeys.Send("{ENTER}");
            }
        }

        private void SwitchLeftMouseClickToShift()
        {
            if (IsLeftMouseClickOn())
            {
                PinvokeDlls.keybd_event(0xA0, 0, 0, 0);
            }
            else
            {
                PinvokeDlls.keybd_event(0xA0, 0, 0xA2, 0);
            }
        }
        
        public static int IsKeyPressedEx(PinvokeEnums.VirtualKeyStates testKey)
        {
            short result = PinvokeDlls.GetKeyState(testKey);

            switch (result)
            {
                case -1: // Not pressed and not toggled on.
                    return -1;
                case 1: // Not pressed, but toggled on                    
                    return 1;
                default: // Pressed (and may be toggled on)                    
                    return (result & 128) == 128 ? 0 : -1;
            }
        }

        public static bool IsLeftMouseClickOn()
        {
            return ((PinvokeDlls.GetKeyState(PinvokeEnums.VirtualKeyStates.VK_LBUTTON) & 256) == 256);
        }

        public static bool IsNumLockOn()
        {
            return IsKeyPressedEx(PinvokeEnums.VirtualKeyStates.VK_NUMLOCK) == 1;
        }

        public static bool IsSpaceOn()
        {
            return ((PinvokeDlls.GetKeyState(PinvokeEnums.VirtualKeyStates.VK_SPACE) & 256) == 256);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == ' ' || e.KeyChar == ' ') && IsNumLockOn())
            {
                SwitchSpaceToEnter();
                e.Handled = true;
            }
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsNumLockOn() && (e.Button == MouseButtons.Left))
            {
                textBox1.Capture = false;
                SwitchLeftMouseClickToShift();
            }
        }

        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsNumLockOn() && (e.Button == MouseButtons.Left))
            {
                textBox1.Capture = false;
            }
        }

        private void textBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsNumLockOn() && (e.Button == MouseButtons.Left))
            {
                textBox1.Capture = true;
                SwitchLeftMouseClickToShift();
            }
        }
    }
}