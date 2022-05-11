using ClassLibraryForTCPConnectionAPI_C_sharp_;
using DatabaseEntities;

namespace TCPConnectionAPIClientModule_C_sharp_
{
    public interface IDataModifyAccess : IDataViewAccess   
    {
        AnswerFromServer CreateNewDetailSupplier(DetailSupplier obj);
        AnswerFromServer CreateNewDetail(Detail obj, string supplierName);
        AnswerFromServer UpdateDetail(Detail newVersion);
        AnswerFromServer UpdateDetailSupplier(DetailSupplier newVersion);
        AnswerFromServer DeleteDetail(int id);
        AnswerFromServer DeleteDetailSupplier(int id);
    }
}
