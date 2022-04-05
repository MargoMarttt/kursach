using System;
using System.Collections.Generic;
using DatabaseEntities;
using TCP_Common_Entities;

namespace Server
{
    public interface IAdminAbilityProtocol : IClientAbilityProtocol
    {
        int RegisterNewUser(TypeOfUser type, string login, string password, float rateWeight = 0);
        List<Client> FindClientsWhere(Func<Client, bool> comparer);
        List<Admin> FindAdminsWhere(Func<Admin, bool> comparer);
        List<Expert> FindExpertsWhere(Func<Expert, bool> comparer);
        bool BanClientsWhere(Func<Client, bool> comparer);
        bool BanExpertsWhere(Func<Expert, bool> comparer);
        bool UnbanExpertsWhere(Func<Expert, bool> comparer);
        bool UnbanClientsWhere(Func<Client, bool> comparer);
        bool DeleteClientsWhere(Func<Client, bool> comparer);
        bool DeleteExpertsWhere(Func<Expert, bool> comparer);
        string CreateReportAboutVehicles();
    }
}
