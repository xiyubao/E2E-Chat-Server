using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static E2E_Server.Para;

namespace E2E_Server
{
    public class Message
    {
        public string message;
        public Message_Type mt;

        public string to_id;
        public string msg;
        public string time;

        public Message(string message)
        {
            this.message = message;
        }

        public Message(string to_id,string msg,string time)
        {
            this.to_id = to_id;
            this.msg = msg;
            this.time = time;
            this.mt = Message_Type.Send; 
        }
    }
}
