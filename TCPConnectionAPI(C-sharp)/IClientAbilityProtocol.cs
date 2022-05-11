using DatabaseEntities;
using System;
using System.Collections.Generic;

namespace TCPConnectionAPI_C_sharp_
{
    public interface IClientAbilityProtocol : IDisposable
    {
        List<DetailSupplier> FindDetailSuppliers(Func<DetailSupplier, bool> func);
        List<Detail> FindDetails(Func<Detail, bool> func);
    }
}
