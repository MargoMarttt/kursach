using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TCP_Common_Entities;

namespace Server
{
    public interface IServer
    {
        Socket acceptConnectionRequest();
        string receiveString(Socket from);
        void sendString(string str, Socket destination);
        string receiveLogin(Socket from);
        string receivePassword(Socket from);
        void sendCollection<T>(List<T> collection, Socket destination);
        List<T> receiveCollection<T>(Socket from);
        T ReceiveObject<T>(Socket from) where T : class;
        void SendObject<T>(T obj, Socket destination) where T : class;
        CommandsToServer receiveCommand(Socket from);
        void sendAnswerFromServer(AnswerFromServer answer, Socket desination);
        void sendTypeOfUser(TypeOfUser typeOfUser, Socket destination);
        TypeOfUser receiveTypeOfUser(Socket from);
        void closeConnection();
    }
}
