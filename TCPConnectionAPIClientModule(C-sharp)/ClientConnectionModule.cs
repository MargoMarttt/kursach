using ClassLibraryForTCPConnectionAPI_C_sharp_;
using DatabaseEntities;
using System.Collections.Generic;
using System.Linq;

namespace TCPConnectionAPIClientModule_C_sharp_
{
    public class ClientConnectionModule : IAdminAccess, IExpertAccess, IClientAccess
    {
        protected IUserProtocol protocol;

        protected static int amountOfObjects;

        public bool Connected { get; }

        public ClientConnectionModule()
        {
            protocol = new TCPClientProtocol();
            if (amountOfObjects == 0)
            {
                Connected = protocol.connect();
            }
            amountOfObjects++;
        }

        public TypeOfUser Authorization(string login, string password)
        {
            protocol.sendCommand(CommandsToServer.Authorization);
            protocol.sendLogin(login);
            protocol.sendPassword(password);
            return protocol.receiveTypeOfUser();
        }

        public AnswerFromServer Registration(TypeOfUser type, string login, string password, float expertWeight = 0)
        {
            protocol.sendCommand(CommandsToServer.Registration);
            protocol.sendTypeOfUser(type);
            protocol.sendLogin(login);
            protocol.sendPassword(password);
            if (type == TypeOfUser.Expert)
            {
                protocol.sendString(expertWeight.ToString());
            }
            return protocol.receiveAnswerFromServer();
        }

        public AnswerFromServer Rate(int entityId, float Rate)
        {
            protocol.sendCommand(CommandsToServer.RateDetailSupplier);
            protocol.sendString(entityId.ToString());
            protocol.sendString(Rate.ToString());
            return protocol.receiveAnswerFromServer();
        }

        public void PreviousRoom()
        {
            protocol.GoToPreviousRoom();
        }

        public Client FindClientByLogin(string login)
        {
            protocol.sendCommand(CommandsToServer.FindClientByLogin);
            protocol.sendLogin(login);
            var received = protocol.receiveCollection<Client>();
            if (received.Count == 0 || received.Count > 1)
            {
                return new Client();
            }
            else
            {
                return received.First();
            }
        }

        public Expert FindExpertByLogin(string login)
        {
            protocol.sendCommand(CommandsToServer.FindExpertByLogin);
            protocol.sendLogin(login);
            var received = protocol.receiveCollection<Expert>();
            if (received.Count == 0 || received.Count > 1)
            {
                return new Expert();
            }
            else
            {
                return received.First();
            }
        }

        public Admin FindAdminByLogin(string login)
        {
            protocol.sendCommand(CommandsToServer.FindAdminByLogin);
            protocol.sendLogin(login);
            var received = protocol.receiveCollection<Admin>();
            if (received.Count == 0 || received.Count > 1)
            {
                return new Admin();
            }
            else
            {
                return received.First();
            }
        }

        public AnswerFromServer RegisterNewAdmin(string login, string password)
        {
            protocol.sendCommand(CommandsToServer.RegisterNewUser);
            protocol.sendTypeOfUser(TypeOfUser.Admin);
            protocol.sendLogin(login);
            protocol.sendPassword(password);
            return protocol.receiveAnswerFromServer();
        }

        public AnswerFromServer RegisterNewClient(string login, string password)
        {
            protocol.sendCommand(CommandsToServer.RegisterNewUser);
            protocol.sendTypeOfUser(TypeOfUser.Client);
            protocol.sendLogin(login);
            protocol.sendPassword(password);
            return protocol.receiveAnswerFromServer();
        }

        public AnswerFromServer RegisterNewExpert(string login, string password, float rateWeight)
        {
            protocol.sendCommand(CommandsToServer.RegisterNewUser);
            protocol.sendTypeOfUser(TypeOfUser.Expert);
            protocol.sendLogin(login);
            protocol.sendPassword(password);
            protocol.sendString(rateWeight.ToString());
            return protocol.receiveAnswerFromServer();
        }

        public AnswerFromServer BanClientWith(string login)
        {
            protocol.sendCommand(CommandsToServer.BanClient);
            protocol.sendLogin(login);
            return protocol.receiveAnswerFromServer();
        }

        public AnswerFromServer BanExpertWith(string login)
        {
            protocol.sendCommand(CommandsToServer.BanExpert);
            protocol.sendLogin(login);
            return protocol.receiveAnswerFromServer();
        }

        public AnswerFromServer UnbanExpertWith(string login)
        {
            protocol.sendCommand(CommandsToServer.UnbanExpert);
            protocol.sendLogin(login);
            return protocol.receiveAnswerFromServer();
        }

        public AnswerFromServer UnbanClientWith(string login)
        {
            protocol.sendCommand(CommandsToServer.UnbanExpert);
            protocol.sendLogin(login);
            return protocol.receiveAnswerFromServer();
        }

        public AnswerFromServer DeleteExpertWith(string login)
        {
            protocol.sendCommand(CommandsToServer.DeleteExpert);
            protocol.sendLogin(login);
            return protocol.receiveAnswerFromServer();
        }

        public AnswerFromServer DeleteClientWith(string login)
        {
            protocol.sendCommand(CommandsToServer.DeleteClient);
            protocol.sendLogin(login);
            return protocol.receiveAnswerFromServer();
        }

        public string GetReportAboutDetailSuppliers()
        {
            //protocol.sendCommand(CommandsToServer.CreateReportAboutVehicles);
            return protocol.receiveString();
        }

        public List<Client> GetAllClients()
        {
            protocol.sendCommand(CommandsToServer.GetAllClients);
            return protocol.receiveCollection<Client>();
        }

        public List<Expert> GetAllExperts()
        {
            protocol.sendCommand(CommandsToServer.GetAllExperts);
            return protocol.receiveCollection<Expert>();
        }

        public AnswerFromServer CreateNewDetailSupplier(DetailSupplier obj)
        {
            protocol.sendCommand(CommandsToServer.CreateDetailSupplier);
            protocol.SendObject(obj);
            return protocol.receiveAnswerFromServer();
        }

        public AnswerFromServer CreateNewDetail(Detail obj, string supplierName)
        {
            protocol.sendCommand(CommandsToServer.CreateDetail);
            protocol.SendObject(obj);
            protocol.sendString(supplierName);
            return protocol.receiveAnswerFromServer();
        }

        public AnswerFromServer UpdateDetail(Detail newVersion)
        {
            protocol.sendCommand(CommandsToServer.UpdateDetail);
            protocol.SendObject(newVersion);
            return protocol.receiveAnswerFromServer();
        }

        public AnswerFromServer UpdateDetailSupplier(DetailSupplier newVersion)
        {
            protocol.sendCommand(CommandsToServer.UpdateDetailSupplier);
            protocol.SendObject(newVersion);
            return protocol.receiveAnswerFromServer();
        }

        public AnswerFromServer DeleteDetail(int id)
        {
            protocol.sendCommand(CommandsToServer.DeleteDetail);
            protocol.sendString(id.ToString());
            return protocol.receiveAnswerFromServer();
        }

        public AnswerFromServer DeleteDetailSupplier(int id)
        {
            protocol.sendCommand(CommandsToServer.DeleteDetailSupplier);
            protocol.sendString(id.ToString());
            return protocol.receiveAnswerFromServer();
        }

        public List<DetailSupplier> FindDetailSuppliersByName(string name)
        {
            protocol.sendCommand(CommandsToServer.FindDetailBySupplierName);
            protocol.sendString(name);
            return protocol.receiveCollection<DetailSupplier>();
        }

        public List<DetailSupplier> FindDetailSuppliersByTotalRate(float rate)
        {
            protocol.sendCommand(CommandsToServer.FindDetailSupplierByTotalRate);
            protocol.sendString(rate.ToString());
            return protocol.receiveCollection<DetailSupplier>();
        }

        public List<Detail> FindDetailByName(string name)
        {
            protocol.sendCommand(CommandsToServer.FindDetailByName);
            protocol.sendString(name);
            return protocol.receiveCollection<Detail>();
        }

        public List<Detail> FindDetailByVendorCode(int code)
        {
            protocol.sendCommand(CommandsToServer.FindDetailByVendorCode);
            protocol.sendString(code.ToString());
            return protocol.receiveCollection<Detail>();
        }

        public List<Detail> FindDetailBySupplierName(string supplierName)
        {
            protocol.sendCommand(CommandsToServer.FindDetailBySupplierName);
            protocol.sendString(supplierName);
            return protocol.receiveCollection<Detail>();
        }
    }
}
