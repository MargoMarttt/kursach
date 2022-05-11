using DatabaseEntities;
using System;
using System.Collections.Generic;


namespace TCPConnectionAPI_C_sharp_
{
    public class ExpertAbilityProtocol : IExpertAbilityProtocol
    {
        public IExpertMethod expertMethod { get; set; }

        public IDataModifyPermission DBconnection;

        public void Dispose()
        {
            DBconnection.Dispose();
        }

        public IRateable Rate(IRateable obj, Expert expert, float rate)
        {
            return expertMethod.Rate(obj, expert, rate);
        }

        public List<DetailSupplier> FindDetailSuppliers(Func<DetailSupplier, bool> func)
        {
            return DBconnection.FindDetailSuppliers(func);
        }

        public List<Detail> FindDetails(Func<Detail, bool> func)
        {
            return DBconnection.FindDetails(func);
        }

        public ExpertAbilityProtocol()
        {
            DBconnection = new DatabaseContext();
        }
    }
}
