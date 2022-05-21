using DatabaseEntities;
using System;
using System.Collections.Generic;

namespace TCPConnectionAPI_C_sharp_
{
    public class ClientAbilityProtocol : IClientAbilityProtocol
    {
        IDataView DBconnection;

        public ClientAbilityProtocol()
        {
            DBconnection = new DatabaseWrapper();
        }

        public void Dispose()
        {
            DBconnection.Dispose();
        }

        public List<Car> FindCarsWhere(Func<Car, bool> comparer)
        {
            return DBconnection.FindCarsWhere(comparer);
        }
    }
}
