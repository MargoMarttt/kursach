namespace ClassLibraryForTCPConnectionAPI_C_sharp_
{
    public enum CommandsToServer
    {
        Registration,
        Authorization,
        PreviousRoom,
        //Admin commands
        RegisterNewUser,
        BanClient,
        BanExpert,
        UnbanClient,
        UnbanExpert,
        DeleteClient,
        DeleteExpert,
        FindClientByLogin,
        GetAllClients,
        FindExpertByLogin,
        GetAllExperts,
        FindAdminByLogin,
        CreateDetailSupplier,
        CreateDetail,
        UpdateDetailSupplier,
        UpdateDetail,
        DeleteDetailSupplier,
        DeleteDetail,
        //Expert commands
        RateDetailSupplier, 
        //Client commands
        FindDetailSupplierByName,
        FindDetailByName,
        FindDetailByVendorCode,
        FindDetailBySupplierName,
        FindDetailSupplierByTotalRate,
        GetAllDetails,
        GetAllSuppliers
    }
}
