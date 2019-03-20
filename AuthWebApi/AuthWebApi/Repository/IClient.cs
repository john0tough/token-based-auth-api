namespace AuthWebApi.Repository
{
   public interface IClient<out TClient>
   {
      TClient FindClient(string clientId);
   }
}