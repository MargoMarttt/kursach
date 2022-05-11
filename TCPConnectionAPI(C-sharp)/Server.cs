using ClassLibraryForTCPConnectionAPI_C_sharp_;
using DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TCPConnectionAPI_C_sharp_
{
    public class Server
    {
        protected IServerProtocol _protocol;

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
                    case CommandsToServer.PreviousRoom:
                        {
                            return 0;
                        }
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
            while (true)
            {
                user.DB_Id = Validation(ref user);
                if (user.DB_Id <= 0) { Console.WriteLine("Client disconnected"); user.ConnectionSocket.Close(); return; }
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

                        case CommandsToServer.PreviousRoom:
                            {
                                var disconnectingUser = _userViewAndCreatePermission.FindClientsWhere(c => c.Id == user.DB_Id).First();
                                disconnectingUser.IsOnline = false;
                                _userViewAndCreatePermission.UpdateClient(disconnectingUser);
                                return;
                            }
                        case CommandsToServer.FindDetailSupplierByName:
                            {
                                var param = _protocol.receiveString(user.ConnectionSocket);
                                _protocol.sendCollection(clientProtocol.FindDetailSuppliers(c => c.Name == param), user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.FindDetailByName:
                            {
                                var param = _protocol.receiveString(user.ConnectionSocket);
                                _protocol.sendCollection(clientProtocol.FindDetails(c => c.Name == param), user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.FindDetailByVendorCode:
                            {
                                var param = int.Parse(_protocol.receiveString(user.ConnectionSocket));
                                _protocol.sendCollection(clientProtocol.FindDetails(c => c.VendorCode == param), user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.FindDetailBySupplierName:
                            {
                                var param = _protocol.receiveString(user.ConnectionSocket);
                                _protocol.sendCollection(clientProtocol.FindDetails(c => c.DetailSupplier.Name == param), user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.GetAllDetails:
                            {
                                var param = _protocol.receiveString(user.ConnectionSocket);
                                _protocol.sendCollection(clientProtocol.FindDetails(c => c != null), user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.GetAllSuppliers:
                            {
                                var param = _protocol.receiveString(user.ConnectionSocket);
                                _protocol.sendCollection(clientProtocol.FindDetails(c => c != null), user.ConnectionSocket);
                                break;
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
                        case CommandsToServer.CreateDetailSupplier:
                            {
                                var res = adminProtocol.CreateNewDetailSupplier(_protocol.ReceiveObject<DetailSupplier>(user.ConnectionSocket));
                                if (res > 0) _protocol.sendAnswerFromServer(AnswerFromServer.Successfully, user.ConnectionSocket);
                                else _protocol.sendAnswerFromServer(AnswerFromServer.Error, user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.CreateDetail:
                            {
                                var obj = _protocol.ReceiveObject<Detail>(user.ConnectionSocket);
                                var supplierName = _protocol.receiveString(user.ConnectionSocket);
                                var res = adminProtocol.CreateNewDetail(obj, supplierName);
                                if (res > 0) _protocol.sendAnswerFromServer(AnswerFromServer.Successfully, user.ConnectionSocket);
                                else _protocol.sendAnswerFromServer(AnswerFromServer.Error, user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.UpdateDetailSupplier:
                            {
                                var obj = _protocol.ReceiveObject<DetailSupplier>(user.ConnectionSocket);
                                var res = adminProtocol.UpdateDetailSupplier(obj);
                                if (res) _protocol.sendAnswerFromServer(AnswerFromServer.Successfully, user.ConnectionSocket);
                                else _protocol.sendAnswerFromServer(AnswerFromServer.Error, user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.UpdateDetail:
                            {
                                var obj = _protocol.ReceiveObject<Detail>(user.ConnectionSocket);
                                var res = adminProtocol.UpdateDetail(obj);
                                if (res) _protocol.sendAnswerFromServer(AnswerFromServer.Successfully, user.ConnectionSocket);
                                else _protocol.sendAnswerFromServer(AnswerFromServer.Error, user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.DeleteDetailSupplier:
                            {
                                var id = int.Parse(_protocol.receiveString(user.ConnectionSocket));
                                var res = adminProtocol.DeleteDetailSuppliers(c=>c.Id == id);
                                if (res) _protocol.sendAnswerFromServer(AnswerFromServer.Successfully, user.ConnectionSocket);
                                else _protocol.sendAnswerFromServer(AnswerFromServer.Error, user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.DeleteDetail:
                            {
                                var id = int.Parse(_protocol.receiveString(user.ConnectionSocket));
                                var res = adminProtocol.DeleteDetails(c => c.Id == id);
                                if (res) _protocol.sendAnswerFromServer(AnswerFromServer.Successfully, user.ConnectionSocket);
                                else _protocol.sendAnswerFromServer(AnswerFromServer.Error, user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.FindDetailSupplierByName:
                            {
                                var param = _protocol.receiveString(user.ConnectionSocket);
                                _protocol.sendCollection(adminProtocol.FindDetailSuppliers(c => c.Name == param), user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.FindDetailSupplierByTotalRate:
                            {
                                var param = float.Parse(_protocol.receiveString(user.ConnectionSocket));
                                _protocol.sendCollection(adminProtocol.FindDetailSuppliers(c => c.TotalRate == param), user.ConnectionSocket);
                                break;
                            }

                        case CommandsToServer.FindDetailByName:
                            {
                                var param = _protocol.receiveString(user.ConnectionSocket);
                                _protocol.sendCollection(adminProtocol.FindDetails(c => c.Name == param), user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.FindDetailByVendorCode:
                            {
                                var param = int.Parse(_protocol.receiveString(user.ConnectionSocket));
                                _protocol.sendCollection(adminProtocol.FindDetails(c => c.VendorCode == param), user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.FindDetailBySupplierName:
                            {
                                var param = _protocol.receiveString(user.ConnectionSocket);
                                _protocol.sendCollection(adminProtocol.FindDetails(c => c.DetailSupplier.Name == param), user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.GetAllDetails:
                            {
                                var param = _protocol.receiveString(user.ConnectionSocket);
                                _protocol.sendCollection(adminProtocol.FindDetails(c => c != null), user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.GetAllSuppliers:
                            {
                                var param = _protocol.receiveString(user.ConnectionSocket);
                                _protocol.sendCollection(adminProtocol.FindDetails(c => c != null), user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.PreviousRoom:
                            {
                                var adm = _userViewAndCreatePermission.FindAdminsWhere(c => c.Id == user.DB_Id).First();
                                adm.IsOnline = false;
                                _userViewAndCreatePermission.UpdateAdmin(adm);
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
                        case CommandsToServer.FindDetailSupplierByName:
                            {
                                var param = _protocol.receiveString(user.ConnectionSocket);
                                _protocol.sendCollection(expertProtocol.FindDetailSuppliers(c => c.Name == param), user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.FindDetailByName:
                            {
                                var param = _protocol.receiveString(user.ConnectionSocket);
                                _protocol.sendCollection(expertProtocol.FindDetails(c => c.Name == param), user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.FindDetailByVendorCode:
                            {
                                var param = int.Parse(_protocol.receiveString(user.ConnectionSocket));
                                _protocol.sendCollection(expertProtocol.FindDetails(c => c.VendorCode == param), user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.FindDetailBySupplierName:
                            {
                                var param = _protocol.receiveString(user.ConnectionSocket);
                                _protocol.sendCollection(expertProtocol.FindDetails(c => c.DetailSupplier.Name == param), user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.GetAllDetails:
                            {
                                var param = _protocol.receiveString(user.ConnectionSocket);
                                _protocol.sendCollection(expertProtocol.FindDetails(c => c != null), user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.GetAllSuppliers:
                            {
                                var param = _protocol.receiveString(user.ConnectionSocket);
                                _protocol.sendCollection(expertProtocol.FindDetails(c => c != null), user.ConnectionSocket);
                                break;
                            }
                        case CommandsToServer.RateDetailSupplier:
                            {
                                var id = int.Parse(_protocol.receiveString(user.ConnectionSocket));
                                var rate = float.Parse(_protocol.receiveString(user.ConnectionSocket));
                                var obj = expertProtocol.FindDetailSuppliers(c => c.Id == id)[0];
                                var res = expertProtocol.Rate(obj, expert, rate);
                                break;
                            }
                        case CommandsToServer.PreviousRoom:
                            {
                                var client = _userViewAndCreatePermission.FindExpertsWhere(c => c.Id == user.DB_Id).First();
                                client.IsOnline = false;
                                _userViewAndCreatePermission.UpdateExpert(client);
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

        public void openConnection()
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
