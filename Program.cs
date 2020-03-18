using System;
using System.Windows.Forms;
using ajTextFinder.app;

namespace ajTextFinder {
    static class Program {
        //================================================================================
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var frm = CajApp.start();
            if (frm != null) {
                Application.Run(frm);
            }
        }
        //================================================================================
    }
}
