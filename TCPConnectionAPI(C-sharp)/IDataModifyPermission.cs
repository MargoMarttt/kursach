using System;
using DatabaseEntities;
namespace TCPConnectionAPI_C_sharp_
{
    public interface IDataModifyPermission : IDataViewPermision
    {
        int CreateDetailSupplier(DetailSupplier obj);
        bool DeleteDetailSupplierWhere(Func<DetailSupplier, bool> func);
        bool DeleteDetailsWhere(Func<Detail, bool> func);
        bool UpdateDetailSupplier(DetailSupplier newVersion);
        bool UpdateDetail(Detail newVersion);
    }
}
