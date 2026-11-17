using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;

namespace FamilyGuard
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainForm());
        }
    }
}
