using Nhom14_KinhDoanhDienNuoc.Views;
using Quanlydiennuoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom14_KinhDoanhDienNuoc
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
            //Application.Run(new BanHangView());
            //Application.Run(new KhoHangView());
            //Application.Run(new BaoCaoBanHangView());
        }
    }
}
