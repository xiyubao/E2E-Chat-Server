using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace E2E_Server
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

    
        List<Tcp> tcp;
        List<Message> message;


        public void StartServer()
        {


            message = new List<Message>();
            tcp = new List<Tcp>(); //Server 与 每一个online user 保持一个 connect
            for(int i=0;i< Para.Max_Online_Num; ++i)
            {
                Tcp tmp = new Tcp();
                tmp.log += Log;
                tmp.light += Light;
                tmp.send += Send;
                tmp.startServer(Para.ip, Para.port);
                tcp.Add(tmp);
            }


        }

        public void Send(string id,string msg)
        {
            Message tmp = new Message(id, msg, Time());
            int index = Find(id);
            if (index != -1)
                tcp[index].Send(tmp);
            else
                Storage(tmp);
        }

        public int Find(string id)
        {
            int count = tcp.Count;
            for(int i=0;i<count;++i)
            {
                if (tcp[i].EqualsId(id))
                    return i;
            }
            return -1;
        }

        public void Storage(Message tmp)
        {
            message.Add(tmp);
        }

        private void Open_Server(object sender, RoutedEventArgs e)
        {
            StartServer();
        }

        private void Connect_Database(object sender, RoutedEventArgs e)
        {

        }

     
    }
}
