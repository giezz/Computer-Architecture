using System;
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

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GetKeyState();
        }

        public static unsafe void GetKeyState()
        {
            INPUT[] input = new INPUT[1];
            input[0].type = PinvokeEnums.InputType.INPUT_KEYBOARD;
            input[0].U.ki.time = 0;
            input[0].U.ki.dwFlags = 0;
            input[0].U.ki.dwExtraInfo = PinvokeDlls.GetMessageExtraInfo();
            input[0].U.ki.wVk = PinvokeEnums.VirtualKeyShort.SPACE;
            PinvokeDlls.SendInput(2, input, sizeof(INPUT));
            short numLockKeyState = PinvokeDlls.GetKeyState(PinvokeEnums.VirtualKeyStates.VK_Q);
            Console.WriteLine(numLockKeyState);
            if ((numLockKeyState & 0x80) != 0)
            {
                
            }
        }
    }
}