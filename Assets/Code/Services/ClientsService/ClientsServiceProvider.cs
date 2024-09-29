namespace Code.Services.ClientsService
{
    public sealed class ClientsServiceProvider : IClientsServiceProvider
    {
        public IClientsService ClientsService { get; private set; }
        
        public void Set(IClientsService clientsService)
        {
            ClientsService = clientsService;
        }
    }
}