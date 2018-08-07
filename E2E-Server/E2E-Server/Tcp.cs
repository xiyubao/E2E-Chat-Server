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
    class Tcp
    {
        public IWebSocketConnection iwsc;

        private WebSocketServer server = null;

        private string id;

        public delegate void LogDelegate(string msg);
        public LogDelegate log;

        public delegate void LightDelegate(bool judge,Light_Type LT);
        public LightDelegate light;

        public delegate void SendDelefate(string to_id, string msg);
        public SendDelefate send;

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

                    iwsc.Send("server@login succssful");
               
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
            if (ss[1].Equals("login"))
            {
                this.id = ss[0];
            }
            else if(ss[1].Equals("exit"))
            {
                this.endServer();
            }
            else
            {
                string[] tt = ss[1].Split('#');
                if(tt[0].Equals("send"))
                {
                    string id_to = tt[1];
                    string msg = tt[2];


                    this.send(id_to, msg);
                }
            }
        }

        public bool EqualsId(string id)
        {
            return this.id.Equals(id);
        }
    }
}
