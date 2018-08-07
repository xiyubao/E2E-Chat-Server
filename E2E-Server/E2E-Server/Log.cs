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

        public String Line()
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
            return st.GetFrame(0).GetFileLineNumber().ToString();
        }

        public void Function_Binding(Tcp tmp)
        {
            tmp.log += Log;
            tmp.light += Light;
            tmp.send += Send;
            tmp.login += DatabaseLogin;
            tmp.register += DatabaseRegister;
        }
    }
}
