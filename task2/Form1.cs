using System;
using System.Windows.Forms;
using WinApi;

namespace task2
{
    public partial class Form1 : Form
    {
        static short numLockKeyState = PinvokeDlls.GetKeyState(PinvokeEnums.VirtualKeyStates.VK_NUMLOCK);
        static short lbmKeyState = PinvokeDlls.GetKeyState(PinvokeEnums.VirtualKeyStates.VK_LBUTTON);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // KeysState();
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            KeysState();
        }

        private void KeysState()
        {
            numLockKeyState = PinvokeDlls.GetKeyState(PinvokeEnums.VirtualKeyStates.VK_NUMLOCK);
            lbmKeyState = PinvokeDlls.GetKeyState(PinvokeEnums.VirtualKeyStates.VK_LBUTTON);
            Console.WriteLine("NumLock: " + numLockKeyState);
            Console.WriteLine("LMB: " + lbmKeyState);
            if (numLockKeyState == 1)
            {
                if ((lbmKeyState & 0x80) != 0)
                {
                    Console.WriteLine(lbmKeyState);
                    // PinvokeDlls.keybd_event((byte) PinvokeEnums.VirtualKeyStates.VK_CAPITAL, 0, 0x0001 | 0x0002, 0);
                    // PinvokeDlls.keybd_event((byte) PinvokeEnums.VirtualKeyStates.VK_CAPITAL, 0, 0x0001, 0);
                    PinvokeDlls.keybd_event(0xA0, 0, 0, 0);
                }
                else
                {
                    // PinvokeDlls.keybd_event((byte) PinvokeEnums.VirtualKeyStates.VK_CAPITAL, 0, 0x0002, 0);
                    PinvokeDlls.keybd_event(0xA0, 0, 0xA2, 0);
                }
            }

            Console.WriteLine("----");
        }
    }
}