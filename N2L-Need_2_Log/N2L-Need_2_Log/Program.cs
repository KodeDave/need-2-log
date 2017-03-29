using System;
using System.Windows.Forms;

namespace N2L_Need_2_Log
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            core.Controller.CheckSettings();
            Application.Run(new gui.FormMainMenu());
        }
    }
}
