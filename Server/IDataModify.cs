using System;
using DatabaseEntities;
namespace TCPConnectionAPI_C_sharp_
{
    public interface IDataModify : IDataView
    {
        int CreateCar(Car obj);
        bool DeleteCarsWhere(Func<Car, bool> comparer);
        bool UpdateCar(Car newVersion);
    }
}
