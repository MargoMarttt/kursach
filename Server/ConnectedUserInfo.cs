using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TCP_Common_Entities;

namespace Server
{
    public class ConnectedUserInfo
    {
        public Socket ConnectionSocket { get; set; }
        public TypeOfUser Type { get; set; }
        public int DB_Id { get; set; }
        public ConnectedUserInfo()
        {
            Type = TypeOfUser.Undefined;
        }
    }
}
