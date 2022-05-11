using DatabaseEntities;
using System;
using System.Collections.Generic;

namespace TCPConnectionAPI_C_sharp_
{
    public class ClientAbilityProtocol : IClientAbilityProtocol
    {
        IDataViewPermision DBconnection;

        public ClientAbilityProtocol()
        {
            DBconnection = new DatabaseContext();
        }

        public void Dispose()
        {
            DBconnection.Dispose();
        }

        public List<Detail> FindDetails(Func<Detail, bool> func)
        {
            return DBconnection.FindDetails(func);
        }

        public List<DetailSupplier> FindDetailSuppliers(Func<DetailSupplier, bool> func)
        {
            return DBconnection.FindDetailSuppliers(func);
        }
    }
}
