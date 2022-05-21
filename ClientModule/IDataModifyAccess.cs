using ClassLibraryForTCPConnectionAPI_C_sharp_;
using DatabaseEntities;

namespace TCPConnectionAPIClientModule_C_sharp_
{
    public interface IDataModifyAccess : IDataViewAccess   
    {
        AnswerFromServer CreateCar(Car obj);
        AnswerFromServer ModifyCar(Car obj);
        AnswerFromServer DeleteCar(int id);
    }
}
