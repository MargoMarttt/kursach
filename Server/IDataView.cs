using DatabaseEntities;
using System;
using System.Collections.Generic;
namespace TCPConnectionAPI_C_sharp_
{
    public interface IDataView : IDisposable
    {
        List<Car> FindCarsWhere(Func<Car, bool> comparer);
    }
}
