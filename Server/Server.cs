using DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TCP_Common_Entities;

namespace Server
{
    public class Server
    {
        protected IServer _protocol;

        protected ICollection<ConnectedUserInfo> _connectedUsers;

        protected IUserModifyPermission _userViewAndCreatePermission;

        protected virtual int Registration(ref ConnectedUserInfo user)
        {
            user.Type = _protocol.receiveTypeOfUser(user.ConnectionSocket);
            string login = _protocol.receiveLogin(user.ConnectionSocket);
            string password = _protocol.receivePassword(user.ConnectionSocket);
            float rateWeight = 0;
            if (user.Type == TypeOfUser.Expert)
            {
                rateWeight = float.Parse(_protocol.receiveString(user.ConnectionSocket));
            }
            switch (user.Type)
            {
                case TypeOfUser.Admin:
                    return _userViewAndCreatePermission.CreateAdmin(new Admin(login, password));
                case TypeOfUser.Client:
                    return _userViewAndCreatePermission.CreateClient(new Client(login, password));
                case TypeOfUser.Expert:
                    return _userViewAndCreatePermission.CreateExpert(new Expert(login, password, rateWeight));
                case TypeOfUser.Undefined:
                    return 0;
                default:
                    return 0;
            }
        }

        protected virtual int Authorization(ref ConnectedUserInfo user)
        {
            string login = _protocol.receiveLogin(user.ConnectionSocket);
            string password = _protocol.receivePassword(user.ConnectionSocket);

            var admins = _userViewAndCreatePermission.FindAdminsWhere(c => c.Login == login);
            if (admins.Count == 0) { user.Type = TypeOfUser.Undefined; }
            else
            {
                var admin = admins.First();
                if (admin.Id > 0 && admin.Password == password && admin.UserStatus == Status.NotBanned)
                { user.Type = TypeOfUser.Admin; return admin.Id; }
            }

            var clients = _userViewAndCreatePermission.FindClientsWhere(c => c.Login == login);
            if (clients.Count == 0) { user.Type = TypeOfUser.Undefined; }
            else
            {
                var client = clients.First();
                if (client.Id > 0 && client.Password == password && client.UserStatus == Status.NotBanned)
                { user.Type = TypeOfUser.Client; return client.Id; }
            }


            var experts = _userViewAndCreatePermission.FindExpertsWhere(c => c.Login == login);
            if (experts.Count == 0) { user.Type = TypeOfUser.Undefined; }
            else
            {
                var expert = experts.First();
                if (expert.Id > 0 && expert.Password == password && expert.UserStatus == Status.NotBanned)
                { user.Type = TypeOfUser.Expert; return expert.Id; }
                user.Type = TypeOfUser.Undefined;
                return 0;
            }
            user.Type = TypeOfUser.Undefined;
            return 0;
        }

        protected virtual int Validation(ref ConnectedUserInfo user)
        {
            while (true)
            {
                switch (_protocol.receiveCommand(user.ConnectionSocket))
                {
                    case CommandsToServer.Registration:
                        {
                            int id = Registration(ref user);
                            if (id > 0)
                            {
                                _protocol.sendAnswerFromServer(AnswerFromServer.Successfully, user.ConnectionSocket);
                                return id;
                            }
                            else
                            {
                                _protocol.sendAnswerFromServer(AnswerFromServer.Error, user.ConnectionSocket);
                                continue;
                            }
                        }
                    case CommandsToServer.Authorization:
                        {
                            int id = Authorization(ref user);
                            _protocol.sendTypeOfUser(user.Type, user.ConnectionSocket);
                            return id;
                        }
                    case CommandsToServer.Disconnect:
                        return 0;
                    default:
                        {
                            _protocol.sendAnswerFromServer(AnswerFromServer.UnknownCommand, user.ConnectionSocket);
                            return 0;
                        }
                }
            }
        }

        protected virtual void UserHandler(object client)
        {
            ConnectedUserInfo user = client as ConnectedUserInfo;
            user.DB_Id = Validation(ref user);
            if (user.DB_Id <= 0) { user.ConnectionSocket.Close(); return; }
            switch (user.Type)
            {
                case TypeOfUser.Admin:
                    { AdminHandler(user); break; }
                case TypeOfUser.Client:
                    { ClientHandler(user); break; }
                case TypeOfUser.Expert:
                    { ExpertHandler(user); break; }
                default:
                    break;
            }
        }

        protected virtual void ClientHandler(ConnectedUserInfo user)
        {
            var client = _userViewAndCreatePermission.FindClientsWhere(c => c.Id == user.DB_Id).First();
            client.IsOnline = true;
            _userViewAndCreatePermission.UpdateClient(client);
            using (IClientAbilityProtocol clientProtocol = new ClientAbilityProtocol())
            {
                while (true)
                {
                    switch (_protocol.receiveCommand(user.ConnectionSocket))
                    {
                        
                        case CommandsToServer.Disconnect:
                            {
                                var disconnectingUser = _userViewAndCreatePermission.FindClientsWhere(c => c.Id == user.DB_Id).First();
                                disconnectingUser.IsOnline = false;
                                _userViewAndCreatePermission.UpdateClient(disconnectingUser);
                                user.ConnectionSocket.Close();
                                return;
                            }
                        default:
                            {
                                _protocol.sendAnswerFromServer(AnswerFromServer.UnknownCommand, user.ConnectionSocket);
                                break;
                            }
                    }
                }
            }
        }

        protected virtual void AdminHandler(ConnectedUserInfo user)
        {
            var admin = _userViewAndCreatePermission.FindAdminsWhere(c => c.Id == user.DB_Id).First();
            admin.IsOnline = true;
            _userViewAndCreatePermission.UpdateAdmin(admin);
            using (IAdminAbilityProtocol adminProtocol = new AdminAbilityProtocol())
            {
                while (true)
                {
                    switch (_protocol.receiveCommand(user.ConnectionSocket))
                    {
                       
                        case CommandsToServer.GetAllClients:
                            {
                                _protocol.sendCollection(adminProtocol.FindClientsWhere(c => c != null), user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.GetAllExperts:
                            {
                                _protocol.sendCollection(adminProtocol.FindExpertsWhere(c => c != null), user.ConnectionSocket);

                                break;
                            }
                        case CommandsToServer.RegisterNewUser:
                            {
                                var userType = _protocol.receiveTypeOfUser(user.ConnectionSocket);
                                var login = _protocol.receiveLogin(user.ConnectionSocket);
                                var password = _protocol.receivePassword(user.ConnectionSocket);
                                float rateWeight = 0.0F;
                                if (userType == TypeOfUser.Expert) { rateWeight = float.Parse(_protocol.receiveString(user.ConnectionSocket)); };
                                var res = adminProtocol.RegisterNewUser(userType, login, password, rateWeight);
                                if (res > 0) _protocol.sendAnswerFromServer(AnswerFromServer.Successfully, user.ConnectionSocket);
                                else _protocol.sendAnswerFromServer(AnswerFromServer.Error, user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.BanClient:
                            {
                                var login = _protocol.receiveLogin(user.ConnectionSocket);
                                var res = adminProtocol.BanClientsWhere(a => a.Login == login);
                                if (res) _protocol.sendAnswerFromServer(AnswerFromServer.Successfully, user.ConnectionSocket);
                                else _protocol.sendAnswerFromServer(AnswerFromServer.Error, user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.BanExpert:
                            {
                                var login = _protocol.receiveLogin(user.ConnectionSocket);
                                var res = adminProtocol.BanExpertsWhere(a => a.Login == login);
                                if (res) _protocol.sendAnswerFromServer(AnswerFromServer.Successfully, user.ConnectionSocket);
                                else _protocol.sendAnswerFromServer(AnswerFromServer.Error, user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.UnbanClient:
                            {
                                var login = _protocol.receiveLogin(user.ConnectionSocket);
                                var res = adminProtocol.UnbanClientsWhere(a => a.Login == login);
                                if (res) _protocol.sendAnswerFromServer(AnswerFromServer.Successfully, user.ConnectionSocket);
                                else _protocol.sendAnswerFromServer(AnswerFromServer.Error, user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.UnbanExpert:
                            {
                                var login = _protocol.receiveLogin(user.ConnectionSocket);
                                var res = adminProtocol.UnbanExpertsWhere(a => a.Login == login);
                                if (res) _protocol.sendAnswerFromServer(AnswerFromServer.Successfully, user.ConnectionSocket);
                                else _protocol.sendAnswerFromServer(AnswerFromServer.Error, user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.DeleteClient:
                            {
                                var login = _protocol.receiveLogin(user.ConnectionSocket);
                                var res = adminProtocol.DeleteClientsWhere(a => a.Login == login);
                                if (res) _protocol.sendAnswerFromServer(AnswerFromServer.Successfully, user.ConnectionSocket);
                                else _protocol.sendAnswerFromServer(AnswerFromServer.Error, user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.DeleteExpert:
                            {
                                var login = _protocol.receiveLogin(user.ConnectionSocket);
                                var res = adminProtocol.DeleteExpertsWhere(a => a.Login == login);
                                if (res) _protocol.sendAnswerFromServer(AnswerFromServer.Successfully, user.ConnectionSocket);
                                else _protocol.sendAnswerFromServer(AnswerFromServer.Error, user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.CreateReportAboutVehicles:
                            {
                                _protocol.sendString(adminProtocol.CreateReportAboutVehicles(), user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.FindAdminByLogin:
                            {
                                var login = _protocol.receiveLogin(user.ConnectionSocket);
                                _protocol.sendCollection(adminProtocol.FindAdminsWhere(c => c.Login == login), user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.FindClientByLogin:
                            {
                                var login = _protocol.receiveLogin(user.ConnectionSocket);
                                _protocol.sendCollection(adminProtocol.FindClientsWhere(c => c.Login == login), user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.FindExpertByLogin:
                            {
                                var login = _protocol.receiveLogin(user.ConnectionSocket);
                                _protocol.sendCollection(adminProtocol.FindExpertsWhere(c => c.Login == login), user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.Disconnect:
                            {
                                var adm = _userViewAndCreatePermission.FindAdminsWhere(c => c.Id == user.DB_Id).First();
                                adm.IsOnline = false;
                                _userViewAndCreatePermission.UpdateAdmin(adm);
                                user.ConnectionSocket.Close();
                                return;
                            }
                        default:
                            {
                                _protocol.sendAnswerFromServer(AnswerFromServer.UnknownCommand, user.ConnectionSocket);
                                break;
                            }
                    }
                }
            }
        }

        protected virtual void ExpertHandler(ConnectedUserInfo user)
        {
            var expert = _userViewAndCreatePermission.FindExpertsWhere(c => c.Id == user.DB_Id).First();
            expert.IsOnline = true;
            _userViewAndCreatePermission.UpdateExpert(expert);
            using (IExpertAbilityProtocol expertProtocol = new ExpertAbilityProtocol())
            {
                while (true)
                {
                    switch (_protocol.receiveCommand(user.ConnectionSocket))
                    {
                       
                        case CommandsToServer.Disconnect:
                            {
                                var client = _userViewAndCreatePermission.FindExpertsWhere(c => c.Id == user.DB_Id).First();
                                client.IsOnline = false;
                                _userViewAndCreatePermission.UpdateExpert(client);
                                user.ConnectionSocket.Close();
                                return;
                            }
                        
                        default:
                            {
                                _protocol.sendAnswerFromServer(AnswerFromServer.UnknownCommand, user.ConnectionSocket);
                                break;
                            }
                    }
                }
            }
        }

        public Server()
        {
            _protocol = new TCPServerProtocol();
            _connectedUsers = new List<ConnectedUserInfo>();
            _userViewAndCreatePermission = new DatabaseContext();
        }

        public void AcceptConnectionRequests()
        {
            while (true)
            {
                ConnectedUserInfo connectedUserInfo = new ConnectedUserInfo();
                connectedUserInfo.ConnectionSocket = _protocol.acceptConnectionRequest();
                Console.WriteLine("Подключился новый пользователь!");
                ThreadPool.QueueUserWorkItem(UserHandler, connectedUserInfo);
            }
        }
    }
}
