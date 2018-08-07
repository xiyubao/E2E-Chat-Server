using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static E2E_Server.Para;

namespace E2E_Server
{
    public class Tcp
    {
        public IWebSocketConnection iwsc;

        private WebSocketServer server = null;

        private string id;
        private bool online;
        private Para.DeviceType dt;

        public delegate void LogDelegate(string msg);
        public LogDelegate log;

        public delegate void LightDelegate(bool judge,Light_Type LT);
        public LightDelegate light;

        public delegate void SendDelefate(string to_id, string msg);
        public SendDelefate send;

        public delegate bool LoginDelefate(string username, string password);
        public LoginDelefate login;

        public delegate bool RegisterDelefate(string username, string password);
        public RegisterDelefate register;

        public void startServer(string ip,int port)
        {
            server = new WebSocketServer("ws://" + ip + ":" + port + "");
            server.Start(socket =>
            {
                iwsc = socket;
                socket.OnOpen = () =>
                {
                    this.log(socket.ConnectionInfo.ClientIpAddress + " is connecting");
                    this.light(true, Light_Type.Open);

                    iwsc.Send(Para.DeviceType.Server+"@"+Para.OperateType.login_success);
               
                };

                socket.OnClose = () =>
                {
                    this.log(socket.ConnectionInfo.ClientIpAddress + " has been disconnected");
                    this.light(false, Light_Type.Open);
                };
                socket.OnMessage = message =>
                {
                    this.log("get \"" + message + "\"" + " from " + socket.ConnectionInfo.ClientIpAddress);
                    this.light(true, Light_Type.Send);

                    Analyst(message);
                };

            });
            Console.ReadLine();

        }
        
        public void endServer()
        {
            server.Dispose();
            server = null;
        }
        /*
         * user@login
         * user@send#to#msg
         * user@exit
         * 
         * 
         * 
         * 
         */
        public void Send(Message msg)
        {
            this.iwsc.Send(msg.msg);
        }

        public void Analyst(string message)
        {
            string[] ss = message.Split('@');

            if (ss[0].Equals(Para.DeviceType.Web.ToString()))
            {
                string[] tt = ss[1].Split('#');
                string type = tt[0];
                string username = tt[1];
                string password = tt[2];
                bool result;
                if (type.Equals(Para.OperateType.login.ToString()))
                    result = login(username, password);
                else
                    result = register(username, password);

                iwsc.Send(result.ToString());
            }
            else 
            {
                if (ss[0].Equals(Para.DeviceType.Android.ToString()))
                    dt = Para.DeviceType.Android;
                if (ss[0].Equals(Para.DeviceType.Windows.ToString()))
                    dt = Para.DeviceType.Windows;

                if (ss[1].Equals(Para.OperateType.exit.ToString()))
                {
                    this.endServer();
                }
                else
                {
                    string[] tt = ss[1].Split('#');
                    if (tt[0].Equals(Para.OperateType.send.ToString()))
                    {
                        string id_to = tt[1];
                        string msg = tt[2];


                        this.send(id_to, msg);
                    }
                    else if(tt[0].Equals(Para.OperateType.login.ToString()))
                    {
                        string username = tt[1];
                        string password = tt[2];
                        if (login(username, password))
                            this.id = username;
                        else
                            iwsc.Send(Para.DeviceType.Server+"@"+ Para.OperateType.login_fail);
                    }
                }
            }
        }

        public bool EqualsId(string id)
        {
            return this.id.Equals(id);
        }
    }
}
