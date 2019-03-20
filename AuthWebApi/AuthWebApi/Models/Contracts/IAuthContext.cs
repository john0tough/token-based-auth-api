namespace AuthWebApi.Models
{
   public interface IAuthContext: IContext
   {
      IDbEntity<Client> EntityClients { get; set; }
      IDbEntity<RefreshToken> EntityRefreshTokens { get; set; }
   }
}