using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;

namespace Server
{
    public interface IUserModifyPermission : IUserViewPermission
    {
        int CreateAdmin(Admin admin);
        int CreateClient(Client client);
        int CreateExpert(Expert expert);

        bool UpdateClient(Client newVersion);//С ID изменяемого объекта
        bool DeleteClientsWhere(Func<Client, bool> comparer);

        bool UpdateExpert(Expert newVersion);//С ID изменяемого объекта
        bool DeleteExpertsWhere(Func<Expert, bool> comparer);

        bool UpdateAdmin(Admin newVersion);


    }
}
