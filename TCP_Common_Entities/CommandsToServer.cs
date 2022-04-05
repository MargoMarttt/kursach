using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP_Common_Entities
{
    public enum CommandsToServer
    {
        Registration,
        Authorization,
        Disconnect,
        //Admin commands
        RegisterNewUser,
        BanClient,
        BanExpert,
        UnbanClient,
        UnbanExpert,
        DeleteClient,
        DeleteExpert,
        FindClientByLogin,
        GetAllClients,
        FindExpertByLogin,
        GetAllExperts,
        FindAdminByLogin,
    }
}
