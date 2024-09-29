namespace Code.Services.ClientsService
{
    public interface IClientsServiceProvider
    {
        public IClientsService ClientsService { get; }

        public void Set(IClientsService clientsService);
    }
}