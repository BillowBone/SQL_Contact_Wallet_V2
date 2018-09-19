using System;
using System.Windows.Forms;

namespace SQL_Contact_Wallet_V2
{
    //Entry point of the program
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Wallet());
        }
    }
}
