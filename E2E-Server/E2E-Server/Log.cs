using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static E2E_Server.Para;

namespace E2E_Server
{
    public partial class MainWindow : Window
    {

        public void Log(string msg)
        {
            textBlock1.Dispatcher.Invoke(new Action(() => { textBlock1.Text += Time() + msg + "\n"; ; }));
            ScrollViewer1.Dispatcher.Invoke(new Action(() => { ScrollViewer1.ScrollToEnd(); }));
        }


        public void Light(bool judge, Light_Type light)
        {

        }

       public string Time()
        {
            return DateTime.Now.ToString();
        }
    }
}
