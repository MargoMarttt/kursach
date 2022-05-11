using ClassLibraryForTCPConnectionAPI_C_sharp_;

namespace TCPConnectionAPIClientModule_C_sharp_
{
    public interface IExpertAccess : IDataModifyAccess, ICommonAccess
    {
        AnswerFromServer Rate(int objId, float rate);
    }
}
