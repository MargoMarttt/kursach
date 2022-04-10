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

        public ExpertAbilityProtocol()
        {
            DBconnection = new DatabaseContext();
        }
    }
}
