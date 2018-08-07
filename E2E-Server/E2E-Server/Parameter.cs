using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace E2E_Server
{
    public class Para
    {
        public static string ip = "192.168.2.171";
        public static int port = 8888;

        public static int Max_Online_Num = 5;


        public enum Light_Type { Open,Data, Send };
        public enum Message_Type { Send };
        public enum DeviceType { Server,Android,Web,Windows};
        public enum OperateType { login,register,exit,send,login_success,login_fail};
    }
}
