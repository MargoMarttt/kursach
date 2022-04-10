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
        //Expert commands
        RateVehicle, 
        //Client commands
    }
}
