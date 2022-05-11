using DatabaseEntities;
using System.Collections.Generic;

namespace TCPConnectionAPIClientModule_C_sharp_
{
    public interface IDataViewAccess
    {
        List<DetailSupplier> FindDetailSuppliersByName(string name);
        List<DetailSupplier> FindDetailSuppliersByTotalRate(float rate);
        List<Detail> FindDetailByName(string name);
        List<Detail> FindDetailByVendorCode(int code);
        List<Detail> FindDetailBySupplierName(string supplierName);
    }
}
