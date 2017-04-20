using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Invoker
{
    public partial class Form1 : Form
    {    
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int uFlags);

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        public Form1()
        {
            InitializeComponent();

            this.TopLevel = true;
            this.TopMost = true;
            this.Visible = true;               
            this.TransparencyKey = Color.AliceBlue;
            this.BackColor = Color.AliceBlue;

            int initialStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);

            Left = 276;
            Top = 742;            
        }

        private void setTopmost()
        {
            // SEE http://stackoverflow.com/a/34703664
            SetWindowPos(this.Handle, (int)HWND_TOPMOST, 0, 0, 0, 0, (int)TOPMOST_FLAGS);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // this was omitted originally but i found now the window wont even show without this
            setTopmost();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Show(object sender, EventArgs e)
        {
            while(true)
                setTopmost();                  
        }
    }
}
